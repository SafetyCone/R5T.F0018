using System;


namespace R5T.F0018
{
    public class FieldOperator : IFieldOperator
    {
        #region Infrastructure

        public static IFieldOperator Instance { get; } = new FieldOperator();


        private FieldOperator()
        {
        }

        #endregion
    }
}
