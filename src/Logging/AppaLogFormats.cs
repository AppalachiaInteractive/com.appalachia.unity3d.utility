using System;
using System.Collections.Generic;
using Appalachia.Utility.Constants;
using Appalachia.Utility.Strings;
using Sirenix.OdinInspector;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;

namespace Appalachia.Utility.Logging
{
    public class AppaLogFormats : ScriptableObject
    {
        [Flags]
        public enum LogFormatFlags
        {
            None = 0,
            Bold = 1 << 1,
            Italic = 1 << 2,
            Surrounded = 1 << 3,
            Hyphenated = 1 << 4,
        }

        public enum TextTransformation
        {
            None = 0,
            UPPER = 10,
            lower = 20,
        }

        #region Constants and Static Readonly

        public const string ADDRESS = "AppaLogColors";

        #endregion

        #region Fields and Autoproperties

        [FoldoutGroup("Contexts"), InlineProperty, HideLabel]
        public Contexts contexts;

        [FoldoutGroup("Levels"), InlineProperty, HideLabel]
        public LogLevel levels;

        [FoldoutGroup("Specials"), InlineProperty, HideLabel]
        public Specials specials;

        #endregion

        #region Event Functions

        private void Awake()
        {
            AppaLogFormatHolder.formats = this;
        }

        #endregion

        #region Nested type: Contexts

        [Serializable]
        public struct Contexts
        {
            #region Fields and Autoproperties

            public LogFormat _extra00;
            public LogFormat _extra01;
            public LogFormat _extra02;
            public LogFormat _extra03;
            public LogFormat _extra04;
            public LogFormat _extra05;
            public LogFormat _extra06;
            public LogFormat _extra07;
            public LogFormat _extra08;
            public LogFormat _extra09;
            public LogFormat _extra10;
            public LogFormat _extra11;
            public LogFormat _extra12;
            public LogFormat _extra13;
            public LogFormat _extra14;
            public LogFormat _extra15;
            public LogFormat _extra16;
            public LogFormat _extra17;
            public LogFormat _extra18;
            public LogFormat _extra19;
            public LogFormat _extra20;
            public LogFormat _extra21;
            public LogFormat _extra22;
            public LogFormat _extra23;
            public LogFormat _extra24;
            public LogFormat _extra25;
            public LogFormat _extra26;
            public LogFormat _extra27;
            public LogFormat _extra28;
            public LogFormat _extra29;
            public LogFormat _extra30;
            public LogFormat _extra31;
            public LogFormat _extra32;
            public LogFormat _extra33;
            public LogFormat _extra34;
            public LogFormat _extra35;
            public LogFormat _extra36;
            public LogFormat _extra37;
            public LogFormat _extra38;
            public LogFormat _extra39;
            public LogFormat _extra40;
            public LogFormat _extra41;
            public LogFormat _extra42;
            public LogFormat _extra43;
            public LogFormat _extra44;
            public LogFormat _extra45;
            public LogFormat _extra46;
            public LogFormat _extra47;
            public LogFormat _extra48;
            public LogFormat _extra49;
            public LogFormat _extra50;
            public LogFormat _extra51;
            public LogFormat _extra52;
            public LogFormat _extra53;
            public LogFormat _extra54;
            public LogFormat _extra55;
            public LogFormat _extra56;
            public LogFormat _extra57;
            public LogFormat _extra58;
            public LogFormat _extra59;
            public LogFormat _extra60;
            public LogFormat _extra61;
            public LogFormat _extra62;
            public LogFormat _extra63;
            public LogFormat _extra64;
            public LogFormat _extra65;
            public LogFormat _extra66;
            public LogFormat _extra67;
            public LogFormat _extra68;
            public LogFormat _extra69;
            public LogFormat _extra70;
            public LogFormat _extra71;
            public LogFormat _extra72;
            public LogFormat _extra73;
            public LogFormat _extra74;
            public LogFormat _extra75;
            public LogFormat _extra76;
            public LogFormat _extra77;
            public LogFormat _extra78;
            public LogFormat _extra79;
            public LogFormat _extra80;
            public LogFormat _extra81;
            public LogFormat _extra82;
            public LogFormat _extra83;
            public LogFormat _extra84;
            public LogFormat _extra85;
            public LogFormat _extra86;
            public LogFormat _extra87;
            public LogFormat _extra88;
            public LogFormat _extra89;
            public LogFormat _extra90;
            public LogFormat _extra91;
            public LogFormat _extra92;
            public LogFormat _extra93;
            public LogFormat _extra94;
            public LogFormat _extra95;
            public LogFormat _extra96;
            public LogFormat _extra97;
            public LogFormat _extra98;

            [FormerlySerializedAs("_extra99")]
            public LogFormat dependencies;

            public LogFormat animal;
            public LogFormat animation;
            public LogFormat application;
            public LogFormat area;
            public LogFormat arrayPooling;
            public LogFormat assets;
            public LogFormat audio;
            public LogFormat bazooka;
            public LogFormat behaviours;
            public LogFormat bootload;
            public LogFormat caching;
            public LogFormat character;
            public LogFormat ci;
            public LogFormat clock;
            public LogFormat collections;
            public LogFormat components;
            public LogFormat convexDecomposition;
            public LogFormat core;
            public LogFormat crafting;
            public LogFormat cursor;
            public LogFormat data;
            public LogFormat database;
            public LogFormat debugOverlay;
            public LogFormat devConsole;
            public LogFormat editing;
            public LogFormat editor;
            public LogFormat execution;
            public LogFormat extensions;
            public LogFormat filtering;
            public LogFormat fire;
            public LogFormat frameEvent;
            public LogFormat game;
            public LogFormat gameplay;
            public LogFormat globals;
            public LogFormat hud;
            public LogFormat inGameMenu;
            public LogFormat initialize;
            public LogFormat input;
            public LogFormat inventory;
            public LogFormat jobs;
            public LogFormat koc;
            public LogFormat labels;
            public LogFormat layers;
            public LogFormat lifetime;
            public LogFormat lighting;
            public LogFormat loadingScreen;
            public LogFormat mainMenu;
            public LogFormat maintenance;
            public LogFormat math;
            public LogFormat meshBurial;
            public LogFormat meshData;
            public LogFormat obi;
            public LogFormat objectPooling;
            public LogFormat octree;
            public LogFormat optimization;
            public LogFormat overrides;
            public LogFormat pauseMenu;
            public LogFormat playables;
            public LogFormat postProcessing;
            public LogFormat prefabRendering;
            public LogFormat prefabs;
            public LogFormat preferences;
            public LogFormat prototype;
            public LogFormat reactionSystem;
            public LogFormat rendering;
            public LogFormat runtimeGraphs;
            public LogFormat scriptables;
            public LogFormat sdf;
            public LogFormat shading;
            public LogFormat shell;
            public LogFormat simulation;
            public LogFormat singleton;
            public LogFormat spatial;
            public LogFormat splashScreen;
            public LogFormat startEnvironment;
            public LogFormat startScreen;
            public LogFormat styling;
            public LogFormat terrain;
            public LogFormat timeline;
            public LogFormat touchBend;
            public LogFormat trees;
            public LogFormat ui;
            public LogFormat uncategorized;
            public LogFormat utility;
            public LogFormat vfx;
            public LogFormat visualizers;
            public LogFormat volumes;
            public LogFormat voxels;
            public LogFormat water;
            public LogFormat wind;

            #endregion

            public void DefaultAll()
            {
                animal.color = Color.white;
                animal.format = LogFormatFlags.Bold;
                animation.color = Color.white;
                animation.format = LogFormatFlags.Bold;
                application.color = Color.white;
                application.format = LogFormatFlags.Bold;
                area.color = Color.white;
                area.format = LogFormatFlags.Bold;
                arrayPooling.color = Color.white;
                arrayPooling.format = LogFormatFlags.Bold;
                assets.color = Color.white;
                assets.format = LogFormatFlags.Bold;
                audio.color = Color.white;
                audio.format = LogFormatFlags.Bold;
                bazooka.color = Color.white;
                bazooka.format = LogFormatFlags.Bold;
                behaviours.color = Color.white;
                behaviours.format = LogFormatFlags.Bold;
                bootload.color = Color.white;
                bootload.format = LogFormatFlags.Bold;
                caching.color = Color.white;
                caching.format = LogFormatFlags.Bold;
                character.color = Color.white;
                character.format = LogFormatFlags.Bold;
                ci.color = Color.white;
                ci.format = LogFormatFlags.Bold;
                clock.color = Color.white;
                clock.format = LogFormatFlags.Bold;
                collections.color = Color.white;
                collections.format = LogFormatFlags.Bold;
                components.color = Color.white;
                components.format = LogFormatFlags.Bold;
                convexDecomposition.color = Color.white;
                convexDecomposition.format = LogFormatFlags.Bold;
                core.color = Color.white;
                core.format = LogFormatFlags.Bold;
                crafting.color = Color.white;
                crafting.format = LogFormatFlags.Bold;
                cursor.color = Color.white;
                cursor.format = LogFormatFlags.Bold;
                data.color = Color.white;
                data.format = LogFormatFlags.Bold;
                database.color = Color.white;
                database.format = LogFormatFlags.Bold;
                debugOverlay.color = Color.white;
                debugOverlay.format = LogFormatFlags.Bold;
                devConsole.color = Color.white;
                devConsole.format = LogFormatFlags.Bold;
                editing.color = Color.white;
                editing.format = LogFormatFlags.Bold;
                editor.color = Color.white;
                editor.format = LogFormatFlags.Bold;
                execution.color = Color.white;
                execution.format = LogFormatFlags.Bold;
                extensions.color = Color.white;
                extensions.format = LogFormatFlags.Bold;
                filtering.color = Color.white;
                filtering.format = LogFormatFlags.Bold;
                fire.color = Color.white;
                fire.format = LogFormatFlags.Bold;
                frameEvent.color = Color.white;
                frameEvent.format = LogFormatFlags.Bold;
                game.color = Color.white;
                game.format = LogFormatFlags.Bold;
                gameplay.color = Color.white;
                gameplay.format = LogFormatFlags.Bold;
                globals.color = Color.white;
                globals.format = LogFormatFlags.Bold;
                hud.color = Color.white;
                hud.format = LogFormatFlags.Bold;
                inGameMenu.color = Color.white;
                inGameMenu.format = LogFormatFlags.Bold;
                input.color = Color.white;
                input.format = LogFormatFlags.Bold;
                inventory.color = Color.white;
                inventory.format = LogFormatFlags.Bold;
                jobs.color = Color.white;
                jobs.format = LogFormatFlags.Bold;
                koc.color = Color.white;
                koc.format = LogFormatFlags.Bold;
                labels.color = Color.white;
                labels.format = LogFormatFlags.Bold;
                layers.color = Color.white;
                layers.format = LogFormatFlags.Bold;
                lifetime.color = Color.white;
                lifetime.format = LogFormatFlags.Bold;
                lighting.color = Color.white;
                lighting.format = LogFormatFlags.Bold;
                loadingScreen.color = Color.white;
                loadingScreen.format = LogFormatFlags.Bold;
                mainMenu.color = Color.white;
                mainMenu.format = LogFormatFlags.Bold;
                maintenance.color = Color.white;
                maintenance.format = LogFormatFlags.Bold;
                math.color = Color.white;
                math.format = LogFormatFlags.Bold;
                meshBurial.color = Color.white;
                meshBurial.format = LogFormatFlags.Bold;
                meshData.color = Color.white;
                meshData.format = LogFormatFlags.Bold;
                obi.color = Color.white;
                obi.format = LogFormatFlags.Bold;
                objectPooling.color = Color.white;
                objectPooling.format = LogFormatFlags.Bold;
                octree.color = Color.white;
                octree.format = LogFormatFlags.Bold;
                optimization.color = Color.white;
                optimization.format = LogFormatFlags.Bold;
                overrides.color = Color.white;
                overrides.format = LogFormatFlags.Bold;
                pauseMenu.color = Color.white;
                pauseMenu.format = LogFormatFlags.Bold;
                playables.color = Color.white;
                playables.format = LogFormatFlags.Bold;
                postProcessing.color = Color.white;
                postProcessing.format = LogFormatFlags.Bold;
                prefabRendering.color = Color.white;
                prefabRendering.format = LogFormatFlags.Bold;
                prefabs.color = Color.white;
                prefabs.format = LogFormatFlags.Bold;
                preferences.color = Color.white;
                preferences.format = LogFormatFlags.Bold;
                prototype.color = Color.white;
                prototype.format = LogFormatFlags.Bold;
                reactionSystem.color = Color.white;
                reactionSystem.format = LogFormatFlags.Bold;
                rendering.color = Color.white;
                rendering.format = LogFormatFlags.Bold;
                runtimeGraphs.color = Color.white;
                runtimeGraphs.format = LogFormatFlags.Bold;
                scriptables.color = Color.white;
                scriptables.format = LogFormatFlags.Bold;
                sdf.color = Color.white;
                sdf.format = LogFormatFlags.Bold;
                shading.color = Color.white;
                shading.format = LogFormatFlags.Bold;
                shell.color = Color.white;
                shell.format = LogFormatFlags.Bold;
                simulation.color = Color.white;
                simulation.format = LogFormatFlags.Bold;
                singleton.color = Color.white;
                singleton.format = LogFormatFlags.Bold;
                spatial.color = Color.white;
                spatial.format = LogFormatFlags.Bold;
                splashScreen.color = Color.white;
                splashScreen.format = LogFormatFlags.Bold;
                startEnvironment.color = Color.white;
                startEnvironment.format = LogFormatFlags.Bold;
                startScreen.color = Color.white;
                startScreen.format = LogFormatFlags.Bold;
                styling.color = Color.white;
                styling.format = LogFormatFlags.Bold;
                terrain.color = Color.white;
                terrain.format = LogFormatFlags.Bold;
                timeline.color = Color.white;
                timeline.format = LogFormatFlags.Bold;
                touchBend.color = Color.white;
                touchBend.format = LogFormatFlags.Bold;
                trees.color = Color.white;
                trees.format = LogFormatFlags.Bold;
                ui.color = Color.white;
                ui.format = LogFormatFlags.Bold;
                uncategorized.color = Color.white;
                uncategorized.format = LogFormatFlags.Bold;
                utility.color = Color.white;
                utility.format = LogFormatFlags.Bold;
                vfx.color = Color.white;
                vfx.format = LogFormatFlags.Bold;
                visualizers.color = Color.white;
                visualizers.format = LogFormatFlags.Bold;
                volumes.color = Color.white;
                volumes.format = LogFormatFlags.Bold;
                voxels.color = Color.white;
                voxels.format = LogFormatFlags.Bold;
                water.color = Color.white;
                water.format = LogFormatFlags.Bold;
                wind.color = Color.white;
                wind.format = LogFormatFlags.Bold;

                _extra00.color = Color.white;
                _extra00.format = LogFormatFlags.Bold;
                _extra01.color = Color.white;
                _extra01.format = LogFormatFlags.Bold;
                _extra02.color = Color.white;
                _extra02.format = LogFormatFlags.Bold;
                _extra03.color = Color.white;
                _extra03.format = LogFormatFlags.Bold;
                _extra04.color = Color.white;
                _extra04.format = LogFormatFlags.Bold;
                _extra05.color = Color.white;
                _extra05.format = LogFormatFlags.Bold;
                _extra06.color = Color.white;
                _extra06.format = LogFormatFlags.Bold;
                _extra07.color = Color.white;
                _extra07.format = LogFormatFlags.Bold;
                _extra08.color = Color.white;
                _extra08.format = LogFormatFlags.Bold;
                _extra09.color = Color.white;
                _extra09.format = LogFormatFlags.Bold;
                _extra10.color = Color.white;
                _extra10.format = LogFormatFlags.Bold;
                _extra11.color = Color.white;
                _extra11.format = LogFormatFlags.Bold;
                _extra12.color = Color.white;
                _extra12.format = LogFormatFlags.Bold;
                _extra13.color = Color.white;
                _extra13.format = LogFormatFlags.Bold;
                _extra14.color = Color.white;
                _extra14.format = LogFormatFlags.Bold;
                _extra15.color = Color.white;
                _extra15.format = LogFormatFlags.Bold;
                _extra16.color = Color.white;
                _extra16.format = LogFormatFlags.Bold;
                _extra17.color = Color.white;
                _extra17.format = LogFormatFlags.Bold;
                _extra18.color = Color.white;
                _extra18.format = LogFormatFlags.Bold;
                _extra19.color = Color.white;
                _extra19.format = LogFormatFlags.Bold;
                _extra20.color = Color.white;
                _extra20.format = LogFormatFlags.Bold;
                _extra21.color = Color.white;
                _extra21.format = LogFormatFlags.Bold;
                _extra22.color = Color.white;
                _extra22.format = LogFormatFlags.Bold;
                _extra23.color = Color.white;
                _extra23.format = LogFormatFlags.Bold;
                _extra24.color = Color.white;
                _extra24.format = LogFormatFlags.Bold;
                _extra25.color = Color.white;
                _extra25.format = LogFormatFlags.Bold;
                _extra26.color = Color.white;
                _extra26.format = LogFormatFlags.Bold;
                _extra27.color = Color.white;
                _extra27.format = LogFormatFlags.Bold;
                _extra28.color = Color.white;
                _extra28.format = LogFormatFlags.Bold;
                _extra29.color = Color.white;
                _extra29.format = LogFormatFlags.Bold;
                _extra30.color = Color.white;
                _extra30.format = LogFormatFlags.Bold;
                _extra31.color = Color.white;
                _extra31.format = LogFormatFlags.Bold;
                _extra32.color = Color.white;
                _extra32.format = LogFormatFlags.Bold;
                _extra33.color = Color.white;
                _extra33.format = LogFormatFlags.Bold;
                _extra34.color = Color.white;
                _extra34.format = LogFormatFlags.Bold;
                _extra35.color = Color.white;
                _extra35.format = LogFormatFlags.Bold;
                _extra36.color = Color.white;
                _extra36.format = LogFormatFlags.Bold;
                _extra37.color = Color.white;
                _extra37.format = LogFormatFlags.Bold;
                _extra38.color = Color.white;
                _extra38.format = LogFormatFlags.Bold;
                _extra39.color = Color.white;
                _extra39.format = LogFormatFlags.Bold;
                _extra40.color = Color.white;
                _extra40.format = LogFormatFlags.Bold;
                _extra41.color = Color.white;
                _extra41.format = LogFormatFlags.Bold;
                _extra42.color = Color.white;
                _extra42.format = LogFormatFlags.Bold;
                _extra43.color = Color.white;
                _extra43.format = LogFormatFlags.Bold;
                _extra44.color = Color.white;
                _extra44.format = LogFormatFlags.Bold;
                _extra45.color = Color.white;
                _extra45.format = LogFormatFlags.Bold;
                _extra46.color = Color.white;
                _extra46.format = LogFormatFlags.Bold;
                _extra47.color = Color.white;
                _extra47.format = LogFormatFlags.Bold;
                _extra48.color = Color.white;
                _extra48.format = LogFormatFlags.Bold;
                _extra49.color = Color.white;
                _extra49.format = LogFormatFlags.Bold;
                _extra50.color = Color.white;
                _extra50.format = LogFormatFlags.Bold;
                _extra51.color = Color.white;
                _extra51.format = LogFormatFlags.Bold;
                _extra52.color = Color.white;
                _extra52.format = LogFormatFlags.Bold;
                _extra53.color = Color.white;
                _extra53.format = LogFormatFlags.Bold;
                _extra54.color = Color.white;
                _extra54.format = LogFormatFlags.Bold;
                _extra55.color = Color.white;
                _extra55.format = LogFormatFlags.Bold;
                _extra56.color = Color.white;
                _extra56.format = LogFormatFlags.Bold;
                _extra57.color = Color.white;
                _extra57.format = LogFormatFlags.Bold;
                _extra58.color = Color.white;
                _extra58.format = LogFormatFlags.Bold;
                _extra59.color = Color.white;
                _extra59.format = LogFormatFlags.Bold;
                _extra60.color = Color.white;
                _extra60.format = LogFormatFlags.Bold;
                _extra61.color = Color.white;
                _extra61.format = LogFormatFlags.Bold;
                _extra62.color = Color.white;
                _extra62.format = LogFormatFlags.Bold;
                _extra63.color = Color.white;
                _extra63.format = LogFormatFlags.Bold;
                _extra64.color = Color.white;
                _extra64.format = LogFormatFlags.Bold;
                _extra65.color = Color.white;
                _extra65.format = LogFormatFlags.Bold;
                _extra66.color = Color.white;
                _extra66.format = LogFormatFlags.Bold;
                _extra67.color = Color.white;
                _extra67.format = LogFormatFlags.Bold;
                _extra68.color = Color.white;
                _extra68.format = LogFormatFlags.Bold;
                _extra69.color = Color.white;
                _extra69.format = LogFormatFlags.Bold;
                _extra70.color = Color.white;
                _extra70.format = LogFormatFlags.Bold;
                _extra71.color = Color.white;
                _extra71.format = LogFormatFlags.Bold;
                _extra72.color = Color.white;
                _extra72.format = LogFormatFlags.Bold;
                _extra73.color = Color.white;
                _extra73.format = LogFormatFlags.Bold;
                _extra74.color = Color.white;
                _extra74.format = LogFormatFlags.Bold;
                _extra75.color = Color.white;
                _extra75.format = LogFormatFlags.Bold;
                _extra76.color = Color.white;
                _extra76.format = LogFormatFlags.Bold;
                _extra77.color = Color.white;
                _extra77.format = LogFormatFlags.Bold;
                _extra78.color = Color.white;
                _extra78.format = LogFormatFlags.Bold;
                _extra79.color = Color.white;
                _extra79.format = LogFormatFlags.Bold;
                _extra80.color = Color.white;
                _extra80.format = LogFormatFlags.Bold;
                _extra81.color = Color.white;
                _extra81.format = LogFormatFlags.Bold;
                _extra82.color = Color.white;
                _extra82.format = LogFormatFlags.Bold;
                _extra83.color = Color.white;
                _extra83.format = LogFormatFlags.Bold;
                _extra84.color = Color.white;
                _extra84.format = LogFormatFlags.Bold;
                _extra85.color = Color.white;
                _extra85.format = LogFormatFlags.Bold;
                _extra86.color = Color.white;
                _extra86.format = LogFormatFlags.Bold;
                _extra87.color = Color.white;
                _extra87.format = LogFormatFlags.Bold;
                _extra88.color = Color.white;
                _extra88.format = LogFormatFlags.Bold;
                _extra89.color = Color.white;
                _extra89.format = LogFormatFlags.Bold;
                _extra90.color = Color.white;
                _extra90.format = LogFormatFlags.Bold;
                _extra91.color = Color.white;
                _extra91.format = LogFormatFlags.Bold;
                _extra92.color = Color.white;
                _extra92.format = LogFormatFlags.Bold;
                _extra93.color = Color.white;
                _extra93.format = LogFormatFlags.Bold;
                _extra94.color = Color.white;
                _extra94.format = LogFormatFlags.Bold;
                _extra95.color = Color.white;
                _extra95.format = LogFormatFlags.Bold;
                _extra96.color = Color.white;
                _extra96.format = LogFormatFlags.Bold;
                _extra97.color = Color.white;
                _extra97.format = LogFormatFlags.Bold;
                _extra98.color = Color.white;
                _extra98.format = LogFormatFlags.Bold;
                dependencies.color = Color.white;
                dependencies.format = LogFormatFlags.Bold;
            }
        }

        #endregion

        #region Nested type: LogFormat

        [Serializable, InlineProperty, LabelWidth(110)]
        public struct LogFormat
        {
            #region Constants and Static Readonly

            private const string HYPHEN = "-";

            #endregion

            #region Fields and Autoproperties

            [FormerlySerializedAs("color")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(40)]
            [PropertyOrder(3)]
            [SerializeField]
            private Color _color;

            private Dictionary<string, string> _formattedStrings;

            [FormerlySerializedAs("format")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(45)]
            [PropertyOrder(1)]
            [SerializeField]
            private LogFormatFlags _format;

            [FormerlySerializedAs("surroundLeft")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(10)]
            [LabelText("L")]
            [EnableIf(nameof(_enableSurroundFields))]
            [PropertyOrder(14)]
            [SerializeField]
            private string _surroundLeft;

            [FormerlySerializedAs("surroundRight")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(10)]
            [LabelText("R")]
            [EnableIf(nameof(_enableSurroundFields))]
            [PropertyOrder(15)]
            [SerializeField]
            private string _surroundRight;

            [FormerlySerializedAs("style")]
            [OnValueChanged(nameof(Validate))]
            [HorizontalGroup("Format")]
            [LabelWidth(35)]
            [PropertyOrder(10)]
            [SerializeField]
            private TextTransformation _style;

            #endregion

            public Color color
            {
                get => _color == Color.clear ? Color.white : _color;
                set => _color = value;
            }

            public LogFormatFlags format
            {
                get => _format;
                set => _format = value;
            }

            public string surroundLeft
            {
                get => _surroundLeft;
                set => _surroundLeft = value;
            }

            public string surroundRight
            {
                get => _surroundRight;
                set => _surroundRight = value;
            }

            public TextTransformation style
            {
                get => _style;
                set => _style = value;
            }

            private bool _enableSurroundFields => format.HasFlag(LogFormatFlags.Surrounded);

            public string Format(string v)
            {
                using (_PRF_Format.Auto())
                {
                    if (v == null)
                    {
                        return null;
                    }

                    if (TryGetCachedString(v, out var result))
                    {
                        return result;
                    }

                    var input = v;

                    var output = new Utf16ValueStringBuilder(true);

                    output.Append(v);

                    if (format.HasFlag(LogFormatFlags.Hyphenated))
                    {
                        for (var i = 1; i < output.Length; i++)
                        {
                            if (char.IsUpper(output[i]) && !char.IsUpper(output[i - 1]))
                            {
                                output.Insert(i, HYPHEN);
                                i += 1;
                            }
                        }
                    }

                    if (style != TextTransformation.None)
                    {
                        for (var i = 0; i < output.Length; i++)
                        {
                            if (style == TextTransformation.UPPER)
                            {
                                output[i] = char.ToUpperInvariant(output[i]);
                            }
                            else if (style == TextTransformation.lower)
                            {
                                output[i] = char.ToLowerInvariant(output[i]);
                            }
                        }
                    }

                    if (format.HasFlag(LogFormatFlags.Surrounded))
                    {
                        output.Insert(0, surroundLeft);
                        output.Append(surroundRight);
                    }

                    result = output.ToString();
                    output.Dispose();

                    if (format.HasFlag(LogFormatFlags.Bold))
                    {
                        result = result.Bold();
                    }

                    if (format.HasFlag(LogFormatFlags.Italic))
                    {
                        result = result.Italic();
                    }

                    result = result.Color(_color);

                    _formattedStrings ??= new Dictionary<string, string>();

                    _formattedStrings.Add(input, result);

                    return result;
                }
            }

            private void InvalidateCache()
            {
                using (_PRF_InvalidateCache.Auto())
                {
                    _formattedStrings?.Clear();
                }
            }

            private bool TryGetCachedString(string input, out string output)
            {
                using (_PRF_TryGetCachedString.Auto())
                {
                    _formattedStrings ??= new Dictionary<string, string>();

                    if (_formattedStrings.ContainsKey(input))
                    {
                        output = _formattedStrings[input];
                        return true;
                    }

                    output = null;
                    return false;
                }
            }

            private void Validate()
            {
                using (_PRF_Validate.Auto())
                {
                    if (_color.a == 0f)
                    {
                        _color.a = 1f;
                    }

                    InvalidateCache();
                }
            }

            #region Profiling

            private const string _PRF_PFX = nameof(LogFormat) + ".";

            private static readonly ProfilerMarker _PRF_InvalidateCache =
                new ProfilerMarker(_PRF_PFX + nameof(InvalidateCache));

            private static readonly ProfilerMarker _PRF_TryGetCachedString =
                new ProfilerMarker(_PRF_PFX + nameof(TryGetCachedString));

            private static readonly ProfilerMarker
                _PRF_Format = new ProfilerMarker(_PRF_PFX + nameof(Format));

            private static readonly ProfilerMarker _PRF_Validate =
                new ProfilerMarker(_PRF_PFX + nameof(Validate));

            #endregion
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

            public void DefaultAll()
            {
                critical.color = Color.white;
                critical.format = LogFormatFlags.Bold;
                debug.color = Color.white;
                debug.format = LogFormatFlags.Bold;
                error.color = Color.white;
                error.format = LogFormatFlags.Bold;
                exception.color = Color.white;
                exception.format = LogFormatFlags.Bold;
                fatal.color = Color.white;
                fatal.format = LogFormatFlags.Bold;
                info.color = Color.white;
                info.format = LogFormatFlags.Bold;
                trace.color = Color.white;
                trace.format = LogFormatFlags.Bold;
                warn.color = Color.white;
                warn.format = LogFormatFlags.Bold;
            }
        }

        #endregion

        #region Nested type: Specials

        [Serializable]
        public struct Specials
        {
            #region Fields and Autoproperties

            [PropertyOrder(10)] public LogFormat className;
            [PropertyOrder(40)] public LogFormat exceptionMessage;
            [PropertyOrder(30)] public LogFormat exceptionName;
            [PropertyOrder(20)] public LogFormat filename;
            [PropertyOrder(60)] public LogFormat message;
            [PropertyOrder(50)] public LogFormat structural;

            #endregion

            public void DefaultAll()
            {
                className.color = Colors.Colors.LightGoldenrodYellow;
                className.format = LogFormatFlags.Bold;
                exceptionMessage.color = Colors.Colors.Red4DarkRed;
                exceptionMessage.format = LogFormatFlags.Bold;
                exceptionName.color = Colors.Colors.Red4DarkRed;
                filename.color = Colors.Colors.LightGoldenrodYellow;
                filename.format = LogFormatFlags.Bold;
                message.color = Color.white;
                structural.color = Color.white;
            }
        }

        #endregion

#if UNITY_EDITOR

        [ButtonGroup("1"), PropertyOrder(-10)]
        private void AwakeAll()
        {
            AppaLog.Context.Awake();
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ButtonGroup("1"), PropertyOrder(-10)]
        private void Test()
        {
            AppaLog.Context.Awake();
            AppaLog.ClearDeveloperConsole();
            AppaLog.Test();
            UnityEditor.EditorUtility.SetDirty(this);
        }

        public Vector2 hueRange = new Vector2(90f,        260f);
        public Vector2 saturationRange = new Vector2(20f, 50f);
        public Vector2 valueRange = new Vector2(70f,      100f);

        [ButtonGroup("1"), PropertyOrder(-8)]
        private void Distribute()
        {
            AppaLog.Context.Awake();
            var count = Logging.Contexts.AllContexts.Count - 1;

            var index = 0f;

            foreach (var context in Logging.Contexts.AllContexts)
            {
                var time = Mathf.Clamp01(index / count);

                index += 1f;

                var format = context.GetPrefixFormatInstance();

                var h = hueRange.x + ((hueRange.y - hueRange.x) * time);
                var s = UnityEngine.Random.Range(saturationRange.x, saturationRange.y);
                var v = UnityEngine.Random.Range(valueRange.x,      valueRange.y);

                UpdateContext(context, format, h, s, v);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ButtonGroup("1"), PropertyOrder(-7)]
        private void Randomize()
        {
            AppaLog.Context.Awake();

            foreach (var context in Logging.Contexts.AllContexts)
            {
                var format = context.GetPrefixFormatInstance();

                var h = UnityEngine.Random.Range(hueRange.x,        hueRange.y);
                var s = UnityEngine.Random.Range(saturationRange.x, saturationRange.y);
                var v = UnityEngine.Random.Range(valueRange.x,      valueRange.y);

                UpdateContext(context, format, h, s, v);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ButtonGroup("2"), PropertyOrder(-7)]
        private void Bold()
        {
            AppaLog.Context.Awake();

            foreach (var context in Logging.Contexts.AllContexts)
            {
                var format = context.GetPrefixFormatInstance();

                format.format |= LogFormatFlags.Bold;

                SaveContext(context, format);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ButtonGroup("2"), PropertyOrder(-7)]
        private void Braced()
        {
            AppaLog.Context.Awake();

            foreach (var context in Logging.Contexts.AllContexts)
            {
                var format = context.GetPrefixFormatInstance();

                format.format |= LogFormatFlags.Surrounded;
                format.surroundLeft = "[";
                format.surroundRight = "]";

                SaveContext(context, format);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

        [ButtonGroup("2"), PropertyOrder(-7)]
        private void UPPER()
        {
            AppaLog.Context.Awake();

            foreach (var context in Logging.Contexts.AllContexts)
            {
                var format = context.GetPrefixFormatInstance();

                format.style = TextTransformation.UPPER;

                SaveContext(context, format);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }

        private void SaveContext(AppaLogContext context, LogFormat format)
        {
            context.UpdateFormatInstance(format);
            UnityEditor.EditorUtility.SetDirty(this);
        }

        private void UpdateContext(AppaLogContext context, LogFormat format, float h, float s, float v)
        {
            if (h > 1.0f)
            {
                h /= 360f;
            }

            if (s > 1.0f)
            {
                s /= 100f;
            }

            if (v > 1.0f)
            {
                v /= 100f;
            }

            h = Mathf.Clamp01(h);
            s = Mathf.Clamp01(s);
            v = Mathf.Clamp01(v);

            var newColor = Colors.Colors.HSVToRGB(h, s, v, 1.0f, false);

            format.color = newColor;

            SaveContext(context, format);
        }
#endif
    }
}
