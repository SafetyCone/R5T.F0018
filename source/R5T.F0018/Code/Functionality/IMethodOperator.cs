using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IMethodOperator : IFunctionalityMarker
    {
        public bool Is_FunctionMethod(MethodInfo methodInfo)
        {
            var output = true
                // Only public methods.
                && methodInfo.IsPublic
                // Must not be a property.
                && !Instances.MethodOperator.Is_PropertyMethod(methodInfo)
                ;

            return output;
        }

        public bool Is_Obsolete(MethodInfo methodInfo)
        {
            var output = Instances.MemberOperator.Is_Obsolete(methodInfo);
            return output;
        }

        /// <summary>
        /// Determines whether the method is a property get or set method.
        /// </summary>
        public bool Is_PropertyMethod(MethodInfo methodInfo)
        {
            // There is no direct method to determine if a method is a property method.
            // This implemention gets the properties of the method's declaring type, and then tests if the method is one of the get- or set-mmethods of any of the properties.

            var output = true
                // All property methods have special names.
                && methodInfo.IsSpecialName
                // Among all the properties of the method's declaring type, is the current method a get- or set-method of a property?
                && methodInfo.DeclaringType.GetProperties()
                    .Any(property => false
                        || property.GetGetMethod() == methodInfo
                        || property.GetSetMethod() == methodInfo);

            return output;
        }
    }
}
