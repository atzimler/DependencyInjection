using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ATZ.Reflection;
using Ninject;
using Ninject.Planning.Bindings;

namespace ATZ.DependencyInjection
{
    public static class DependencyResolver
    {
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

        public static void Initialize()
        {
            Instance = new StandardKernel();
        }

        public static T GetInterface<T>(this IKernel kernel, Type interfaceType, Type interfaceArgument)
            where T : class
        {
            var activation = new Stack<Type>();

            var templateArgument = interfaceArgument;
            while (templateArgument != null)
            {
                activation.Push(templateArgument);

                var closedTemplateType = interfaceType.CloseTemplate(new[]{templateArgument});
                var bindings = kernel.GetBindings(closedTemplateType);
                // ReSharper disable PossibleMultipleEnumeration => bindings.Any should return if there is any element, so it will not enumerate the bindings,
                // and bindings.ToList() will enumerate only it, when registration of the newly found bindings should occurs. => no multiple enumeration of sequence.
                if (bindings.Any())
                {
                    if (templateArgument != interfaceArgument)
                    {
                        bindings.ToList().ConvertAll(b => new Binding(interfaceType, b.BindingConfiguration)).ForEach(kernel.AddBinding);
                    }
                    return kernel.Get(closedTemplateType) as T;
                }
                // ReSharper restore PossibleMultipleEnumeration

                templateArgument = ApplyContravarianceToTemplateArgument(interfaceType, templateArgument);
            }

            throw ActivationExceptionExtensions.Create(interfaceType, interfaceArgument, activation);
        }
    }
}
