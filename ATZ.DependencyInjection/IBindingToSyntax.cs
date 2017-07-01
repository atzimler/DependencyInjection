using JetBrains.Annotations;

namespace ATZ.DependencyInjection
{
    public interface IBindingToSyntax<T>
    {
        IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T;

        IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstant<TImplementation>([NotNull] TImplementation value)
            where TImplementation : T;
    }
}