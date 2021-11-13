using System;
using Appalachia.Utility.Constants;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Appalachia.Utility.Logging
{
    public class AppaLogFormats : ScriptableObject
    {
        public const string ADDRESS = "AppaLogColors";
        
        #region Fields

        [BoxGroup("General")]
        public LogFormat filename;

        [BoxGroup("General")]
        public LogFormat message;
        
        [BoxGroup("Contexts")]
        public Contexts contexts;
        
        [BoxGroup("Levels")]
        public LogLevel levels;

        #endregion

        [Button]
        private void Test()
        {
            AppaLog.ClearDeveloperConsole();
            AppaLog.Test();
        }

        #region Nested type: Contexts

        [Serializable]
        public struct Contexts
        {
            #region Fields

            public LogFormat application;
            public LogFormat area;
            public LogFormat bootload;
            public LogFormat subArea;

            #endregion
        }

        #endregion

        #region Nested type: LogFormat

        [Serializable, InlineProperty, LabelWidth(100)]
        public struct LogFormat
        {
            #region Fields

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
            #region Fields

            [PropertyOrder(00)] public LogFormat fatal;
            [PropertyOrder(10)] public LogFormat critical;
            [PropertyOrder(20)] public LogFormat exception;
            [PropertyOrder(30)] public LogFormat error;            
            [PropertyOrder(40)] public LogFormat warn;
            [PropertyOrder(50)] public LogFormat info;
            [PropertyOrder(60)] public LogFormat debug;
            [PropertyOrder(70)] public LogFormat trace;

            #endregion
        }

        #endregion
    }
}
