using System;


namespace R5T.F0018
{
    public class TypeNames : ITypeNames
    {
        #region Infrastructure

        public static ITypeNames Instance { get; } = new TypeNames();


        private TypeNames()
        {
        }

        #endregion
    }
}
