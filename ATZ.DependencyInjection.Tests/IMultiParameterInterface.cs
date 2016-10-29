namespace ATZ.DependencyInjection.Tests
{
    // ReSharper disable UnusedTypeParameter => T1 and T2 are used for checking interface with two template parameter.
    public interface IMultiParameterInterface<in T1, in T2>
    // ReSharper restore UnusedTypeParameter
    {
    }
}
