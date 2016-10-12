using Ninject;

namespace ATZ.DependencyInjection
{
    public static class DependencyResolver
    {
        public static IKernel Instance { get; }

        static DependencyResolver()
        {
            Instance = new StandardKernel();
        }
    }
}
