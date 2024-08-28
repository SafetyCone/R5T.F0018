using System;


namespace R5T.F0018
{
    public static class Instances
    {
        public static IAssemblyOperator AssemblyOperator => F0018.AssemblyOperator.Instance;
        public static L0066.IEnumerableOperator EnumerableOperator => L0066.EnumerableOperator.Instance;
        public static L0066.IMemberInfoOperator MemberInfoOperator => L0066.MemberInfoOperator.Instance;
        public static IMemberOperator MemberOperator => F0018.MemberOperator.Instance;
        public static IMethodOperator MethodOperator => F0018.MethodOperator.Instance;
        public static L0066.IPathOperator PathOperator => L0066.PathOperator.Instance;
        public static IPropertyOperator PropertyOperator => F0018.PropertyOperator.Instance;
        public static ITypeNames TypeNames => F0018.TypeNames.Instance;
        public static ITypeOperator TypeOperator => F0018.TypeOperator.Instance;
    }
}