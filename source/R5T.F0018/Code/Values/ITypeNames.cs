using System;

using R5T.T0131;


namespace R5T.F0018
{
    [ValuesMarker]
    public partial interface ITypeNames : IValuesMarker
    {
        public string ObsoleteAttribute => "System.ObsoleteAttribute";
    }
}
