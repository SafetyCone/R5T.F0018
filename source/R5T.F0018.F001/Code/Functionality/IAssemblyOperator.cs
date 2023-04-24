using System;
using System.Reflection;

using R5T.T0132;
using R5T.T0161;


namespace R5T.F0018.F001
{
    [FunctionalityMarker]
    public partial interface IAssemblyOperator : IFunctionalityMarker
    {
        private static F0018.IAssemblyOperator StringlyTypedOperator => F0018.AssemblyOperator.Instance;

        public TypeInfo Select_Type(
            Assembly assembly,
            ITypeName typeName)
        {
            var typeInfo = StringlyTypedOperator.Select_Type(
                assembly,
                typeName.Value);

            return typeInfo;
        }
    }
}
