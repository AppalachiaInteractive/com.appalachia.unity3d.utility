using System;
using Appalachia.Utility.Preferences.Easy.Base;
using UnityEditor;

namespace Appalachia.Utility.Preferences.Easy
{
    public class BoolEditorPref : EasyEditorPref<bool>
    {
        public BoolEditorPref(
            string path,
            string key,
            string label,
            bool defaultValue = default,
            SettingsScope scope = SettingsScope.User,
            int order = 0,
            Func<bool> drawIf = null,
            Func<bool> enableIf = null,
            Action actionButton = null,
            string actionLabel = "Action") : base(
            path,
            key,
            label,
            defaultValue,
            scope,
            order,
            drawIf,
            enableIf,
            actionButton,
            actionLabel
        )
        {
        }

        /// <inheritdoc />
        public override bool Value
        {
            get
            {
                if (!EditorPrefs.HasKey(key))
                {
                    EditorPrefs.SetBool(key, defaultValue);
                }

                return EditorPrefs.GetBool(key, defaultValue);
            }
            set => EditorPrefs.SetBool(key, value);
        }

        /// <inheritdoc />
        protected override bool Draw()
        {
            return EditorGUILayout.Toggle(label, Value);
        }

        /// <inheritdoc />
        protected override bool DrawDelayed()
        {
            return EditorGUILayout.Toggle(label, Value);
        }
    }
}
