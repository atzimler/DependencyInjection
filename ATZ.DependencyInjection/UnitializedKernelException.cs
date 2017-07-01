using System;

namespace ATZ.DependencyInjection
{
    public class UnitializedKernelException : Exception
    {
        public UnitializedKernelException()
            : base("Before using the Kernel system it has to be initialized with DependencyResolver.Initialize(<kernel object>)!")
        {
        }
    }
}
