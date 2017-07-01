using System;

namespace ATZ.DependencyInjection
{
    public class ActivationException : Exception
    {
        public ActivationException(string message)
            : base(message)
        {
        }
    }
}
