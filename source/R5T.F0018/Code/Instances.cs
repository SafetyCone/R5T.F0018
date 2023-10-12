using System;


namespace R5T.F0018
{
    public static class Instances
    {
        public static IAssemblyOperator AssemblyOperator => F0018.AssemblyOperator.Instance;
        public static F0000.IEnumerableOperator EnumerableOperator => F0000.EnumerableOperator.Instance;
        public static L0053.IMemberInfoOperator MemberInfoOperator => L0053.MemberInfoOperator.Instance;
        public static IMemberOperator MemberOperator => F0018.MemberOperator.Instance;
        public static F0002.IPathOperator PathOperator => F0002.PathOperator.Instance;
        public static ITypeNames TypeNames => F0018.TypeNames.Instance;
        public static ITypeOperator TypeOperator => F0018.TypeOperator.Instance;
    }
}