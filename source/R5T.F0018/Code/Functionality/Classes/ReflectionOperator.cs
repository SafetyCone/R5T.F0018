using System;


namespace R5T.F0018
{
    public class ReflectionOperator : IReflectionOperator
    {
        #region Infrastructure

        public static ReflectionOperator Instance { get; } = new();

        private ReflectionOperator()
        {
        }

        #endregion
    }
}
