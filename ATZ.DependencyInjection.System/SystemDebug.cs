using System.Diagnostics;

namespace ATZ.DependencyInjection.System
{
    /// <summary>
    /// IDebug implementation that calls the System.Diagnostics.Debug class.
    /// </summary>
    public class SystemDebug : IDebug
    {
        /// <see cref="Debug.WriteLine(object)"/>
        public void WriteLine(object value)
        {
            Debug.WriteLine(value);
        }

        /// <see cref="Debug.WriteLine(object, string)"/>
        public void WriteLine(object value, string category)
        {
            Debug.WriteLine(value, category);
        }

        /// <see cref="Debug.WriteLine(string)"/>
        public void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }

        /// <see cref="Debug.WriteLine(string, object[])"/>
        public void WriteLine(string format, params object[] args)
        {
            Debug.WriteLine(format, args);
        }

        /// <see cref="Debug.WriteLine(string, string)"/>
        public void WriteLine(string message, string category)
        {
            Debug.WriteLine(message, category);
        }
    }
}
