using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using R5T.T0132;

using Glossary = R5T.Y0000.Glossary;


namespace R5T.F0018
{
    [DraftFunctionalityMarker]
    public interface IReflectionOperator : IDraftFunctionalityMarker
    {
        /// <summary>
        /// Can use <inheritdoc cref="Glossary.ForOutput.OutputByClosure" path="/name"/> to return outputs from the assembly action.
        /// </summary>
        public async Task InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<Func<Assembly, Task>> assemblyActions)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            foreach (var assemblyAction in assemblyActions)
            {
                await assemblyAction(assembly);
            }
        }

        public async Task InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<string> otherAssemblyDirectoryFilePaths,
            IEnumerable<Func<Assembly, Task>> assemblyActions)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(Instances.EnumerableOperator.From(assemblyDirectoryPath)
                .Append(otherAssemblyDirectoryFilePaths));

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            foreach (var assemblyAction in assemblyActions)
            {
                await assemblyAction(assembly);
            }
        }

        /// <inheritdoc cref="InAssemblyContext(string, IEnumerable{Func{Assembly, Task}})"/>
        public Task InAssemblyContext(
            string assemblyFilePath,
            Func<Assembly, Task> assemblyAction)
        {
            return InAssemblyContext(
                assemblyFilePath,
                Instances.EnumerableOperator.From(assemblyAction));
        }

        /// <inheritdoc cref="InAssemblyContext(string, IEnumerable{Func{Assembly, Task}})"/>
        public Task InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<string> otherAssemblyDirectoryPaths,
            Func<Assembly, Task> assemblyAction)
        {
            return InAssemblyContext(
                assemblyFilePath,
                otherAssemblyDirectoryPaths,
                Instances.EnumerableOperator.From(assemblyAction));
        }

        /// <inheritdoc cref="InAssemblyContext(string, IEnumerable{Func{Assembly, Task}})"/>
        public void InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<Action<Assembly>> assemblyActions)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            foreach (var assemblyAction in assemblyActions)
            {
                assemblyAction(assembly);
            }
        }

        public void InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<string> otherAssemblyDirectoryFilePaths,
            IEnumerable<Action<Assembly>> assemblyActions)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(Instances.EnumerableOperator.From(assemblyDirectoryPath)
                .AppendRange(otherAssemblyDirectoryFilePaths)
                // TODO: 
                );

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            foreach (var assemblyAction in assemblyActions)
            {
                assemblyAction(assembly);
            }
        }

        /// <inheritdoc cref="InAssemblyContext(string, IEnumerable{Func{Assembly, Task}})"/>
        public void InAssemblyContext(
            string assemblyFilePath,
            Action<Assembly> assemblyAction)
        {
            InAssemblyContext(
                assemblyFilePath,
                Instances.EnumerableOperator.From(assemblyAction));
        }

        /// <inheritdoc cref="InAssemblyContext(string, IEnumerable{Func{Assembly, Task}})"/>
        public void InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<string> otherAssemblyDirectoryFilePaths,
            Action<Assembly> assemblyAction)
        {
            InAssemblyContext(
                assemblyFilePath,
                otherAssemblyDirectoryFilePaths,
                Instances.EnumerableOperator.From(assemblyAction));
        }

        public string[] GetDirectoryDllFilePaths(string directoryPath)
        {
            var output = Directory.GetFiles(directoryPath, "*.dll");
            return output;
        }

        public PathAssemblyResolver GetPathAssemblyResolver_FromDirectoryPaths(
            IEnumerable<string> assemblyDirectoryPaths)
        {
            var assemblyFilePaths = assemblyDirectoryPaths
                .SelectMany(assemblyDirectoryPath =>
                    GetDirectoryDllFilePaths(assemblyDirectoryPath))
                ;

            var resolver = new PathAssemblyResolver(assemblyFilePaths);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver_FromDirectoryPaths(params string[] assemblyDirectoryPaths)
        {
            var output = GetPathAssemblyResolver_FromDirectoryPaths(assemblyDirectoryPaths.AsEnumerable());
            return output;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            IEnumerable<string> assemblyDirectoryPaths)
        {
            var runtimeDirectoryPath = RuntimeEnvironment.GetRuntimeDirectory();

            var assemblyFilePaths = assemblyDirectoryPaths
                .Append(runtimeDirectoryPath)
                .SelectMany(assemblyDirectoryPath =>
                    GetDirectoryDllFilePaths(assemblyDirectoryPath))
                ;

            var resolver = new PathAssemblyResolver(assemblyFilePaths);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            string assemblyDirectoryPath,
            string runtimeDirectoryPath)
        {
            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath, runtimeDirectoryPath);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            string assemblyDirectoryPath)
        {
            var runtimeDirectoryPath = RuntimeEnvironment.GetRuntimeDirectory();

            var output = GetPathAssemblyResolver(
                assemblyDirectoryPath,
                runtimeDirectoryPath);

            return output;
        }

        public T InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, T> assemblyFunction)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var output = assemblyFunction(assembly);
            return output;
        }

        public Task<T> InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, Task<T>> assemblyFunction)
        {
            var assemblyDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var output = assemblyFunction(assembly);
            return output;
        }
    }
}
