using System;
using System.Reflection;


namespace R5T.F0018.Extensions
{
    public static class AssemblyExtensions
    {
        public static void Foreach_TypeInAssembly(this Assembly assembly,
            Action<TypeInfo> action)
        {
            Instances.AssemblyOperator.Foreach_TypeInAssembly(
                assembly,
                action);
        }
    }
}
