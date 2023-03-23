using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using R5T.F0000;
using R5T.T0132;


namespace R5T.F0018
{
	[FunctionalityMarker]
	public partial interface ITypeOperator : IFunctionalityMarker,
        F0000.ITypeOperator
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
            string attributeNamespacedTypeName)
        {
            bool Internal(Type type)
            {
                var output = true
                   // Does the type have the attribute?
                   && this.HasAttributeOfType(
                       type,
                       attributeNamespacedTypeName)
                   ;

                return output;
            }

            return Internal;
        }

        /// <summary>
        /// Returns the <inheritdoc cref="Documentation.TypeNameMeansFullyQualifiedTypeName" path="/summary"/> of the type.
        /// </summary>
        public string Get_TypeName(Type type)
        {
            // The full name corresponds to our concept of type name.
            var typeName = type.FullName;
            return typeName;
        }

        /// <inheritdoc cref="Get_TypeName(Type)"/>
        public string Get_TypeNameOf<T>()
        {
            var type = this.GetTypeOf<T>();

            // The full name corresponds to our concept of type name.
            var typeName = this.Get_TypeName(type);
            return typeName;
        }

        public WasFound<CustomAttributeData> HasAttributeOfType(
            Type type,
            string attributeTypeNamespacedTypeName)
        {
            var output = Instances.MemberOperator.HasAttributeOfType(type, attributeTypeNamespacedTypeName);
            return output;
        }

        public bool HasBaseType(Type type)
        {
            var hasBaseType = type.BaseType is object;
            return hasBaseType;
        }

        public bool IsObsolete(Type type)
        {
            var output = Instances.MemberOperator.IsObsolete(type);
            return output;
        }
    }
}