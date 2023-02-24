using System;


namespace R5T.F0018
{
    /// <summary>
    /// Reflection-related functionality.
    /// </summary>
    public static class Documentation
    {
        /// <summary>
        /// type name (fully qualified type name)
        /// </summary>
        public static readonly object TypeNameMeansFullyQualifiedTypeName;

        /// <summary>
        /// method name (simple method name)
        /// </summary>
        /// <remarks>
        /// This is the confirmed behavior of <see cref="System.Reflection.MemberInfo.Name"/>.
        /// </remarks>
        public static readonly object MethodNameMeansSimpleMethodName;

        /// <summary>
        /// property name (simple method name)
        /// </summary>
        /// <remarks>
        /// This is the confirmed behavior of <see cref="System.Reflection.PropertyInfo.Name"/>.
        /// </remarks>
        public static readonly object PropertyNameMeansSimplePropertyName;
    }
}