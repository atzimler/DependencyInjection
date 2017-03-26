namespace ATZ.DependencyInjection.System.Windows
{
    /// <summary>
    /// Bindings for the ATZ.DependencyInjection.System.Windows namespace.
    /// </summary>
    // ReSharper disable once UnusedMember.Global => Part of public API.
    public static class Bindings
    {
        /// <summary>
        /// Initialize Bindings for the interfaces and their implementation in the ATZ.DependencyInjection.System.Windows
        /// namespace.
        /// </summary>
        // ReSharper disable once UnusedMember.Global => Part of public API.
        public static void Initialize()
        {
            DependencyResolver.Instance.Bind<IMessageBox>().ToConstant(new SystemWindowsMessageBox());
        }
    }
}
