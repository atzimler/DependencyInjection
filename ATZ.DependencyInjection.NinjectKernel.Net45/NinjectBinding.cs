using JetBrains.Annotations;

namespace ATZ.DependencyInjection.NinjectKernel.Net45
{
    public class NinjectBinding : IBinding
    {
        [NotNull]
        public Ninject.Planning.Bindings.IBinding Binding { get; }

        public NinjectBinding([NotNull] Ninject.Planning.Bindings.IBinding binding)
        {
            Binding = binding;
        }

        public IBindingConfiguration BindingConfiguration
            => new NinjectBindingConfiguration(Binding.BindingConfiguration);
    }
}