#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Logging;
using Appalachia.Utility.Strings;
using Unity.Profiling;

#endregion

namespace Appalachia.Utility.Reflection.Extensions
{
    public static partial class ReflectionExtensions
    {
        #region Constants and Static Readonly

        private static readonly Dictionary<MethodInfo, ParameterInfo[]> _methodParams = new();

        #endregion

        #region Static Fields and Autoproperties

        private static Dictionary<MethodBase, string> _cache;
        private static Dictionary<string, List<MethodInfo>> _tempsMethods;

        #endregion

        public static MethodInfo GetBestMethod(IEnumerable<MethodInfo> candidates, Type returnType, params Type[] args)
        {
            using (_PRF_GetBestMethod.Auto())
            {
                ICollection<MethodInfo> candidatesCollection;

                if (candidates is ICollection<MethodInfo> collection)
                {
                    candidatesCollection = collection;
                }
                else
                {
                    candidatesCollection = candidates.ToArray();
                }

                foreach (var method in candidatesCollection)
                {
                    using (_PRF_GetBestMethod_ReturnTypeCheck.Auto())
                    {
                        if ((returnType != null) && (method.ReturnType == typeof(void)))
                        {
                            continue;
                        }
                    }

                    ParameterInfo[] parameters;

                    using (_PRF_GetBestMethod_GetParameters.Auto())
                    {
                        if (!_methodParams.ContainsKey(method))
                        {
                            using (_PRF_GetBestMethod_GetParameters_GetFromMethod.Auto())
                            {
                                parameters = method.GetParameters();
                                _methodParams.Add(method, parameters);
                            }
                        }
                        else
                        {
                            using (_PRF_GetBestMethod_GetParameters_GetFromCache.Auto())
                            {
                                parameters = _methodParams[method];
                            }
                        }
                    }

                    using (_PRF_GetBestMethod_LengthCheck.Auto())
                    {
                        var total = parameters.Length;
                        var provided = args.Length;

                        if (total != provided)
                        {
                            continue;
                        }
                    }

                    using (_PRF_GetBestMethod_ParameterTypeCheck.Auto())
                    {
                        for (var j = 0; j < parameters.Length; j++)
                        {
                            var parameterType = parameters[j].ParameterType;
                            var argType = args[j];

                            if (!parameterType.IsAssignableFrom(argType))
                            {
                                throw new NotSupportedException(nameof(argType));
                            }
                        }
                    }

                    return method;
                }

                var first = candidatesCollection.FirstOrDefault();

                ThrowMissingMethodException(
                    first?.DeclaringType,
                    first?.Name,
                    BindingFlags.Default,
                    candidatesCollection.Select(m => m.Name)
                );

                return default;
            }
        }

        public static List<MethodInfo> GetMethods_CACHE(this Type t)
        {
            using (_PRF_GetMethods_CACHE.Auto())
            {
                return t.GetMethods_CACHE(BindingFlags.Default);
            }
        }

        public static List<MethodInfo> GetMethods_CACHE(this Type t, BindingFlags flags)
        {
            using (_PRF_GetMethods_CACHE.Auto())
            {
                if (_METHOD_CACHE_BASIC.ContainsKey(t) && _METHOD_CACHE_BASIC[t].TryGetValue(flags, out var result))
                {
                    return result;
                }

                PopulateMethods_INTERNAL(t, flags);

                return _METHOD_CACHE_BASIC[t][flags];
            }
        }

        public static List<MethodInfo> GetMethods_CACHE(Type t, string methodName, BindingFlags flags)
        {
            using (_PRF_GetMethods_CACHE.Auto())
            {
                if (_METHOD_CACHE.ContainsKey(t) &&
                    _METHOD_CACHE[t].ContainsKey(flags) &&
                    _METHOD_CACHE[t][flags].ContainsKey(methodName))
                {
                    return _METHOD_CACHE[t][flags][methodName];
                }

                PopulateMethods_INTERNAL(t, flags);

                var targets = _METHOD_CACHE[t][flags];

                if (!targets.ContainsKey(methodName))
                {
                    ThrowMissingMethodException(t, methodName, flags, targets.Keys);
                }

                return targets[methodName];
            }
        }

        public static string GetParamsNames(this MethodBase method)
        {
            var parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (var length = parameterInfoArray.Length; index < length; ++index)
            {
                var parameterInfo = parameterInfoArray[index];
                var niceName = parameterInfo.ParameterType.GetReadableName();
                stringBuilder.Append(niceName);
                stringBuilder.Append(" ");
                stringBuilder.Append(parameterInfo.Name);
                if (index < (length - 1))
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        public static string GetReadableName(this MethodBase method, string extensionMethodPrefix)
        {
            if (_cache == null)
            {
                _cache = new Dictionary<MethodBase, string>();
            }

            if (_cache.TryGetValue(method, out var result))
            {
                return result;
            }

            var stringBuilder = new StringBuilder();
            if (method.IsExtensionMethod())
            {
                stringBuilder.Append(extensionMethodPrefix);
            }

            stringBuilder.Append(method.Name);
            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                stringBuilder.Append("<");
                for (var index = 0; index < genericArguments.Length; ++index)
                {
                    if (index != 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(genericArguments[index].GetReadableName());
                }

                stringBuilder.Append(">");
            }

            stringBuilder.Append("(");
            stringBuilder.Append(method.GetParamsNames());
            stringBuilder.Append(")");
            result = stringBuilder.ToString();

            _cache.Add(method, result);

            return result;
        }

        public static string GetReadableName(this MethodBase method)
        {
            return method.GetReadableName("[ext] ");
        }

        public static bool HasParameters(this MethodInfo methodInfo, IList<Type> paramTypes, bool inherit = true)
        {
            var parameters = methodInfo.GetParameters();
            if (parameters.Length != paramTypes.Count)
            {
                return false;
            }

            return !parameters.Where(
                                   (t, index) => (inherit && !paramTypes[index].InheritsFrom(t.ParameterType)) ||
                                                 (t.ParameterType != paramTypes[index])
                               )
                              .Any();
        }

        public static bool IsExtensionMethod(this MethodBase method)
        {
            var declaringType = method.DeclaringType;
            return (declaringType != null) &&
                   declaringType.IsSealed &&
                   !declaringType.IsGenericType &&
                   !declaringType.IsNested &&
                   method.IsDefined(typeof(ExtensionAttribute), false);
        }

        public static MethodInfo PrepareAndGetBestMethod(
            this Type t,
            string methodName,
            BindingFlags flags,
            Type returnType,
            params Type[] args)
        {
            using (_PRF_PrepareAndGetBestMethod.Auto())
            {
                var results = GetMethods_CACHE(t, methodName, flags);
                return GetBestMethod(results, returnType, args);
            }
        }

        private static void ThrowMissingMethodException(
            Type t,
            string methodName,
            BindingFlags flags,
            IEnumerable<string> methodCandidates)
        {
            using (_PRF_ThrowMissingMethodException.Auto())
            {
                var possibilities = string.Join(", ", methodCandidates.Select(m => m.FormatMethodForLogging()));
                var flagString = flags.ToString();

                var outputMessage = ZString.Format(
                    "Method {0} could not be found on {1} using flags {2}. Methods available: {3}.",
                    methodName.FormatMethodForLogging(),
                    t.FormatForLogging(),
                    flagString.FormatConstantForLogging(),
                    possibilities
                );

                AppaLog.Context.Extensions.Error(outputMessage);

                throw new MissingMethodException(t.Name, methodName);
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_ThrowMissingMethodException =
            new ProfilerMarker(_PRF_PFX + nameof(ThrowMissingMethodException));

        private static readonly ProfilerMarker _PRF_GetBestMethod = new(_PRF_PFX + nameof(GetBestMethod));

        private static readonly ProfilerMarker _PRF_GetBestMethod_ReturnTypeCheck =
            new(_PRF_PFX + nameof(GetBestMethod) + ".ReturnTypeCheck");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters =
            new(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters_GetFromMethod =
            new(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters.GetFromMethod");

        private static readonly ProfilerMarker _PRF_GetBestMethod_GetParameters_GetFromCache =
            new(_PRF_PFX + nameof(GetBestMethod) + ".GetParameters.GetFromCache");

        private static readonly ProfilerMarker _PRF_GetBestMethod_LengthCheck =
            new(_PRF_PFX + nameof(GetBestMethod) + ".LengthCheck");

        private static readonly ProfilerMarker _PRF_GetBestMethod_ParameterTypeCheck =
            new(_PRF_PFX + nameof(GetBestMethod) + ".ParameterTypeCheck");

        private static readonly ProfilerMarker _PRF_PrepareAndGetBestMethod =
            new(_PRF_PFX + nameof(PrepareAndGetBestMethod));

        private static readonly ProfilerMarker _PRF_GetMethods_CACHE =
            new ProfilerMarker(_PRF_PFX + nameof(GetMethods_CACHE));

        #endregion
    }
}
