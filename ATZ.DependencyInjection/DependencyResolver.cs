using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ATZ.Reflection;
using Ninject;
using Ninject.Planning.Bindings;

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
        public static IKernel Instance { get; private set; }

        static DependencyResolver()
        {
            Initialize();
        }

        private static Type ApplyContravarianceToTemplateArgument(Type templateType, Type templateArgument)
        {
            var genericTypeParameters = templateType.GetTypeInfo().GenericTypeParameters;
            if (genericTypeParameters[0].GetTypeInfo().GenericParameterAttributes ==
                GenericParameterAttributes.Contravariant)
            {
                return templateArgument.BaseType;
            }

            return null;
        }

        /// <summary>
        /// Initialize (or reinitialize) the kernel.
        /// </summary>
        public static void Initialize()
        {
            Instance?.Dispose();

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
        /// <exception cref="ArgumentOutOfRangeException">The interfaceType has more than one generic parameter. Because contravariancy resolution
        /// complicates the situation if more than one parameter is used on the interface, it is currently not supported.</exception>
        /// <exception cref="ActivationException">The request cannot be fulfilled even when trying to apply contravariance.</exception>
        /// <exception cref="ArgumentException">The interface parameter is non-generic.</exception>
        /// <remarks>This is the implementation of the GetInterface without type safety on the return value to allow debugging of binding problems.</remarks>
        public static object GetInterface(this IKernel kernel, Type interfaceType, Type interfaceArgument)
        {
            if (interfaceType.GenericTypeParameterCount() > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(interfaceType), $@"Error activating {interfaceType.NonGenericName()}.
At the moment, multiple generic parameters are not supported by this method.");
            }

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
                        bindings.ToList().ConvertAll(b => new Binding(interfaceType, b.BindingConfiguration)).ForEach(kernel.AddBinding);
                    }
                    return kernel.Get(closedTemplateType);
                }
                // ReSharper restore PossibleMultipleEnumeration

                templateArgument = ApplyContravarianceToTemplateArgument(interfaceType, templateArgument);
            }

            throw ActivationExceptionExtensions.Create(interfaceType, interfaceArgument, activation);
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
        /// <exception cref="ArgumentOutOfRangeException">The interfaceType has more than one generic parameter. Because contravariancy resolution
        /// complicates the situation if more than one parameter is used on the interface, it is currently not supported.</exception>
        /// <exception cref="ActivationException">The request cannot be fulfilled even when trying to apply contravariance.</exception>
        /// <exception cref="ArgumentException">The interface parameter is non-generic.</exception>
        /// <remarks>Uses internally the non-typesafe version of GetInterface and returns the values in a type safe manner. When not debugging binding issues, this version of
        /// the method should be used as best practice.</remarks>
        public static T GetInterface<T>(this IKernel kernel, Type interfaceType, Type interfaceArgument)
            where T : class
        {
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
