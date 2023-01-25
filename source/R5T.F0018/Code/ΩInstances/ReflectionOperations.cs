using System;


namespace R5T.F0018
{
    public class ReflectionOperations : IReflectionOperations
    {
        #region Infrastructure

        public static IReflectionOperations Instance { get; } = new ReflectionOperations();


        private ReflectionOperations()
        {
        }

        #endregion
    }
}
