using System;
using System.Reflection;

using R5T.T0132;
using R5T.T0161;


namespace R5T.F0018.F001
{
    [FunctionalityMarker]
    public partial interface IReflectionOperator : IFunctionalityMarker
    {
        private static F0000.ITypeOperator SimpleTypeOperator => F0000.TypeOperator.Instance;
        private static F0018.IReflectionOperator StringlyTypedOperator => F0018.ReflectionOperator.Instance;


        public void InMethodContext_Synchronous(
            string assemblyFilePath,
            ITypeName typeName,
            IMethodName methodName,
            Action<MethodInfo> methodInfoAction)
        {
            this.InTypeContext_Synchronous(
                assemblyFilePath,
                typeName,
                typeInfo =>
                {
                    var methodInfo = Instances.TypeInfoOperator.Select_Method(
                        typeInfo,
                        methodName);

                    methodInfoAction(methodInfo);
                });
        }

        public void InPropertyContext_Synchronous(
            string assemblyFilePath,
            ITypeName typeName,
            IPropertyName propertyName,
            Action<PropertyInfo> propertyInfoAction)
        {
            this.InTypeContext_Synchronous(
                assemblyFilePath,
                typeName,
                typeInfo =>
                {
                    var propertyInfo = Instances.TypeInfoOperator.Select_Property(
                        typeInfo,
                        propertyName);

                    propertyInfoAction(propertyInfo);
                });
        }

        public void InTypeContext_Synchronous(
        string assemblyFilePath,
        ITypeName typeName,
        Action<TypeInfo> typeInfoAction)
        {
            StringlyTypedOperator.InAssemblyContext(
                assemblyFilePath,
                assembly =>
                {
                    var typeInfo = Instances.AssemblyOperator.Select_Type(
                        assembly,
                        typeName);

                    typeInfoAction(typeInfo);
                });
        }

        public void InTypeContext_Synchronous(
            string assemblyFilePath,
            Type type,
            Action<TypeInfo> typeInfoAction)
        {
            var typeName = Instances.TypeOperator.Get_TypeName(type);

            this.InTypeContext_Synchronous(
                assemblyFilePath,
                typeName,
                typeInfoAction);
        }

        public void InTypeContext_Synchronous<T>(
            string assemblyFilePath,
            Action<TypeInfo> typeInfoAction)
        {
            var type = SimpleTypeOperator.GetTypeOf<T>();

            this.InTypeContext_Synchronous(
                assemblyFilePath,
                type,
                typeInfoAction);
        }
    }
}
