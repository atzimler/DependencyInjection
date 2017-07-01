using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace ATZ.DependencyInjection
{
    public interface IKernel
    {
        void AddBinding([NotNull] Type type, [NotNull] IBindingConfiguration bindingConfiguration);

        [NotNull]
        IBindingToSyntax<object> Bind([NotNull] params Type[] services);

        [NotNull]
        IBindingToSyntax<T> Bind<T>();

        object Get([NotNull] Type type);
        T Get<T>();
        IEnumerable<IBinding> GetBindings([NotNull] Type type);
    }
}
