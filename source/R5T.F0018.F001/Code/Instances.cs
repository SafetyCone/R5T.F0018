using System;


namespace R5T.F0018.F001
{
    public static class Instances
    {
        public static IAssemblyOperator AssemblyOperator => F001.AssemblyOperator.Instance;
        public static IReflectionOperations ReflectionOperations => F001.ReflectionOperations.Instance;
        public static IReflectionOperator ReflectionOperator => F001.ReflectionOperator.Instance;
        public static ITypeInfoOperator TypeInfoOperator => F001.TypeInfoOperator.Instance;
        public static ITypeOperator TypeOperator => F001.TypeOperator.Instance;
    }
}