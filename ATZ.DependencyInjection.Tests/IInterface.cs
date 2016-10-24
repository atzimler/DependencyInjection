namespace ATZ.DependencyInjection.Tests
{
    // ReSharper disable once UnusedTypeParameter => Type parameter is needed to test Contravariancy.
    public interface IInterface<in T>
    {
    }
}
