using System;
using System.Reflection;

using R5T.T0132;
using R5T.T0161;


namespace R5T.F0018.F001
{
    [FunctionalityMarker]
    public partial interface IReflectionOperations : IFunctionalityMarker
    {
        private static F0018.IReflectionOperations StringlyTypedOperations => F0018.ReflectionOperations.Instance;
    }
}
