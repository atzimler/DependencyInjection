using System.Diagnostics;

namespace ATZ.DependencyInjection.System
{
    /// <summary>
    /// Interface for replacing System.Diagnostics.Debug static object, so that its calls
    /// can be tested for TDD.
    /// </summary>
    public interface IDebug
    {
        // ReSharper disable UnusedMember.Global => Part of public API.
        /// <see cref="Debug.WriteLine(object)"/>
        void WriteLine(object value);

        /// <see cref="Debug.WriteLine(object, string)"/>
        void WriteLine(object value, string category);

        /// <see cref="Debug.WriteLine(string)"/>
        void WriteLine(string message);

        /// <see cref="Debug.WriteLine(string, object[])"/>
        void WriteLine(string format, params object[] args);

        /// <see cref="Debug.WriteLine(string, string)"/>
        void WriteLine(string message, string category);
        // ReSharper restore UnusedMember.Global
    }
}