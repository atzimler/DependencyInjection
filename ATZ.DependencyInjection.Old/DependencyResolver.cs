using ATZ.Reflection;
using JetBrains.Annotations;
using Ninject;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATZ.DependencyInjection
{
    /// <summary>
    /// Class for holding the singleton instance of the Ninject kernel.
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// The singleton instance of the Ninject kernel.
        /// </summary>
        [NotNull]
        public static IKernel Instance { get; private set; }

        static DependencyResolver()
        {
            Instance = new StandardKernel();
        }

        private static bool IsContravariantTemplate([NotNull] Type interfaceType)
        {
            var genericTypeParameters = interfaceType.GetGenericTypeParameters();
            if (genericTypeParameters.Length == 0 || genericTypeParameters[0] == null)
            {
                throw new ArgumentOutOfRangeException(nameof(interfaceType));
            }
            var isContravariantTemplate = genericTypeParameters[0].IsContravariant();
            return isContravariantTemplate;
        }


        private static object ResolveInterface([NotNull] IKernel kernel, [NotNull] Type interfaceType, [NotNull] Type interfaceArgument)
        {
            var isContravariantTemplate = IsContravariantTemplate(interfaceType);

            var activation = new Stack<Type>();

            var templateArgument = interfaceArgument;
            while (templateArgument != null)
            {
                activation.Push(templateArgument);

                var closedTemplateType = interfaceType.CloseTemplate(new[] { templateArgument });
                var bindings = kernel.GetBindings(closedTemplateType);
                // ReSharper disable PossibleMultipleEnumeration => bindings.Any should return if there is any element, so it will not enumerate the bindings,
                // and bindings.ToList() will enumerate only it, when registration of the newly found bindings should occurs. => no multiple enumeration of sequence.
                if (bindings.Any())
                {
                    if (templateArgument != interfaceArgument)
                    {
                        bindings
                            .ToList()
                            .ForEach(
                                b =>
                                {
                                    if (b != null)
                                    {
                                        kernel.AddBinding(new Binding(interfaceType, b.BindingConfiguration));
                                    }
                                });
                    }
                    return kernel.Get(closedTemplateType);
                }
                // ReSharper restore PossibleMultipleEnumeration

                templateArgument = isContravariantTemplate ? templateArgument.BaseType : null;
            }

            throw ActivationExceptionExtensions.Create(interfaceType, interfaceArgument, activation);
        }

        /// <summary>
        /// Initialize (or reinitialize) the kernel.
        /// </summary>
        public static void Initialize()
        {
            Instance.Dispose();

            Instance = new StandardKernel();
        }

        /// <summary>
        /// Get an interface with the specified interface type for the interface argument type. If the interface contains contravariant type
        /// and no specific interface binding is registered then try to apply contravariancy to locate the interface requested.
        /// </summary>
        /// <param name="kernel">The kernel instance used to resolve the interface type.</param>
        /// <param name="interfaceType">The requested interface type.</param>
        /// <param name="interfaceArgument">The type to use as the parameter of the interface. If the parameter in the interface is contravariant and no
        /// exact match can be found, the algorithms try to apply contravariancy to find proper interface type binding. If this resolution leads to
        /// success, the resolution is placed into the kernel for optimizing future response times.</param>
        /// <returns>The result of the type resolution.</returns>
        /// <exception cref="ArgumentNullException">Either kernel or interfaceType or interfaceArgument parameter is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The interfaceType has more than one generic parameter or is non-generic. Because contravariancy resolution
        /// complicates the situation if more than one parameter is used on the interface, it is currently not supported.</exception>
        /// <exception cref="ActivationException">The request cannot be fulfilled even when trying to apply contravariance.</exception>
        /// <remarks>This is the implementation of the GetInterface without type safety on the return value to allow debugging of binding problems.</remarks>
        public static object GetInterface(this IKernel kernel, Type interfaceType, Type interfaceArgument)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException(nameof(kernel));
            }
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }
            if (interfaceArgument == null)
            {
                throw new ArgumentNullException(nameof(interfaceArgument));
            }

            if (interfaceType.GenericTypeParameterCount() > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(interfaceType), $@"Error activating {interfaceType.NonGenericName()}.
At the moment, multiple generic parameters are not supported by this method.");
            }

            return ResolveInterface(kernel, interfaceType, interfaceArgument);
        }

        /// <summary>
        /// Get an interface with the specified interface type for the interface argument type. If the interface contains contravariant type
        /// and no specific interface binding is registered then try to apply contravariancy to locate the interface requested.
        /// </summary>
        /// <param name="kernel">The kernel instance used to resolve the interface type.</param>
        /// <param name="interfaceType">The requested interface type.</param>
        /// <param name="interfaceArgument">The type to use as the parameter of the interface. If the parameter in the interface is contravariant and no
        /// exact match can be found, the algorithms try to apply contravariancy to find proper interface type binding. If this resolution leads to
        /// success, the resolution is placed into the kernel for optimizing future response times.</param>
        /// <returns>The result of the type resolution.</returns>
        /// <exception cref="ArgumentNullException">interfaceType is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">The interfaceType has more than one generic parameter or is non-generic. Because contravariancy resolution
        /// complicates the situation if more than one parameter is used on the interface, it is currently not supported.</exception>
        /// <exception cref="ActivationException">The request cannot be fulfilled even when trying to apply contravariance.</exception>
        /// <exception cref="ArgumentException">The interface parameter is non-generic.</exception>
        /// <remarks>Uses internally the non-typesafe version of GetInterface and returns the values in a type safe manner. When not debugging binding issues, this version of
        /// the method should be used as best practice.</remarks>
        public static T GetInterface<T>(this IKernel kernel, Type interfaceType, Type interfaceArgument)
            where T : class
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            var obj = GetInterface(kernel, interfaceType, interfaceArgument);
            var resolution = obj as T;
            if (obj != null && resolution == null)
            {
                throw new ActivationException($"Activated object of type {interfaceType.ParameterizedGenericName(interfaceArgument)} cannot be casted to expected return type {typeof(T)}!");
            }

            return resolution;
        }
    }
}
