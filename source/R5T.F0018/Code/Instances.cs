using System;

using R5T.F0000;
using R5T.F0002;


namespace R5T.F0018
{
    public static class Instances
    {
        public static IEnumerableOperator EnumerableOperator { get; } = F0000.EnumerableOperator.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static ITypeOperator TypeOperator { get; } = F0018.TypeOperator.Instance;
    }
}