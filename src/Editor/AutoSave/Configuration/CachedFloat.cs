#if UNITY_EDITOR

#region

using UnityEditor;

#endregion

namespace Appalachia.Utility.AutoSave.Configuration
{
    internal class CachedFloat : Cached<float?, float>
    {
        public CachedFloat(string key, float defaultValue) : base(key, defaultValue)
        {
        }

        /// <inheritdoc />
        protected override float? Get(string key, float defaultValue)
        {
            var value = EditorPrefs.GetFloat(key, defaultValue);
            return value;
        }

        /// <inheritdoc />
        protected override void Set(string key, float? value)
        {
            if (value.HasValue)
            {
                EditorPrefs.SetFloat(key, value.Value);
            }
        }
    }
}

#endif
