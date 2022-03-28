using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace Appalachia.Utility.Strings
{
    public static class NameFormatter
    {
        #region Static Fields and Autoproperties

        private static bool _hasNicifyBuilder;

        private static Dictionary<string, string> _nicifyCache;
        private static Utf16ValueStringBuilder _nicifyBuilder;

        #endregion

        public static string Nicify(this Type variableType)
        {
            return Nicify(variableType.Name);
        }

        public static string Nicify<T>(this T variableName)
            where T : Enum
        {
            return Nicify(variableName.ToString());
        }

        /// <summary>
        ///     Make a displayable name for a variable.
        ///     This function will insert spaces before capital letters and remove optional m_, _ or k followed by uppercase letter in front of the name.
        /// </summary>
        /// <param name="variableName">The variable name.</param>
        /// <returns>The displayable name.</returns>
        public static string Nicify(this string variableName)
        {
            using (_PRF_NicifyVariableName.Auto())
            {
                _nicifyCache ??= new();

                if (!_hasNicifyBuilder)
                {
                    _nicifyBuilder = new Utf16ValueStringBuilder(false);
                    _hasNicifyBuilder = true;
                }

                string result;

                if (_nicifyCache.TryGetValue(variableName, out result))
                {
                    return result;
                }

                var length = variableName.Length;

                if (length <= 2)
                {
                    return variableName;
                }

                _nicifyBuilder.Clear();
                var modified = false;
                var addedFirst = false;

                for (var i = 0; i < length; i++)
                {
                    var currentCharacter = variableName[i];

                    if (i == 0)
                    {
                        if (currentCharacter == 'm')
                        {
                            var nextCharacter = variableName[1];

                            if (nextCharacter == '_')
                            {
                                i = 1;
                                modified = true;
                                continue;
                            }
                        }
                        else if (currentCharacter == 'k')
                        {
                            if (char.IsUpper(variableName, 1))
                            {
                                modified = true;
                                continue;
                            }
                        }
                        else if (currentCharacter == '_')
                        {
                            modified = true;
                            continue;
                        }

                        if (char.IsLower(currentCharacter))
                        {
                            var upperCharacter = char.ToUpperInvariant(currentCharacter);
                            _nicifyBuilder.Append(upperCharacter);
                            modified = true;
                        }
                        else
                        {
                            _nicifyBuilder.Append(currentCharacter);
                        }

                        addedFirst = true;
                    }
                    else
                    {
                        var previousCharacter = variableName[i - 1];

                        var currentCharacterIsLower = char.IsLower(currentCharacter);
                        var currentCharacterIsUpper = char.IsUpper(currentCharacter);
                        var previousCharacterIsLower = char.IsLower(previousCharacter);
                        var previousCharacterIsUpper = char.IsUpper(previousCharacter);

                        if (currentCharacter == '_')
                        {
                            if (previousCharacter != '_')
                            {
                                _nicifyBuilder.Append(' ');
                                addedFirst = true;
                                modified = true;
                            }

                            continue;
                        }

                        var upperCharacter = char.ToUpperInvariant(currentCharacter);

                        if ((previousCharacter == '_') && currentCharacterIsLower)
                        {
                            _nicifyBuilder.Append(upperCharacter);
                            addedFirst = true;
                            modified = true;
                            continue;
                        }

                        if (previousCharacterIsLower && currentCharacterIsUpper)
                        {
                            _nicifyBuilder.Append(' ');
                            _nicifyBuilder.Append(upperCharacter);
                            addedFirst = true;
                            modified = true;
                            continue;
                        }

                        if (!addedFirst)
                        {
                            _nicifyBuilder.Append(upperCharacter);
                            addedFirst = true;
                            modified = true;
                            continue;
                        }

                        _nicifyBuilder.Append(currentCharacter);
                        addedFirst = true;
                    }
                }

                if (!modified)
                {
                    _nicifyCache.Add(variableName, variableName);
                    return variableName;
                }

                result = _nicifyBuilder.ToString();
                _nicifyCache.Add(variableName, result);

                return result;
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(NameFormatter) + ".";

        private static readonly ProfilerMarker _PRF_NicifyVariableName = new ProfilerMarker(_PRF_PFX + nameof(Nicify));

        #endregion
    }
}
