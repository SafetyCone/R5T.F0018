using System;


namespace R5T.F0018
{
	public class TypeOperator : ITypeOperator
	{
		#region Infrastructure

	    public static ITypeOperator Instance { get; } = new TypeOperator();

	    private TypeOperator()
	    {
        }

	    #endregion
	}
}