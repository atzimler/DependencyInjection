using System.Windows;

namespace ATZ.DependencyInjection.System.Windows
{
    /// <summary>
    /// Interface for replacing System.Windows.MessageBox static object, so that its calls
    /// can be tested for TDD.
    /// </summary>
    public interface IMessageBox
    {
        // ReSharper disable UnusedMember.Global => Part of public API.
        /// <see cref="MessageBox.Show(string)"/>
        MessageBoxResult Show(string messageBoxText);
        /// <see cref="MessageBox.Show(string, string)"/>
        MessageBoxResult Show(string messageBoxText, string caption);
        /// <see cref="MessageBox.Show(string, string, MessageBoxButton)"/>
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);
        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage)"/>
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult)"/>
        MessageBoxResult Show(
            string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult);
        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)"/>
        MessageBoxResult Show(
            string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options);

        /// <see cref="MessageBox.Show(Window, string)"/>
        MessageBoxResult Show(Window owner, string messageBoxText);
        /// <see cref="MessageBox.Show(Window, string, string)"/>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption);
        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton)"/>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button);
        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage)"/>
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult)"/>
        MessageBoxResult Show(
            Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult);
        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)"/>
        MessageBoxResult Show(
            Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options);
        // ReSharper restore UnusedMember.Global
    }
}
