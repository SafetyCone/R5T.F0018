using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IAssemblyOperator : IFunctionalityMarker
    {
        public void Foreach_TypeInAssembly(
            Assembly assembly,
            Action<TypeInfo> action)
        {
            var typesInAssembly = this.Get_TypesInAssembly(assembly);

            foreach (var typeInfo in typesInAssembly)
            {
                action(typeInfo);
            }
        }

        //public IEnumerable<MemberInfo> Get_MemberInfos(Assembly assembly)
        //{
        //    // Foreach type in the assembly.
        //    var types = this.Get_TypesInAssembly(assembly);
        //}

        /// <summary>
        /// Returns <see cref="Assembly.DefinedTypes"/>.
        /// </summary>
        public IEnumerable<TypeInfo> Get_TypesInAssembly(Assembly assembly)
        {
            return assembly.DefinedTypes;
        }

        /// <summary>
        /// Selects a type from the assembly based on its <inheritdoc cref="Documentation.TypeNameMeansFullyQualifiedTypeName" path="/summary"/>.
        /// </summary>
        /// <param name="typeName">The <inheritdoc cref="Documentation.TypeNameMeansFullyQualifiedTypeName" path="/summary"/> of the type.</param>
        public TypeInfo Select_Type(
            Assembly assembly,
            string typeName)
        {
            var typeInfo = assembly.DefinedTypes
                // The full-name corresponds to our concept of type name.
                .Where(xType => xType.FullName == typeName)
                // Use first for speed (to avoid evaluating all types as required by single), but not first-or-default so that we throw if no type is found, since we know there should only be zero or one types.
                .First();

            return typeInfo;
        }
    }
}
