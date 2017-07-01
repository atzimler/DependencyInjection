using System;
using System.Collections.Generic;
using System.Linq;

namespace ATZ.DependencyInjection
{
    public class NullBindingWhenInNamedWithOrOnSyntax<T> : IBindingWhenInNamedWithOrOnSyntax<T>
    {
    }

    public class NullKernel : IKernel
    {
        public void AddBinding(Type type, IBindingConfiguration bindingConfiguration)
        {
        }

        public IBindingToSyntax<object> Bind(params Type[] services)
        {
            return new NullBindingToSyntax<object>();
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return new NullBindingToSyntax<T>();
        }

        public IEnumerable<IBinding> GetBindings(Type type)
        {
            return Enumerable.Empty<IBinding>();
        }

        public object Get(Type type)
        {
            return null;
        }

        public T Get<T>()
        {
            return default(T);
        }
    }
}