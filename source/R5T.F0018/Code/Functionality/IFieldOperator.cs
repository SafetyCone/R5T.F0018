using System;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IFieldOperator : IFunctionalityMarker
    {
        public bool IsObsolete(FieldInfo propertyInfo)
        {
            var output = Instances.MemberOperator.Is_Obsolete(propertyInfo);
            return output;
        }
    }
}
