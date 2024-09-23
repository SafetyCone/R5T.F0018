using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;


namespace R5T.F0018
{
    [FunctionalityMarker]
    public partial interface IPropertyOperator : IFunctionalityMarker
    {
        public bool IsObsolete(PropertyInfo propertyInfo)
        {
            var output = Instances.MemberOperator.Is_Obsolete(propertyInfo);
            return output;
        }

        public bool Has_GetMethod(
            PropertyInfo propertyInfo,
            out MethodInfo getMethod)
        {
            getMethod = propertyInfo.GetMethod;

            var output = Instances.NullOperator.Is_NotNull(getMethod);
            return output;
        }

        public bool Has_GetMethod(PropertyInfo propertyInfo)
            => this.Has_GetMethod(
                propertyInfo,
                out _);

        public bool Has_Public_GetMethod(PropertyInfo propertyInfo)
        {
            var has_GetMethod = this.Has_GetMethod(
                propertyInfo,
                out var getMethod);

            if(!has_GetMethod)
            {
                return false;
            }

            var output = Instances.MemberInfoOperator.Is_Public(getMethod);
            return output;
        }

        public bool Has_SetMethod(
            PropertyInfo propertyInfo,
            out MethodInfo setMethod)
        {
            setMethod = propertyInfo.SetMethod;

            var output = Instances.NullOperator.Is_NotNull(setMethod);
            return output;
        }

        public bool Has_SetMethod(PropertyInfo propertyInfo)
            => this.Has_SetMethod(
                propertyInfo,
                out _);

        public bool Is_Without_SetMethod(PropertyInfo propertyInfo)
            => !this.Has_SetMethod(propertyInfo);

        /// <summary>
        /// Is the property not an indexer property?
        /// </summary>
        public bool Is_Not_Indexer(PropertyInfo propertyInfo)
        {
            // Implemented by testing whether the property has any index parameters.
            var output = !this.Is_Indexer(propertyInfo);
            return output;
        }

        /// <summary>
        /// Is the property an indexer property?
        /// </summary>
        public bool Is_Indexer(PropertyInfo propertyInfo)
        {
            var output = propertyInfo.GetIndexParameters().Any();
            return output;
        }

        /// <summary>
        /// True if:
        /// <list type="number">
        /// <item>The property has a public get-method.</item>
        /// <item>The property does not have a set-method.</item>
        /// <item>The property is not an indexer.</item>
        /// </list>
        /// </summary>
        public bool Is_ValueProperty(PropertyInfo propertyInfo)
        {
            var output = true
                // Only properties with public get methods.
                && this.Has_Public_GetMethod(propertyInfo)
                // Only properties *without* set methods.
                && this.Is_Without_SetMethod(propertyInfo)
                // Only properties that are *not* indexers (which is tested by seeing if the property has any index parameters).
                && this.Is_Not_Indexer(propertyInfo)
                ;

            return output;
        }
    }
}
