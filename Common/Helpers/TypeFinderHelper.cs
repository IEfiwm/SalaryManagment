using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class TypeFinderHelper
    {
        public static IEnumerable<T> GetAllInstancesOf<T>()
        {
            var types = GetAllProjectAssemblies()
                                 .SelectMany(assembly => assembly.GetTypes())
                                 .Where(type => typeof(T).IsAssignableFrom(type) &&
                                                 !type.IsAbstract &&
                                                 !type.IsInterface &&
                                                 type.IsClass);

            return types.Select(type => (T)Activator.CreateInstance(type));
        }

        public static IEnumerable<T> GetAllInheritancesOf<T>() where T : class
        {
            var types = GetAllProjectAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null &&
                               type.BaseType == typeof(T) &&
                               type.IsSubclassOf(typeof(T)) &&
                               !type.IsAbstract &&
                               type.IsClass);

            return types.Select(type => (T)Activator.CreateInstance(type));
        }

        public static IEnumerable<Type> GetAllInheritanceTypesOf<T>() where T : class
        {
            return GetAllProjectAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType != null &&
                               //type.BaseType == typeof(T) &&
                               type.IsSubclassOf(typeof(T)) &&
                               !type.IsAbstract &&
                               type.IsClass);
        }

        public static IEnumerable<Type> GetAllInheritancesOf<T>(string assemblyNamePattern) where T : class
        {
            var assembly = GetAssemblyByPatternName(assemblyNamePattern);

            if (assembly == null)
            {
                return null;
            }

            return assembly
                .GetTypes()
                .Where(type => type.BaseType != null &&
                               type.BaseType == typeof(T) &&
                               type.IsSubclassOf(typeof(T)) &&
                               !type.IsAbstract &&
                               type.IsClass);
        }

        public static IEnumerable<Type> GetAllInheritancesOf(Type typeToSearch, IEnumerable<Type> excludeTypes, string assemblyNamePattern)
        {
            var assembly = GetAssemblyByPatternName(assemblyNamePattern);

            return assembly == null ? null :
                   assembly.GetTypes()
                           .Where(type => IsSubclassOf(type, typeToSearch))
                           .Except(excludeTypes);
        }

        public static IEnumerable<Type> GetAllInheritanceTypesOf<T>(Type excludeType) where T : class
        {
            return GetAllProjectAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type != excludeType &&
                               type.BaseType != null &&
                               type.BaseType == typeof(T) &&
                               type.IsSubclassOf(typeof(T)) &&
                               !type.IsAbstract &&
                               type.IsClass);
        }

        public static IEnumerable<Type> GetAllGenericTypesOf(Type genericType)
        {
            return GetAllProjectAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !string.IsNullOrEmpty(type.Namespace) &&
                               type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == genericType);
        }

        public static IEnumerable<Type> GetAllGenericTypesOf(Type genericType, Type excludeType)
        {
            return GetAllProjectAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type != excludeType &&
                               type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == genericType);
        }

        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAllProjectAssemblies()
                .SingleOrDefault(assembly => assembly.GetName().Name == assemblyName);
        }

        public static Type GetClassType(string className)
        {
            return GetAllProjectAssemblies().SelectMany(r => r.GetTypes()).FirstOrDefault(model => model.FullName.EndsWith("." + className));
        }

        public static Assembly GetAssemblyByPatternName(string patternName)
        {
            return GetAllProjectAssemblies()
                .FirstOrDefault(assembly => IsMatchesAssemblyName(assembly.GetName().Name, patternName));
        }

        public static IEnumerable<Assembly> GetAllAssemblies(string path)
        {
            var dllFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

            return dllFiles.Select(Assembly.LoadFile).ToList();
        }

        public static IEnumerable<Assembly> GetAllProjectAssemblies()
        {
            //if (_cachedProjectAssemblies == null)
            //{
            //    var binFolder = AppDomain.CurrentDomain.BaseDirectory + "bin\\";

            //    var assemblies = GetAllAssemblies(binFolder);

            //    assemblies = ExcludeUnnecessaryAssemblies(
            //        assemblies,
            //        GetUnnecessaryAssembliesName(),
            //        GetAssemblyRestrictToLoadingPattern())
            //        .ToList();

            //    _cachedProjectAssemblies = assemblies;
            //}

            //return _cachedProjectAssemblies;

            return ExcludeUnnecessaryAssemblies(
                AppDomain.CurrentDomain.GetAssemblies(),
                GetUnnecessaryAssembliesName(),
                GetAssemblyRestrictToLoadingPattern());
        }

        //public static void EnsureAllAssembliesAreLoaded()
        //{
        //    #region Logger

        //    var logFilePath = HostingEnvironment.MapPath("~/App_Data/AssemblyLoading.txt");

        //    File.AppendAllLines(logFilePath, new[] { DateTime.Now.ToString(CultureInfo.InvariantCulture) });

        //    #endregion

        //    //S.O. NOTE: ELIDED - ALL EXCEPTION HANDLING FOR BREVITY

        //    //get all .dll files from the specified path and load the lot

        //    //you might not want recursion - handy for localized assemblies
        //    //though especially.
        //    var dllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories);

        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        //    foreach (var dllFile in dllFiles)
        //    {
        //        //now get the name of the assembly you've found, without loading it
        //        //though (assuming .Net 2+ of course).
        //        if (dllFile.Contains("msvcr100.dll") || dllFile.Contains("SqlServerSpatial110.dll"))
        //        {
        //            continue;
        //        }

        //        if (dllFile.Contains("msvcr120.dll") || dllFile.Contains("SqlServerSpatial140.dll"))
        //        {
        //            continue;
        //        }

        //        //if(IsNativeAssembly(dllFile))
        //        //    continue;

        //        var assemblyName = AssemblyName.GetAssemblyName(dllFile);

        //        //sanity check - make sure we don't already have an assembly loaded
        //        //that, if this assembly name was passed to the loaded, would actually
        //        //be resolved as that assembly.  Might be unnecessary - but makes me
        //        //happy :)
        //        if (!assemblies.Any(assembly => AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
        //        {
        //            //crucial - USE THE ASSEMBLY NAME.
        //            //in a web app, this assembly will automatically be bound from the
        //            //Asp.Net Temporary folder from where the site actually runs.
        //            Assembly.Load(assemblyName);
        //        }
        //    }
        //}

        //public static void EnsureAllAssembliesAreLoadedTopDirectoryOnly()
        //{
        //    #region Logger

        //    var logFilePath = HttpContext.Current.Server.MapPath("~/App_Data/AssemblyLoading.txt");

        //    File.AppendAllLines(logFilePath, new[] { DateTime.Now.ToString() });

        //    #endregion

        //    //S.O. NOTE: ELIDED - ALL EXCEPTION HANDLING FOR BREVITY

        //    //get all .dll files from the specified path and load the lot

        //    //you might not want recursion - handy for localised assemblies
        //    //though especially.
        //    var dllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly);

        //    foreach (var dllFile in dllFiles)
        //    {
        //        //now get the name of the assembly you've found, without loading it
        //        //though (assuming .Net 2+ of course).
        //        var assemblyName = AssemblyName.GetAssemblyName(dllFile);
        //        //sanity check - make sure we don't already have an assembly loaded
        //        //that, if this assembly name was passed to the loaded, would actually
        //        //be resolved as that assembly.  Might be unnecessary - but makes me
        //        //happy :)
        //        if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly =>
        //          AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
        //        {
        //            //crucial - USE THE ASSEMBLY NAME.
        //            //in a web app, this assembly will automatically be bound from the
        //            //Asp.Net Temporary folder from where the site actually runs.
        //            Assembly.Load(assemblyName);
        //        }
        //    }
        //}

        private static bool IsMatchesAssemblyName(string assemblyFullName, string pattern)
        {
            return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        private static IEnumerable<Assembly> ExcludeUnnecessaryAssemblies(IEnumerable<Assembly> assemblies, string unnecessaryAssembliesName, string assemblyRestrictToLoadingPattern)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            return assemblies.Where(
                assembly => !IsMatchesAssemblyName(assembly.FullName, unnecessaryAssembliesName)
                            && IsMatchesAssemblyName(assembly.FullName, assemblyRestrictToLoadingPattern));
        }

        private static string GetUnnecessaryAssembliesName()
        {
            return "^Elmah|" +
                   "^FluentValidation|" +
                   "^System|" +
                   "^mscorlib|" +
                   "^Microsoft|" +
                   "^AjaxControlToolkit|" +
                   "^Antlr3|" +
                   "^Kendo|" +
                   "^Ionic.Zip|" +
                   "^CaptchaMvc|" +
                   "^Autofac|" +
                   "^StructureMap|" +
                   "^Ninject|" +
                   "^Unity|" +
                   "^Ioniz.Zip|" +
                   "^Owin|" +
                   "^AutoMapper|" +
                   "^Castle|" +
                   "^ComponentArt|" +
                   "^CppCodeProvider|" +
                   "^DotNetOpenAuth|" +
                   "^EntityFramework|" +
                   "^EPPlus|" +
                   "^FluentValidation|" +
                   "^ImageResizer|" +
                   "^itextsharp|" +
                   "^log4net|" +
                   "^MaxMind|" +
                   "^MbUnit|" +
                   "^MiniProfiler|" +
                   "^Mono.Math|" +
                   "^MvcContrib|" +
                   "^Newtonsoft|" +
                   "^NHibernate|" +
                   "^nunit|" +
                   "^Org.Mentalis|" +
                   "^PerlRegex|" +
                   "^QuickGraph|" +
                   "^Recaptcha|" +
                   "^Remotion|" +
                   "^RestSharp|" +
                   "^Rhino|" +
                   "^Telerik|" +
                   "^Iesi|" +
                   "^TestDriven|" +
                   "^TestFu|" +
                   "^UserAgentStringLibrary|" +
                   "^VJSharpCodeProvider|" +
                   "^WebActivator|" +
                   "^WebDev|" +
                   "^WebGrease|" +
                   "^AntiXssLibrary|" +
                   "^Canonicalize|" +
                   "^CodeFirstStoredProcs|" +
                   "^CryptSharp|" +
                   "^DevTrends.MvcDonutCaching|" +
                   "^HtmlSanitizationLibrary|" +
                   "^Lucene.Net|" +
                   "^WebMarkupMin|" +
                   "^SMDiagnostics|" +
                   "^DotNetZip|" +
                   "^Hangfire.Core|" +
                   "^Hangfire.SqlServer|" +
                   "^HtmlAgilityPack|" +
                   "^Anonymously Hosted DynamicMethods Assembly|" +
                   "^App_global.asax|" +
                   "^WebApi.OutputCache.V2|" +
                   "^WebApi.OutputCache.Core";
        }

        private static string GetAssemblyRestrictToLoadingPattern()
        {
            return ".*";
        }

        private static bool IsNativeAssembly(string assemblyFullName)
        {
            if (assemblyFullName.EndsWith("Microsoft.DiaSymReader.Native.amd64.dll"))
            {
                return true;
            }

            if (assemblyFullName.EndsWith("Microsoft.DiaSymReader.Native.x86.dll"))
            {
                return true;
            }

            if (assemblyFullName.EndsWith("Microsoft.CSharp.Core.dll"))
            {
                return true;
            }

            if (assemblyFullName.EndsWith("Microsoft.VisualBasic.Core.dll"))
            {
                return true;
            }

            return false;
        }

        private static bool IsSubclassOf(Type type, Type baseType)
        {
            if (type == null || baseType == null || type == baseType)
            {
                return false;
            }

            if (baseType.IsGenericType == false)
            {
                if (type.IsGenericType == false)
                {
                    return type.IsSubclassOf(baseType);
                }
            }
            else
            {
                baseType = baseType.GetGenericTypeDefinition();
            }

            type = type.BaseType;

            var objectType = typeof(object);

            while (type != objectType && type != null)
            {
                var curentType = type.IsGenericType
                    ? type.GetGenericTypeDefinition()
                    : type;

                if (curentType == baseType)
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }
    }
}
