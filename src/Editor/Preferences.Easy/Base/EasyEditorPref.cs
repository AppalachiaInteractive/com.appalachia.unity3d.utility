using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Appalachia.Utility.Preferences.Easy.Base
{
    public abstract class EasyEditorPref<T> : EasyEditorPrefBase, IEquatable<EasyEditorPref<T>>, IEquatable<T>
    {
        protected EasyEditorPref(
            string path,
            string key,
            string label,
            T defaultValue,
            SettingsScope scope,
            int order,
            Func<bool> drawIf,
            Func<bool> enableIf,
            Action actionButton,
            string actionLabel) : base(
            path,
            key,
            label,
            scope,
            order,
            drawIf,
            enableIf,
            actionButton,
            actionLabel
        )
        {
            this.defaultValue = defaultValue;
        }

        #region Fields and Autoproperties

        public readonly T defaultValue;

        #endregion

        public abstract T Value { get; set; }

        [DebuggerStepThrough]
        public static bool operator ==(EasyEditorPref<T> left, EasyEditorPref<T> right)
        {
            return Equals(left, right);
        }

        [DebuggerStepThrough]
        public static bool operator ==(EasyEditorPref<T> left, T right)
        {
            if (left == null)
            {
                return false;
            }

            return Equals(left.Value, right);
        }

        [DebuggerStepThrough]
        public static implicit operator T(EasyEditorPref<T> wrapper)
        {
            return wrapper.Value;
        }

        [DebuggerStepThrough]
        public static bool operator !=(EasyEditorPref<T> left, EasyEditorPref<T> right)
        {
            return !Equals(left, right);
        }

        [DebuggerStepThrough]
        public static bool operator !=(EasyEditorPref<T> left, T right)
        {
            if (left == null)
            {
                return false;
            }

            return !Equals(left.Value, right);
        }

        /// <inheritdoc />
        public override void DrawDelayedUI()
        {
            DrawInternal(DrawDelayed);
        }

        /// <inheritdoc />
        public override void DrawUI()
        {
            DrawInternal(Draw);
        }

        [DebuggerStepThrough]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((EasyEditorPref<T>)obj);
        }

        [DebuggerStepThrough]
        public override int GetHashCode()
        {
            unchecked
            {
                return ((key != null ? key.GetHashCode() : 0) * 397) ^
                       EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }

        protected abstract T Draw();
        protected abstract T DrawDelayed();

        private void DrawInternal(Func<T> drawAction)
        {
            if ((drawIf != null) && !drawIf())
            {
                return;
            }

            var guiEnabled = GUI.enabled;
            if (enableIf != null)
            {
                GUI.enabled = enableIf();
            }

            EditorGUI.BeginChangeCheck();

            T value;
            using (new EditorGUILayout.HorizontalScope())
            {
                value = drawAction();

                if (actionButton != null)
                {
                    EditorGUILayout.Space(1.0f, false);
                    var l = new GUIContent(actionLabel ?? "   ");

                    var width = EditorStyles.miniButton.CalcSize(l).x;
                    if (GUILayout.Button(l, EditorStyles.miniButton, GUILayout.Width(width + 10f)))
                    {
                        actionButton();
                    }
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                Value = value;
            }

            GUI.enabled = guiEnabled;
        }

        #region IEquatable<EasyEditorPref<T>> Members

        [DebuggerStepThrough]
        public bool Equals(EasyEditorPref<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return (key == other.key) && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        #endregion

        #region IEquatable<T> Members

        [DebuggerStepThrough]
        public bool Equals(T other)
        {
            return !ReferenceEquals(null, other) && EqualityComparer<T>.Default.Equals(Value, other);
        }

        #endregion
    }
}
