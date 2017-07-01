namespace ATZ.DependencyInjection
{
    public class NullBindingToSyntax<T> : IBindingToSyntax<T>
    {
        public IBindingWhenInNamedWithOrOnSyntax<TImplementation> To<TImplementation>() where TImplementation : T
        {
            return new NullBindingWhenInNamedWithOrOnSyntax<TImplementation>();
        }

        public IBindingWhenInNamedWithOrOnSyntax<TImplementation> ToConstant<TImplementation>(TImplementation value) where TImplementation : T
        {
            return new NullBindingWhenInNamedWithOrOnSyntax<TImplementation>();
        }
    }
}