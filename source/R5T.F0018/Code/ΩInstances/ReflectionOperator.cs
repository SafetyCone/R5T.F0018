﻿using System;


namespace R5T.F0018
{
    public class ReflectionOperator : IReflectionOperator
    {
        #region Infrastructure

        public static IReflectionOperator Instance { get; } = new ReflectionOperator();

        private ReflectionOperator()
        {
        }

        #endregion
    }
}
