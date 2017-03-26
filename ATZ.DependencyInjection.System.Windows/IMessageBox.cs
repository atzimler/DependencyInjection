﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATZ.DependencyInjection.System.Windows
{
    public interface IMessageBox
    {
        MessageBoxResult Show(string messageBoxText);
        MessageBoxResult Show(string messageBoxText, string caption);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
        MessageBoxResult Show(
            string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult);
        MessageBoxResult Show(
            string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options);

        MessageBoxResult Show(Window owner, string messageBoxText);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button);
        MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
        MessageBoxResult Show(
            Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult);
        MessageBoxResult Show(
            Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon,
            MessageBoxResult defaultResult, MessageBoxOptions options);
    }
}
