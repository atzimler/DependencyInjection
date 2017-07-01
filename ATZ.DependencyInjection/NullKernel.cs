using System;
using System.Collections.Generic;

namespace ATZ.DependencyInjection
{
    public class NullKernel : IKernel
    {
        public void AddBinding(Type type, IBindingConfiguration bindingConfiguration)
        {
            throw new UnitializedKernelException();
        }

        public IBindingToSyntax<object> Bind(params Type[] services)
        {
            throw new UnitializedKernelException();
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            throw new UnitializedKernelException();
        }

        public IEnumerable<IBinding> GetBindings(Type type)
        {
            throw new UnitializedKernelException();
        }

        public object Get(Type type)
        {
            throw new UnitializedKernelException();
        }

        public T Get<T>()
        {
            throw new UnitializedKernelException();
        }

        public void Unbind<T>()
        {
            throw new UnitializedKernelException();
        }

        public void Unbind(Type service)
        {
            throw new UnitializedKernelException();
        }
    }
}