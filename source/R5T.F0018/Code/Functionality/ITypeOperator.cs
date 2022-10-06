using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;


namespace R5T.F0018
{
	[FunctionalityMarker]
	public partial interface ITypeOperator : IFunctionalityMarker
	{
        // Source: https://stackoverflow.com/a/1613936/10658484
        public IEnumerable<Type> GetOnlyDirectlyImplementedInterfaces(Type type)
        {
            var hasBaseType = this.HasBaseType(type);

            var interfaces = type.GetInterfaces();

            var output = hasBaseType
                ? interfaces.Except(type.BaseType.GetInterfaces())
                : interfaces
                ;

            return output;
        }

        public Func<Type, bool> GetTypeByHasAttributeOfNamespacedTypeNamePredicate(
            string markerAttributeNamespacedTypeName)
        {
            bool Internal(Type typeInfo)
            {
                var output = true
                   // Does it have the functionality marker attribute?
                   && this.HasAttributeOfType(
                       typeInfo,
                       markerAttributeNamespacedTypeName)
                   ;

                return output;
            }

            return Internal;
        }

        public bool HasAttributeOfType(
            Type type,
            string attributeTypeNamespacedTypeName)
        {
            var output = type.CustomAttributes
                .Where(xAttribute => xAttribute.AttributeType.FullName == attributeTypeNamespacedTypeName)
                .Any();

            return output;
        }

        public bool HasBaseType(Type type)
        {
            var hasBaseType = type.BaseType is not null;
            return hasBaseType;
        }
    }
}