using System;
using System.Reflection;

using R5T.T0132;
using R5T.T0161;


namespace R5T.F0018.F001
{
    [FunctionalityMarker]
    public partial interface ITypeInfoOperator : IFunctionalityMarker
    {
        private static F0018.ITypeInfoOperator StringlyTypedOperator => F0018.TypeInfoOperator.Instance;


        /// <inheritdoc cref="F0018.ITypeInfoOperator.Get_TypeName(TypeInfo)"/>
        public TypeName Get_TypeName(TypeInfo typeInfo)
        {
            var typeName = Instances.TypeOperator.Get_TypeName(typeInfo);
            return typeName;
        }

        /// <inheritdoc cref="F0018.ITypeInfoOperator.Select_Method(TypeInfo, string)"/>
        public MethodInfo Select_Method(
            TypeInfo typeInfo,
            MethodName methodName)
        {
            var methodInfo = StringlyTypedOperator.Select_Method(
                typeInfo,
                methodName.Value);

            return methodInfo;
        }

        /// <inheritdoc cref="F0018.ITypeInfoOperator.Select_Property(TypeInfo, string)"/>
        public PropertyInfo Select_Property(
            TypeInfo typeInfo,
            PropertyName propertyName)
        {
            var propertyInfo = StringlyTypedOperator.Select_Property(
                typeInfo,
                propertyName.Value);

            return propertyInfo;
        }
    }
}
