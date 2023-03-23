using System;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IMethodOperator : IFunctionalityMarker
    {
        public bool IsObsolete(MethodInfo methodInfo)
        {
            var output = Instances.MemberOperator.IsObsolete(methodInfo);
            return output;
        }
    }
}
