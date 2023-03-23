using System;
using System.Linq;
using System.Reflection;

using R5T.F0000;
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
            var attributeOrDefault = memberInfo.CustomAttributes
                .Where(xAttribute => xAttribute.AttributeType.FullName == attributeTypeNamespacedTypeName)
                // Choose first even though there might be multiple since this function is more like "Any()".
                .FirstOrDefault();

            var output = WasFound.From(attributeOrDefault);
            return output;
        }

        public bool IsObsolete(MemberInfo memberInfo)
        {
            var hasObsoleteAttribute = this.HasAttributeOfType(
                memberInfo,
                Instances.TypeNames.ObsoleteAttribute);

            return hasObsoleteAttribute;
        }
    }
}
