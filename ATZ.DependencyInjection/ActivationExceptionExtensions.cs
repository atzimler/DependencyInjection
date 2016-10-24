using System;
using System.Collections.Generic;
using System.Text;
using Ninject;

namespace ATZ.DependencyInjection
{
    internal static class ActivationExceptionExtensions
    {
        private static string GetActivationPath(Type interfaceType, Stack<Type> activation)
        {
            var sb = new StringBuilder();
            var counter = 1;

            while (activation.Count > 1)
            {
                var activationType = activation.Pop();
                sb.AppendLine(
                    $"  {counter++}) Looking for bindings of {GetContravariantInterfaceName(interfaceType, activationType)}, found none.");
            }
            sb.AppendLine($"  {counter}) Request for {GetContravariantInterfaceName(interfaceType, activation.Pop())}, no bindings found.");

            return sb.ToString();
        }

        // TODO: This is Reflection.
        private static string GetContravariantInterfaceName(Type interfaceType, Type interfaceArgument)
        {
            return $"{GetInterfaceName(interfaceType)}{{in {interfaceArgument.Name}}}";
        }

        // TODO: This is Reflection.
        private static string GetInterfaceName(Type interfaceType)
        {
            return interfaceType.Name.Replace("`1", "");
        }

        internal static ActivationException Create(Type interfaceType, Type interfaceArgument, Stack<Type> activation)
        {
            return new ActivationException($@"Error activating {GetContravariantInterfaceName(interfaceType, interfaceArgument)}
No matching contravariant bindings are available, and the type is not self-bindable.
Activation path:
{GetActivationPath(interfaceType, activation)}
Suggestions:
  1) Ensure that you have defined a contravariant binding for {GetInterfaceName(interfaceType)} with type parameter of {interfaceArgument.Name} or one of its base class.
");

        }
    }
}
