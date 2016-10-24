using System;
using System.Linq;
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

        public static void Initialize()
        {
            Instance = new StandardKernel();
        }

        public static T GetInterface<T>(this IKernel kernel, Type interfaceType, Type interfaceArgument)
            where T : class
        {
            var templateType = interfaceArgument;
            while (templateType != null)
            {
                var closedTemplateType = interfaceType.CloseTemplate(new[]{templateType});
                var bindings = kernel.GetBindings(closedTemplateType);
                // ReSharper disable PossibleMultipleEnumeration => bindings.Any should return if there is any element, so it will not enumerate the bindings,
                // and bindings.ToList() will enumerate only it, when registration of the newly found bindings should occurs. => no multiple enumeration of sequence.
                if (bindings.Any())
                {
                    if (templateType != interfaceArgument)
                    {
                        bindings.ToList().ConvertAll(b => new Binding(interfaceType, b.BindingConfiguration)).ForEach(kernel.AddBinding);
                    }
                    return kernel.Get(closedTemplateType) as T;
                }
                // ReSharper restore PossibleMultipleEnumeration

                templateType = templateType.BaseType;
            }

            return null;
        }
    }
}
