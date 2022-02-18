#if UNITY_EDITOR

#region

using UnityEditor;

#endregion

namespace Appalachia.Utility.AutoSave.Configuration
{
    internal class CachedInt : Cached<int?, int>
    {
        public CachedInt(string key, int defaultValue) : base(key, defaultValue)
        {
        }

        /// <inheritdoc />
        protected override int? Get(string key, int defaultValue)
        {
            var value = EditorPrefs.GetInt(key, defaultValue);
            return value;
        }

        /// <inheritdoc />
        protected override void Set(string key, int? value)
        {
            if (value.HasValue)
            {
                EditorPrefs.SetInt(key, value.Value);
            }
        }
    }
}

#endif
