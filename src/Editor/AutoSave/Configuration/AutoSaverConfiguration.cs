#if UNITY_EDITOR

#region

using Appalachia.Utility.Strings;
using UnityEditor;
using UnityEngine.Device;

#endregion

namespace Appalachia.Utility.AutoSave.Configuration
{
    public static class AutoSaverConfiguration
    {
        #region Constants and Static Readonly

        internal const string DATE_TIME_FORMAT = "yyyy-MM-dd_HH-mm-ss";

        private static readonly CachedBool _debug;
        private static readonly CachedBool _enable;
        private static readonly CachedFloat _lastSave;

        private static readonly CachedFloat _saveInterval;
        private static readonly CachedInt _filesCount;

        private static readonly CachedString _fileName;

        private static readonly CachedString _location;
        private const string PREFIX = "Appalachia/Internal/AutoSave";

        #endregion

        static AutoSaverConfiguration()
        {
            _debug = new(ZString.Format("{0}/debug",                PREFIX), false);
            _saveInterval = new(ZString.Format("{0}/save-interval", PREFIX), 5);
            _enable = new(ZString.Format("{0}/enable-save",         PREFIX), true);
            _filesCount = new(ZString.Format("{0}/files-count",     PREFIX), 10);
            _lastSave = new(ZString.Format("{0}/last-save",         PREFIX), 0);
            _fileName = new(ZString.Format("{0}/file-name",         PREFIX), "AutoSave");
            _location = new(ZString.Format("{0}/location",          PREFIX), "_autosave");
        }

        internal static float EditorTimer => (float)(EditorApplication.timeSinceStartup % 1000000);

        internal static bool Debug
        {
            get => _debug.Current ?? false;
            set => _debug.Current = value;
        }

        internal static bool Enable
        {
            get => _enable.Current ?? false;
            set => _enable.Current = value;
        }

        internal static float LastSave
        {
            get => (float)_lastSave;
            set => _lastSave.Current = value;
        }

        internal static float SaveInterval
        {
            get => (float)_saveInterval * 60;
            set => _saveInterval.Current = value / 60;
        }

        internal static int FilesCount
        {
            get => _filesCount.Current ?? 0;
            set => _filesCount.Current = value;
        }

        internal static string FileName
        {
            get => _fileName.Current;
            set => _fileName.Current = value;
        }

        internal static string Location
        {
            get => _location.Current;
            set => _location.Current = value;
        }

        internal static string GetRelativeSaveDirectory()
        {
            var relativeSavePath = "Assets/" + Location + "/";
            return relativeSavePath;
        }

        internal static string GetSaveDirectory()
        {
            var savePath = Application.dataPath + "/" + Location;
            return savePath;
        }
    }
}

#endif
