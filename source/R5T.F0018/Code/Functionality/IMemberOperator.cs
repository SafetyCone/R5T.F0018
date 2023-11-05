using System;
using System.Reflection;

using R5T.N0000;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IMemberOperator : IFunctionalityMarker
    {
        public WasFound<CustomAttributeData> HasAttributeOfType(
            MemberInfo memberInfo,
            string attributeTypeNamespacedTypeName)
        {
            var exists = Instances.MemberInfoOperator.Has_AttributeOfType(
                memberInfo,
                attributeTypeNamespacedTypeName,
                out var attributeOrDefault);

            var output = WasFound.From(exists, attributeOrDefault);
            return output;
        }

        public bool IsObsolete(MemberInfo memberInfo)
        {
            var output = Instances.MemberInfoOperator.Is_Obsolete(memberInfo);
            return output;
        }
    }
}
