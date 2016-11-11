namespace ATZ.DependencyInjection.System
{
    /// <summary>
    /// Bindings for the ATZ.DependencyInjection.System namespace.
    /// </summary>
    // ReSharper disable once UnusedMember.Global => Part of public API.
    public static class Bindings
    {
        /// <summary>
        /// Initialize Bindings for the interfaces and their implementation in the ATZ.DependencyInjection.System
        /// namespace.
        /// </summary>
        public static void Initialize()
        {
            DependencyResolver.Instance.Bind<IDebug>().ToConstant(new SystemDebug());
        }
    }
}
