using System;


namespace R5T.F0018
{
    public class MethodOperator : IMethodOperator
    {
        #region Infrastructure

        public static IMethodOperator Instance { get; } = new MethodOperator();


        private MethodOperator()
        {
        }

        #endregion
    }
}
