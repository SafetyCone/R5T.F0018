using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IAssemblyOperator : IFunctionalityMarker
    {
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
