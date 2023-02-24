using System;


namespace R5T.F0018.F001
{
    public class AssemblyOperator : IAssemblyOperator
    {
        #region Infrastructure

        public static IAssemblyOperator Instance { get; } = new AssemblyOperator();


        private AssemblyOperator()
        {
        }

        #endregion
    }
}
