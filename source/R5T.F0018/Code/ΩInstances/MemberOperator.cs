using System;


namespace R5T.F0018
{
    public class MemberOperator : IMemberOperator
    {
        #region Infrastructure

        public static IMemberOperator Instance { get; } = new MemberOperator();


        private MemberOperator()
        {
        }

        #endregion
    }
}
