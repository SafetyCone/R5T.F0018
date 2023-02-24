using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface ITypeInfoOperator : IFunctionalityMarker
    {
        /// <inheritdoc cref="ITypeOperator.Get_TypeName(Type)"/>
        public string Get_TypeName(TypeInfo typeInfo)
        {
            var typeName = Instances.TypeOperator.Get_TypeName(typeInfo);
            return typeName;
        }

        /// <summary>
        /// Selects a method from the assembly based on its <inheritdoc cref="Documentation.MethodNameMeansSimpleMethodName" path="/summary"/>.
        /// </summary>
        /// <param name="methodName">The <inheritdoc cref="Documentation.MethodNameMeansSimpleMethodName" path="/summary"/> of the method.</param>
        public MethodInfo Select_Method(
            TypeInfo typeInfo,
            string methodName)
        {
            var methodInfo = typeInfo.DeclaredMethods
                // The name corresponds to our concept of method name.
                .Where(method => method.Name == methodName)
                // Use first for speed (to avoid evaluating all types as required by single), but not first-or-default so that we throw if no type is found, since we know there should only be zero or one types.
                .First();

            return methodInfo;
        }

        /// <summary>
        /// Selects a property from the assembly based on its <inheritdoc cref="Documentation.PropertyNameMeansSimplePropertyName" path="/summary"/>.
        /// </summary>
        /// <param name="propertyName">The <inheritdoc cref="Documentation.PropertyNameMeansSimplePropertyName" path="/summary"/> of the property.</param>
        public PropertyInfo Select_Property(
            TypeInfo typeInfo,
            string propertyName)
        {
            var propertyInfo = typeInfo.DeclaredProperties
                // The name corresponds to our concept of property name.
                .Where(property => property.Name == propertyName)
                // Use first for speed (to avoid evaluating all types as required by single), but not first-or-default so that we throw if no type is found, since we know there should only be zero or one types.
                .First();

            return propertyInfo;
        }
    }
}
