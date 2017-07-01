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

        /// <see cref="Debug.WriteLine(string)"/>
        public void WriteLine(string message)
        {
            // ReSharper disable once AssignNullToNotNullAttribute => Just passing parameter through.
            Debug.WriteLine(message);
        }

        /// <see cref="Debug.WriteLine(string, object[])"/>
        public void WriteLine(string format, params object[] args)
        {
            // ReSharper disable AssignNullToNotNullAttribute => Just passing parameters through.
            Debug.WriteLine(format, args);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        /// <see cref="Debug.WriteLine(string, string)"/>
        public void WriteLine(string message, string category)
        {
            // ReSharper disable once AssignNullToNotNullAttribute => Just passing parameter through.
            Debug.WriteLine(message, category);
        }
    }
}
