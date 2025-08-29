using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using R5T.L0066.Extensions;
using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IReflectionOperations : IFunctionalityMarker
    {
        public string Describe_Type(TypeInfo type)
        {
            var typeName = Instances.TypeOperator.Get_NameOf(type);
            return typeName;
        }

        /// <inheritdoc cref="IMethodOperator.Is_PropertyMethod(MethodInfo)"/>
        public bool IsPropertyMethod(MethodInfo methodInfo)
            => Instances.MethodOperator.Is_PropertyMethod(methodInfo);

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

            propertiesOnTypes.For_Each(tuple => action(tuple.TypeInfo, tuple.PropertyInfo));
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

            methodsOnTypes.For_Each(tuple => action(tuple.TypeInfo, tuple.MethodInfo));
        }

        /// <inheritdoc cref="F10Y.L0000.IAssemblyOperator.Enumerate_Types(Assembly)"/>
        public IEnumerable<TypeInfo> Get_TypesInAssembly(Assembly assembly)
        {
            return Instances.AssemblyOperator.Enumerate_Types(assembly);
        }

        public void List_TypesInAssembly(
            Assembly assembly,
            Action<string> typeDescriptionConsumer)
        {
            var types = this.Get_TypesInAssembly(assembly);

            foreach (var type in types)
            {
                var descriptionOfType = this.Describe_Type(type);

                typeDescriptionConsumer(descriptionOfType);
            }
        }

        public void List_TypesInAssembly(
            Assembly assembly,
            TextWriter writer)
        {
            void TypeDescriptionConsumer(string description) => writer.WriteLine(description);

            this.List_TypesInAssembly(
                assembly,
                TypeDescriptionConsumer);
        }

        public void List_TypesInAssembly_ToConsole(
            Assembly assembly)
        {
            this.List_TypesInAssembly(
                assembly,
                Console.Out);
        }

        public IEnumerable<(TypeInfo TypeInfo, MethodInfo MethodInfo)> SelectMethodsOnTypes(
            Assembly assembly,
            Func<TypeInfo, bool> typeSelector,
            Func<MethodInfo, bool> methodSelector)
        {
            var output = Instances.AssemblyOperator.Select_Types(assembly, typeSelector)
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
