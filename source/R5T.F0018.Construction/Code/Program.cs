using System;


namespace R5T.F0018.Construction
{
    static class Program
    {
        static void Main(string[] args)
        {
            ReflectionOperator.Instance.InAssemblyContext(
                @"C:\Temp\Publish\D8S.S0001\D8S.S0001.exe",
                assembly =>
                {
                    F0000.AssemblyOperator.Instance.ForAllTypes(
                        assembly,
                        type =>
                        {
                            Console.WriteLine(type.FullName);
                        });
                });
        }
    }
}