using ATZ.Reflection;
using JetBrains.Annotations;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATZ.DependencyInjection
{
    internal static class ActivationExceptionExtensions
    {
        private static string GetActivationPath([NotNull] Type interfaceType, [NotNull] Stack<Type> activation)
        {
            var sb = new StringBuilder();
            var counter = 1;

            while (activation.Count > 1)
            {
                var activationType = activation.Pop();
                sb.AppendLine(
                    $"  {counter++}) Looking for bindings of {interfaceType.ParameterizedGenericName(activationType)}, found none.");
            }
            sb.AppendLine($"  {counter}) Request for {interfaceType.ParameterizedGenericName(activation.Pop())}, no bindings found.");

            return sb.ToString();
        }

        [NotNull]
        internal static ActivationException Create([NotNull] Type interfaceType, [NotNull] Type interfaceArgument, [NotNull] Stack<Type> activation)
        {
            return new ActivationException($@"Error activating {interfaceType.ParameterizedGenericName(interfaceArgument)}
No matching contravariant bindings are available, and the type is not self-bindable.
Activation path:
{GetActivationPath(interfaceType, activation)}
Suggestions:
  1) Ensure that you have defined a contravariant binding for {interfaceType.NonGenericName()} with type parameter of {interfaceArgument.Name} or one of its base class.
");

        }
    }
}
