using JetBrains.Annotations;
using System.Windows;

namespace ATZ.DependencyInjection.System.Windows
{
    /// <summary>
    /// IDebug implementation that calls the System.Windows.MessageBox class.
    /// </summary>
    public class SystemWindowsMessageBox : IMessageBox
    {
        /// <see cref="MessageBox.Show(string)"/>
        public MessageBoxResult Show(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText);
        }

        /// <see cref="MessageBox.Show(string, string)"/>
        public MessageBoxResult Show(string messageBoxText, string caption)
        {
            return MessageBox.Show(messageBoxText, caption);
        }

        /// <see cref="MessageBox.Show(string, string, MessageBoxButton)"/>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(messageBoxText, caption, button);
        }

        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage)"/>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }

        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult)"/>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult);
        }

        /// <see cref="MessageBox.Show(string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)"/>
        public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
        }

        /// <see cref="MessageBox.Show(Window, string)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText)
        {
            return MessageBox.Show(owner, messageBoxText);
        }

        /// <see cref="MessageBox.Show(Window, string, string)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText, string caption)
        {
            return MessageBox.Show(owner, messageBoxText, caption);
        }

        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button);
        }

        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button, icon);
        }

        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult);
        }

        /// <see cref="MessageBox.Show(Window, string, string, MessageBoxButton, MessageBoxImage, MessageBoxResult, MessageBoxOptions)"/>
        public MessageBoxResult Show([NotNull] Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }
    }
}
