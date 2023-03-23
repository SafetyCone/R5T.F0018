using System;


namespace R5T.F0018
{
    public class PropertyOperator : IPropertyOperator
    {
        #region Infrastructure

        public static IPropertyOperator Instance { get; } = new PropertyOperator();


        private PropertyOperator()
        {
        }

        #endregion
    }
}
