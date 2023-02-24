using System;

using R5T.T0132;
using R5T.T0161;


namespace R5T.F0018.F001
{
    [FunctionalityMarker]
    public partial interface ITypeOperator : IFunctionalityMarker
    {
        private static F0018.ITypeOperator StringlyTypedOperator => F0018.TypeOperator.Instance;


        /// <inheritdoc cref="F0018.ITypeOperator.Get_TypeName(Type)"/>
        public TypeName Get_TypeName(Type type)
        {
            var typeNameValue = StringlyTypedOperator.Get_TypeName(type);

            var typeName = new TypeName(typeNameValue);
            return typeName;
        }

        /// <inheritdoc cref="Get_TypeName(Type)"/>
        public TypeName Get_TypeNameOf<T>()
        {
            var type = typeof(T);

            var typeName = this.Get_TypeName(type);
            return typeName;
        }
    }
}
