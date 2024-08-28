using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IPropertyOperator : IFunctionalityMarker
    {
        public bool IsObsolete(PropertyInfo propertyInfo)
        {
            var output = Instances.MemberOperator.Is_Obsolete(propertyInfo);
            return output;
        }

        public bool Is_ValueProperty(PropertyInfo propertyInfo)
        {
            var output = true
                // Only properties with get methods.
                && propertyInfo.GetMethod is object
                // Only properties with public get methods.
                && propertyInfo.GetMethod.IsPublic
                // Only properties *without* set methods.
                && propertyInfo.SetMethod is null
                // Only properties that are *not* indexers (which is tested by seeing if the property has any index parameters).
                && propertyInfo.GetIndexParameters().None()
                ;

            return output;
        }
    }
}
