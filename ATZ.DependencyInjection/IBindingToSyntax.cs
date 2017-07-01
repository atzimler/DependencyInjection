using JetBrains.Annotations;
using System;

namespace ATZ.DependencyInjection
{
    public interface IBindingToSyntax<T>
    {
        IBindingWhenInNamedWithOrOnSyntax<T> To([NotNull] Type implementation);

        IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T;

        IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstant<TImplementation>([NotNull] TImplementation value)
            where TImplementation : T;
    }
}