using System;
using Appalachia.Utility.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Appalachia.Utility.Logging
{
    public class AppaLogFormats : ScriptableObject
    {
        #region Constants and Static Readonly

        public const string ADDRESS = "AppaLogColors";

        #endregion

        #region Fields and Autoproperties

        [BoxGroup("Contexts")] public Contexts contexts;

        [BoxGroup("General")] public LogFormat filename;

        [BoxGroup("General")] public LogFormat message;

        [BoxGroup("Levels")] public LogLevel levels;

        #endregion

#if UNITY_EDITOR
        [Button]
        private void Test()
        {
            AppaLog.ClearDeveloperConsole();
            AppaLog.Test();
        }
#endif

        #region Nested type: Contexts

        [Serializable]
        public struct Contexts
        {
            #region Fields and Autoproperties

            public LogFormat application;
            public LogFormat area;
            public LogFormat bootload;
            public LogFormat maintenance;
            public LogFormat timeline;

            #endregion
        }

        #endregion

        #region Nested type: LogFormat

        [Serializable, InlineProperty, LabelWidth(100)]
        public struct LogFormat
        {
             #region Fields and Autoproperties

            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format", .15f)]
            [ToggleLeft]
            [LabelWidth(40)]
            public bool bold;

            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format", .15f)]
            [ToggleLeft]
            [LabelWidth(40)]
            public bool italic;

            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format", .7f)]
            [LabelWidth(40)]
            public Color color;

            #endregion

            public string Format(string v)
            {
                if (bold)
                {
                    v = v.Bold();
                }

                if (italic)
                {
                    v = v.Italic();
                }

                v = v.Color(color);

                return v;
            }

            private void Validate()
            {
                if (color.a == 0f)
                {
                    color.a = 1f;
                }
            }
        }

        #endregion

        #region Nested type: LogLevel

        [Serializable]
        public struct LogLevel
        {
            #region Fields and Autoproperties

            [PropertyOrder(10)] public LogFormat critical;
            [PropertyOrder(60)] public LogFormat debug;
            [PropertyOrder(30)] public LogFormat error;
            [PropertyOrder(20)] public LogFormat exception;
            [PropertyOrder(00)] public LogFormat fatal;
            [PropertyOrder(50)] public LogFormat info;
            [PropertyOrder(70)] public LogFormat trace;
            [PropertyOrder(40)] public LogFormat warn;

            #endregion
        }

        #endregion
    }
}
