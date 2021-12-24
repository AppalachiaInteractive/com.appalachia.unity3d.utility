#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Appalachia.Utility.Extensions.Cleaning;
using Appalachia.Utility.Strings;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static class StringExtensions
    {
        public enum EncodingPrefix
        {
            UnicodeDefault,
            Underscore
        }

        #region Static Fields and Autoproperties

        private static char[] _npmPackageTrims =
        {
            '/', '\\', '.', ',', '"', '\'', ' ', '\t', '\n', '\r', '\0'
        };

        private static Dictionary<char, string> _encodingReplacements;
        private static Dictionary<EncodingPrefix, string> _encodingPrefixes;
        private static string[] _npmPackageRemovals = { ".tgz", ".gz", ".tar", ".json", ".zip" };
        private static string[] _packagePaths;

        private static StringCleanerWithContext<char[]> _extensionCleaner;

        #endregion
        public static string CleanExtension(this string extension)
        {
            using (_PRF_CleanExtension.Auto())
            {
                if (_extensionCleaner == null)
                {
                    _extensionCleaner = new StringCleanerWithContext<char[]>(
                        new[] { '.', ' ', '\t', ',' },
                        (cleaner, value) =>
                        {
                            var result = value.ToLowerInvariant().Trim(cleaner.context1);
                            return result;
                        }
                    );
                }

                return _extensionCleaner.Clean(extension);
            }
        }

        public static bool Contains(this string source, string toCheck, StringComparison comparisonType)
        {
            return source.IndexOf(toCheck, comparisonType) >= 0;
        }

        public static bool Contains(this string str, char ch)
        {
            using (_PRF_Contains.Auto())
            {
                if (str == null)
                {
                    return false;
                }

                return str.IndexOf(ch) != -1;
            }
        }

        public static void CopyToClipboard(this string s)
        {
            using (_PRF_CopyToClipboard.Auto())
            {
                var te = new TextEditor { text = s };
                te.SelectAll();
                te.Copy();
            }
        }

        public static string Cut(this string s, int chars)
        {
            using (_PRF_Cut.Auto())
            {
                var length = Mathf.Clamp(s.Length - chars, 0, s.Length);

                return s.Substring(0, length);
            }
        }

        public static StringBuilder Cut(this StringBuilder s, int chars)
        {
            using (_PRF_Cut.Auto())
            {
                var length = s.Length;
                var targetLength = length - chars;

                targetLength = Mathf.Clamp(targetLength, 0, length);

                return s.Remove(targetLength - 1, chars);
            }
        }

        public static string EncodeUnicodePathToASCII(this string path, EncodingPrefix prefix)
        {
            using (_PRF_EncodeUnicodePathToASCII.Auto())
            {
                SetupEncodingReplacements();

                var prefixString = _encodingPrefixes[prefix];

                var builder = new StringBuilder();

                for (var i = 0; i < path.Length; i++)
                {
                    var character = path[i];

                    if (_encodingReplacements.ContainsKey(character))
                    {
                        var replacementSuffix = _encodingReplacements[character];
                        var replacement = ZString.Format("{0}{1}", prefixString, replacementSuffix);

                        builder.Append(replacement);
                    }
                    else
                    {
                        builder.Append(character);
                    }
                }

                return builder.ToString();
            }
        }

        public static GameObject FindGameObjectByPath(string absolutePath)
        {
            using (_PRF_FindGameObjectByPath.Auto())
            {
                for (var i = 0; i < SceneManager.sceneCount; ++i)
                {
                    var scene = SceneManager.GetSceneAt(i);

                    var gameObj = scene.FindGameObjectByPath(absolutePath);
                    if (gameObj)
                    {
                        return gameObj;
                    }
                }

                return null;
            }
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            using (_PRF_IsNotNullOrEmpty.Auto())
            {
                return !string.IsNullOrEmpty(str);
            }
        }

        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            using (_PRF_IsNotNullOrWhiteSpace.Auto())
            {
                return !string.IsNullOrWhiteSpace(str);
            }
        }

        public static bool IsNullOrEmpty(this string str)
        {
            using (_PRF_IsNullOrEmpty.Auto())
            {
                return string.IsNullOrEmpty(str);
            }
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            using (_PRF_IsNullOrWhiteSpace.Auto())
            {
                return string.IsNullOrWhiteSpace(str);
            }
        }

        public static bool IsPackagePath(this string path)
        {
            using (_PRF_IsPackagePath.Auto())
            {
                InitializePackagePathReplacements();

                for (var i = 0; i < _packagePaths.Length; i++)
                {
                    var packagePath = _packagePaths[i];

                    if (path.Contains(packagePath))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static string LinuxToWindowsPath(this string path)
        {
            using (_PRF_LinuxToWindowsPath.Auto())
            {
                if (Path.IsPathRooted(path))
                {
                    var builder = new StringBuilder(path);

                    // /c/Program Files
                    // C:\Program Files

                    builder[0] = char.ToUpperInvariant(builder[1]);
                    builder[1] = ':';
                    path = builder.ToString();
                }

                return path.Replace("/", "\\");
            }
        }

        public static string ParseNpmPackageVersion(this string path)
        {
            using (_PRF_ParseNpmPackageVersion.Auto())
            {
                if (path == null)
                {
                    return null;
                }

                var lastIndex0 = path.LastIndexOf(":", StringComparison.Ordinal);
                var lastIndex1 = path.LastIndexOf("@", StringComparison.Ordinal);
                var lastIndex2 = path.LastIndexOf("-", StringComparison.Ordinal);

                var maxIndex = Mathf.Max(lastIndex0, lastIndex1, lastIndex2);

                if (maxIndex < 0)
                {
                    return null;
                }

                var substring = path.Substring(maxIndex + 1);

                var removed = substring;
                for (var i = 0; i < _npmPackageRemovals.Length; i++)
                {
                    var remove = _npmPackageRemovals[i];

                    removed = removed.Replace(remove, string.Empty);
                }

                var final = removed.Trim(_npmPackageTrims);

                return final;
            }
        }

        public static string SeperateWords(this string value)
        {
            using (_PRF_SeperateWords.Auto())
            {
                var caps = 0;
                for (var i = 1; i < value.Length; i++)
                {
                    if (char.IsUpper(value[i]))
                    {
                        caps += 1;
                    }
                }

                if (caps == 0)
                {
                    return value;
                }

                var chars = new char[value.Length + caps];

                var outIndex = 0;

                for (var i = 0; i < value.Length; i++)
                {
                    var character = value[i];

                    if ((i > 0) && char.IsUpper(character))
                    {
                        chars[outIndex] = ' ';
                        outIndex += 1;
                    }

                    chars[outIndex] = character;
                    outIndex += 1;
                }

                return new string(chars);
            }
        }

        public static string SplitPascalCase(this string input)
        {
            using (_PRF_SplitPascalCase.Auto())
            {
                switch (input)
                {
                    case "":
                    case null:
                        return input;
                    default:
                        var stringBuilder = new StringBuilder(input.Length);
                        if (char.IsLetter(input[0]))
                        {
                            stringBuilder.Append(char.ToUpper(input[0]));
                        }
                        else
                        {
                            stringBuilder.Append(input[0]);
                        }

                        for (var index = 1; index < input.Length; ++index)
                        {
                            var c = input[index];
                            if (char.IsUpper(c) && !char.IsUpper(input[index - 1]))
                            {
                                stringBuilder.Append(' ');
                            }

                            stringBuilder.Append(c);
                        }

                        return stringBuilder.ToString();
                }
            }
        }

        public static string ToTitleCase(this string input)
        {
            using (_PRF_ToTitleCase.Auto())
            {
                var stringBuilder = new StringBuilder();
                for (var index = 0; index < input.Length; ++index)
                {
                    var ch = input[index];
                    if ((ch == '_') && ((index + 1) < input.Length))
                    {
                        var upper = input[index + 1];
                        if (char.IsLower(upper))
                        {
                            upper = char.ToUpper(upper, CultureInfo.InvariantCulture);
                        }

                        stringBuilder.Append(upper);
                        ++index;
                    }
                    else
                    {
                        stringBuilder.Append(ch);
                    }
                }

                return stringBuilder.ToString();
            }
        }

        public static string WindowsToLinuxPath(this string path)
        {
            using (_PRF_WindowsToLinuxPath.Auto())
            {
                if (Path.IsPathRooted(path))
                {
                    var builder = new StringBuilder(path);

                    // C:\Program Files
                    // /c/Program Files

                    builder[1] = char.ToLowerInvariant(builder[0]);
                    builder[0] = '/';
                    path = builder.ToString();
                }

                return path.Replace("\\", "/");
            }
        }

        private static void InitializePackagePathReplacements()
        {
            using (_PRF_InitializePackagePathReplacements.Auto())
            {
                if (_packagePaths == null)
                {
                    _packagePaths = new[]
                    {
                        "Library/PackageCache", "Packages/", "Library\\PackageCache", "Packages\\"
                    };
                }
            }
        }

        private static void SetupEncodingReplacements()
        {
            using (_PRF_SetupEncodingReplacements.Auto())
            {
                if ((_encodingReplacements != null) && (_encodingReplacements.Count > 0))
                {
                    return;
                }

                _encodingPrefixes = new Dictionary<EncodingPrefix, string>();

                _encodingPrefixes.Add(EncodingPrefix.Underscore,     "_");
                _encodingPrefixes.Add(EncodingPrefix.UnicodeDefault, "U+");

                _encodingReplacements = new Dictionary<char, string>();

                _encodingReplacements.Add(' ',  "0020"); // Space
                _encodingReplacements.Add('!',  "0021"); // Exclamation mark
                _encodingReplacements.Add('"',  "0022"); // Quotation mark
                _encodingReplacements.Add('#',  "0023"); // Number sign, Hash, Octothorpe, Sharp
                _encodingReplacements.Add('$',  "0024"); // Dollar sign
                _encodingReplacements.Add('%',  "0025"); // Percent sign
                _encodingReplacements.Add('&',  "0026"); // Ampersand
                _encodingReplacements.Add('\'', "0027"); // Apostrophe
                _encodingReplacements.Add('(',  "0028"); // Left parenthesis
                _encodingReplacements.Add(')',  "0029"); // Right parenthesis
                _encodingReplacements.Add('*',  "002A"); // Asterisk
                _encodingReplacements.Add('+',  "002B"); // Plus sign
                _encodingReplacements.Add(',',  "002C"); // Comma
                _encodingReplacements.Add('-',  "002D"); // Hyphen-minus
                _encodingReplacements.Add('.',  "002E"); // Full stop
                _encodingReplacements.Add('/',  "002F"); // Slash (Solidus)
                _encodingReplacements.Add(':',  "003A"); // Colon
                _encodingReplacements.Add(';',  "003B"); // Semicolon
                _encodingReplacements.Add('<',  "003C"); // Less-than sign
                _encodingReplacements.Add('=',  "003D"); // Equal sign
                _encodingReplacements.Add('>',  "003E"); // Greater-than sign
                _encodingReplacements.Add('?',  "003F"); // Question mark
                _encodingReplacements.Add('@',  "0040"); // At sign
                _encodingReplacements.Add('[',  "005B"); // Left Square Bracket
                _encodingReplacements.Add('\\', "005C"); // Backslash
                _encodingReplacements.Add(']',  "005D"); // Right Square Bracket
                _encodingReplacements.Add('^',  "005E"); // Circumflex accent
                _encodingReplacements.Add('_',  "005F"); // Low line
                _encodingReplacements.Add('`',  "0060"); // Grave accent
                _encodingReplacements.Add('{',  "007B"); // Left Curly Bracket
                _encodingReplacements.Add('|',  "007C"); // Vertical bar
                _encodingReplacements.Add('}',  "007D"); // Right Curly Bracket
                _encodingReplacements.Add('~',  "007E"); // Tilde
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(StringExtensions) + ".";

        private static readonly ProfilerMarker _PRF_InitializePackagePathReplacements =
            new ProfilerMarker(_PRF_PFX + nameof(InitializePackagePathReplacements));

        private static readonly ProfilerMarker _PRF_IsNotNullOrEmpty =
            new ProfilerMarker(_PRF_PFX + nameof(IsNotNullOrEmpty));

        private static readonly ProfilerMarker _PRF_IsNotNullOrWhiteSpace =
            new ProfilerMarker(_PRF_PFX + nameof(IsNotNullOrWhiteSpace));

        private static readonly ProfilerMarker _PRF_CleanExtension =
            new ProfilerMarker(_PRF_PFX + nameof(CleanExtension));

        private static readonly ProfilerMarker _PRF_SetupEncodingReplacements =
            new ProfilerMarker(_PRF_PFX + nameof(SetupEncodingReplacements));

        private static readonly ProfilerMarker _PRF_EncodeUnicodePathToASCII =
            new ProfilerMarker(_PRF_PFX + nameof(EncodeUnicodePathToASCII));

        private static readonly ProfilerMarker
            _PRF_Contains = new ProfilerMarker(_PRF_PFX + nameof(Contains));

        private static readonly ProfilerMarker _PRF_CopyToClipboard =
            new ProfilerMarker(_PRF_PFX + nameof(CopyToClipboard));

        private static readonly ProfilerMarker _PRF_Cut = new ProfilerMarker(_PRF_PFX + nameof(Cut));

        private static readonly ProfilerMarker _PRF_FindGameObjectByPath =
            new ProfilerMarker(_PRF_PFX + nameof(FindGameObjectByPath));

        private static readonly ProfilerMarker _PRF_IsPackagePath =
            new ProfilerMarker(_PRF_PFX + nameof(IsPackagePath));

        private static readonly ProfilerMarker _PRF_LinuxToWindowsPath =
            new ProfilerMarker(_PRF_PFX + nameof(LinuxToWindowsPath));

        private static readonly ProfilerMarker _PRF_ParseNpmPackageVersion =
            new ProfilerMarker(_PRF_PFX + nameof(ParseNpmPackageVersion));

        private static readonly ProfilerMarker _PRF_SeperateWords =
            new ProfilerMarker(_PRF_PFX + nameof(SeperateWords));

        private static readonly ProfilerMarker _PRF_SplitPascalCase =
            new ProfilerMarker(_PRF_PFX + nameof(SplitPascalCase));

        private static readonly ProfilerMarker _PRF_ToTitleCase =
            new ProfilerMarker(_PRF_PFX + nameof(ToTitleCase));

        private static readonly ProfilerMarker _PRF_WindowsToLinuxPath =
            new ProfilerMarker(_PRF_PFX + nameof(WindowsToLinuxPath));

        private static readonly ProfilerMarker _PRF_IsNullOrWhiteSpace =
            new ProfilerMarker(_PRF_PFX + nameof(IsNullOrWhiteSpace));

        private static readonly ProfilerMarker _PRF_IsNullOrEmpty =
            new ProfilerMarker(_PRF_PFX + nameof(IsNullOrEmpty));

        #endregion
    }
}
