using ATZ.DependencyInjection;
using ATZ.DependencyInjection.NinjectKernel.Net45;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using IBindingConfiguration = ATZ.DependencyInjection.IBindingConfiguration;
using NBinding = Ninject.Planning.Bindings.Binding;

namespace ATZ.DependencyInjection.NinjectKernel.Net45
{
    public class NinjectBindingWhenInNamedWithOrOnSyntax<T> : IBindingWhenInNamedWithOrOnSyntax<T>
    {
        [NotNull]
        public Ninject.Syntax.IBindingWhenInNamedWithOrOnSyntax<T> BindingInNamedWithOrOnSyntax { get; }

        public NinjectBindingWhenInNamedWithOrOnSyntax([NotNull] Ninject.Syntax.IBindingWhenInNamedWithOrOnSyntax<T> bindingInNamedWithOrOnSyntax)
        {
            BindingInNamedWithOrOnSyntax = bindingInNamedWithOrOnSyntax;
        }
    }

    public class NinjectBindingToSyntax<T> : IBindingToSyntax<T>
    {
        [NotNull]
        public Ninject.Syntax.IBindingToSyntax<T> BindingToSyntax { get; }

        public NinjectBindingToSyntax([NotNull] Ninject.Syntax.IBindingToSyntax<T> bindingToSyntax)
        {
            BindingToSyntax = bindingToSyntax;
        }

        public IBindingWhenInNamedWithOrOnSyntax<TImplementation>
            ToConstant<TImplementation>(TImplementation implementation) where TImplementation : T
        {
            return new NinjectBindingWhenInNamedWithOrOnSyntax<TImplementation>(
                BindingToSyntax.ToConstant(implementation));
        }
    }
}

public class NinjectStandardKernel : IKernel
{
    [NotNull]
    public Ninject.StandardKernel Kernel { get; }

    public NinjectStandardKernel()
    {
        Kernel = new Ninject.StandardKernel();
    }

    public void AddBinding(Type type, IBindingConfiguration bindingConfiguration)
    {
        var ninjectBindingConfiguration = (NinjectBindingConfiguration)bindingConfiguration;
        Kernel.AddBinding(new NBinding(type, ninjectBindingConfiguration.BindingConfiguration));
    }

    public ATZ.DependencyInjection.IBindingToSyntax<T> Bind<T>()
    {
        return new NinjectBindingToSyntax<T>(Kernel.Bind<T>());
    }

    public object Get(Type type)
    {
        return Ninject.ResolutionExtensions.Get(Kernel, type);
    }

    public T Get<T>()
    {
        return Ninject.ResolutionExtensions.Get<T>(Kernel);
    }

    public IEnumerable<IBinding> GetBindings(Type type)
    {
        return Kernel.GetBindings(type).Select(b => new NinjectBinding(b));
    }
}

