using JetBrains.Annotations;

namespace ATZ.DependencyInjection.NinjectKernel.Net45
{
    public class NinjectBindingToSyntax<T> : IBindingToSyntax<T>
    {
        [NotNull]
        public Ninject.Syntax.IBindingToSyntax<T> BindingToSyntax { get; }

        public NinjectBindingToSyntax([NotNull] Ninject.Syntax.IBindingToSyntax<T> bindingToSyntax)
        {
            BindingToSyntax = bindingToSyntax;
        }

        public IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T
        {
            return new NinjectBindingWhenInNamedWithOrOnSyntax<TImplementation>(BindingToSyntax.To<TImplementation>());
        }

        public IBindingWhenInNamedWithOrOnSyntax<TImplementation>
            ToConstant<TImplementation>(TImplementation implementation) where TImplementation : T
        {
            return new NinjectBindingWhenInNamedWithOrOnSyntax<TImplementation>(
                BindingToSyntax.ToConstant(implementation));
        }
    }
}