using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IReflectionOperations : IFunctionalityMarker
    {
        /// <summary>
        /// Determines whether the method is a property get or set method.
        /// </summary>
        public bool IsPropertyMethod(MethodInfo methodInfo)
        {
            var output = true
                // All property methods have special names.
                && methodInfo.IsSpecialName
                && methodInfo.DeclaringType.GetProperties()
                    .Any(property => false
                        || property.GetGetMethod() == methodInfo
                        || property.GetSetMethod() == methodInfo);

            return output;
        }

        public void ForPropertiesOnTypes(
            Assembly assembly,
            Func<TypeInfo, bool> typeSelector,
            Func<PropertyInfo, bool> propertySelector,
            Action<TypeInfo, PropertyInfo> action)
        {
            var propertiesOnTypes = this.SelectPropertiesOnTypes(
                assembly,
                typeSelector,
                propertySelector);

            propertiesOnTypes.ForEach(tuple => action(tuple.TypeInfo, tuple.PropertyInfo));
        }

        public void ForMethodsOnTypes(
            Assembly assembly,
            Func<TypeInfo, bool> typeSelector,
            Func<MethodInfo, bool> methodSelector,
            Action<TypeInfo, MethodInfo> action)
        {
            var methodsOnTypes = this.SelectMethodsOnTypes(
                assembly,
                typeSelector,
                methodSelector);

            methodsOnTypes.ForEach(tuple => action(tuple.TypeInfo, tuple.MethodInfo));
        }

        public IEnumerable<(TypeInfo TypeInfo, MethodInfo MethodInfo)> SelectMethodsOnTypes(
            Assembly assembly,
            Func<TypeInfo, bool> typeSelector,
            Func<MethodInfo, bool> methodSelector)
        {
            var output = F0000.AssemblyOperator.Instance.SelectTypes(assembly, typeSelector)
                .SelectMany(typeInfo => typeInfo.DeclaredMethods
                    .Where(methodSelector)
                    .Select(methodInfo => (typeInfo, methodInfo)));

            return output;
        }

        public IEnumerable<(TypeInfo TypeInfo, PropertyInfo PropertyInfo)> SelectPropertiesOnTypes(
            Assembly assembly,
            Func<TypeInfo, bool> typeSelector,
            Func<PropertyInfo, bool> propertySelector)
        {
            var output = assembly.DefinedTypes
                .Where(typeSelector)
                .SelectMany(typeInfo => typeInfo.DeclaredProperties
                    .Where(propertySelector)
                    .Select(propertyInfo => (typeInfo, propertyInfo)));

            return output;
        }
    }
}
