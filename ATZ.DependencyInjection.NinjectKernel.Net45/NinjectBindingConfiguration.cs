using JetBrains.Annotations;

namespace ATZ.DependencyInjection.NinjectKernel.Net45
{
    public class NinjectBindingConfiguration : IBindingConfiguration
    {
        [NotNull]
        public Ninject.Planning.Bindings.IBindingConfiguration BindingConfiguration { get; }

        public NinjectBindingConfiguration([NotNull] Ninject.Planning.Bindings.IBindingConfiguration bindingConfiguration)
        {
            BindingConfiguration = bindingConfiguration;
        }
    }
}