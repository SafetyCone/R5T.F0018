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
        public IEnumerable<MethodInfo>Get_Methods(TypeInfo typeInfo)
        {
            return typeInfo.DeclaredMethods;
        }

        public IEnumerable<PropertyInfo> Get_Properties(TypeInfo typeInfo)
        {
            return typeInfo.DeclaredProperties;
        }

        public IEnumerable<FieldInfo> Get_Fields_StaticReadonly_Object(TypeInfo typeInfo)
        {
            var output = typeInfo.DeclaredFields
                .Where(field =>
                {
                    var isStatic = field.IsStatic;
                    var isReadonly = field.IsInitOnly;

                    var isObject = field.FieldType.FullName == "System.Object";

                    var output = true
                        && isStatic
                        && isReadonly
                        && isObject
                        ;

                    return output;
                })
                ;

            return output;
        }

        /// <summary>
        /// Can use <inheritdoc cref="Glossary.ForOutput.OutputByClosure" path="/name"/> to return outputs from the assembly action.
        /// </summary>
        public async Task InAssemblyContext(
            string assemblyFilePath,
            IEnumerable<Func<Assembly, Task>> assemblyActions)
        {
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

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
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver_AddRuntimeDirectoryPath(Instances.EnumerableOperator.From(assemblyDirectoryPath)
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
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = this.GetPathAssemblyResolver(assemblyDirectoryPath);

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
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver_AddRuntimeDirectoryPath(Instances.EnumerableOperator.From(assemblyDirectoryPath)
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
            this.InAssemblyContext(
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

        /// <summary>
        /// Gets the path to the directory containing the assemblies (DLLs) of the currently executing runtime.
        /// </summary>
        public string GetExecutingRuntimeDirectoryPath()
        {
            var runtimeDirectoryPath = RuntimeEnvironment.GetRuntimeDirectory();
            return runtimeDirectoryPath;
        }

        public IEnumerable<string> FilterDuplicates_ByFileName(
            IEnumerable<string> assemblyFilePaths)
        {
            var duplicates = assemblyFilePaths
                .GroupBy(filePath => Instances.PathOperator.Get_FileName(filePath))
                .Where(group => group.Count() > 1)
                .Select(group => group.First())
                .Now();

            var nonDuplicateAssemblyFilePaths = assemblyFilePaths
                // Remove duplicates (all instances of anything with a duplicate), leaving only non-dupicates.
                .Except(duplicates)
                // Then add back each duplicate.
                .Append(duplicates);

            return nonDuplicateAssemblyFilePaths;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            IEnumerable<string> assemblyDirectoryPaths)
        {
            // First get *all* assembly file paths.
            var assemblyFilePaths = assemblyDirectoryPaths
                .SelectMany(assemblyDirectoryPath =>
                    this.GetDirectoryDllFilePaths(assemblyDirectoryPath))
                // Only add the distinct.
                .Distinct()
                ;

            // Then filter out any duplicates.
            var nonDuplicateAssemblyFilePaths = this.FilterDuplicates_ByFileName(assemblyFilePaths);

            // Now get the path assembly resolver.
            var resolver = new PathAssemblyResolver(nonDuplicateAssemblyFilePaths);
            return resolver;
        }

        
        public PathAssemblyResolver GetPathAssemblyResolver_WithoutDuplicateFiltering(
            IEnumerable<string> assemblyDirectoryPaths)
        {
            var assemblyFilePaths = assemblyDirectoryPaths
                .SelectMany(assemblyDirectoryPath =>
                    this.GetDirectoryDllFilePaths(assemblyDirectoryPath))
                ;

            var resolver = new PathAssemblyResolver(assemblyFilePaths);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            params string[] assemblyDirectoryPaths)
        {
            var resolver = this.GetPathAssemblyResolver(
                assemblyDirectoryPaths.AsEnumerable());

            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver_AddRuntimeDirectoryPath(
            IEnumerable<string> assemblyDirectoryPaths)
        {
            var runtimeDirectoryPath = RuntimeEnvironment.GetRuntimeDirectory();

            var assemblyFilePaths = assemblyDirectoryPaths
                .Append(runtimeDirectoryPath)
                .SelectMany(assemblyDirectoryPath =>
                    this.GetDirectoryDllFilePaths(assemblyDirectoryPath))
                ;

            var resolver = new PathAssemblyResolver(assemblyFilePaths);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            string assemblyDirectoryPath,
            string runtimeDirectoryPath)
        {
            var assemblyDirectoryPaths = new[]
            {
                assemblyDirectoryPath,
                runtimeDirectoryPath,
            };

            var resolver = this.GetPathAssemblyResolver(assemblyDirectoryPaths);
            return resolver;
        }

        public PathAssemblyResolver GetPathAssemblyResolver(
            string assemblyDirectoryPath,
            bool addRuntimeDirectoryPath = false)
        {
            var runtimeDirectoryPath = RuntimeEnvironment.GetRuntimeDirectory();

            var output = addRuntimeDirectoryPath
                ? this.GetPathAssemblyResolver(
                    assemblyDirectoryPath,
                    runtimeDirectoryPath)
                : this.GetPathAssemblyResolver( new[] { assemblyDirectoryPath })
                ;

            return output;
        }

        public T InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, T> assemblyFunction)
        {
            //// Cannot use ReflectionOnlyLoad, since it is unsupported since.NET Core... LAME!
            //var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyFilePath);

            // Use metadata load context instead.
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = this.GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var output = assemblyFunction(assembly);
            return output;
        }

        public T InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, T> assemblyFunction,
            IEnumerable<string> runtimeDirectoryPaths)
        {
            // Use metadata load context instead.
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = this.GetPathAssemblyResolver(
                runtimeDirectoryPaths
                    .Append(assemblyDirectoryPath));

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var output = assemblyFunction(assembly);
            return output;
        }

        public T InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, T> assemblyFunction,
            params string[] runtimeDirectoryPaths)
        {
            var output = this.InAssemblyContext<T>(
                assemblyFilePath,
                assemblyFunction,
                runtimeDirectoryPaths.AsEnumerable());

            return output;
        }

        public Task<T> InAssemblyContext<T>(
            string assemblyFilePath,
            Func<Assembly, Task<T>> assemblyFunction)
        {
            var assemblyDirectoryPath = Instances.PathOperator.Get_ParentDirectoryPath_ForFile(assemblyFilePath);

            var resolver = GetPathAssemblyResolver(assemblyDirectoryPath);

            using var metadataContext = new MetadataLoadContext(resolver);

            var assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var output = assemblyFunction(assembly);
            return output;
        }

        public bool Is_FunctionMethod(MethodInfo methodInfo)
            => Instances.MethodOperator.Is_FunctionMethod(methodInfo);

        public bool Is_ValueProperty(PropertyInfo propertyInfo)
            => Instances.PropertyOperator.Is_ValueProperty(propertyInfo);
    }
}
