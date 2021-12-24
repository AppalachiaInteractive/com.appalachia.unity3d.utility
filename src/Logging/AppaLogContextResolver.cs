using System.Collections.Generic;
using static Appalachia.Utility.Logging.AppaLog.Context;

namespace Appalachia.Utility.Logging
{
    internal static class AppaLogContextResolver
    {
        #region Static Fields and Autoproperties

        private static ResolutionSet[] _settings;

        #endregion

        public static AppaLogContext Get(System.Type t)
        {
            var typename = t.FullName;

            InitializeSettings();

            for (var dotCount = ResolutionSet.MAX_PARTS; dotCount >= 1; dotCount--)
            {
                for (var index = 0; index < _settings.Length; index++)
                {
                    var setting = _settings[index];

                    if (setting.CanResolve(typename, dotCount))
                    {
                        return setting.context();
                    }
                }
            }

            return Uncategorized;
        }

        private static void InitializeSettings()
        {
            if (_settings != null)
            {
                return;
            }

            _settings = new ResolutionSet[]
            {
                new(() => Application, "Appalachia.Prototype.KOC.Application"),
                new(() => Area, "Appalachia.Prototype.KOC.Application.Areas"),
                new(() => ArrayPooling, "Appalachia.Core.ArrayPooling"),
                new(() => Assets, "Appalachia.Core.Assets"),
                new(() => Assets, "Appalachia.Editor.AssetGeneration"),
                new(() => Audio, "Appalachia.Audio"),
                new(
                    () => Behaviours,
                    "Appalachia.Core.Behaviours",
                    "Appalachia.Prototype.KOC.Application.Behaviours"
                ),
                new(() => CI, "Appalachia.CI", "Appalachia.Editor.CI"),
                new(() => Caching, "Appalachia.Core.Caching"),
                new(() => Character, "Appalachia.Prototype.KOC.Character"),
                new(
                    () => Collections,
                    "Appalachia.Core.Collections",
                    "Appalachia.Prototype.KOC.Application.Collections"
                ),
                new(() => Components, "Appalachia.Prototype.KOC.Application.Components"),
                new(() => ConvexDecomposition, "Appalachia.Spatial.ConvexDecomposition"),
                new(() => Crafting, "Appalachia.Prototype.KOC.Crafting"),
                new(() => Data, "Appalachia.Data"),
                new(() => Data, "Appalachia.Prototype.KOC.Data"),
                new(
                    () => Database,
                    "Appalachia.Data.AccessLayer",
                    "Appalachia.Data.Core.AccessLayer",
                    "Appalachia.Data.Core.Databases"
                ),
                new(() => DebugOverlay, "Appalachia.Prototype.KOC.Application.Areas.DebugOverlay"),
                new(
                    () => DevConsole,
                    "Appalachia.Prototype.KOC.Debugging.DebugConsole",
                    "Appalachia.Prototype.KOC.Debugging.DevConsole"
                ),
                new(() => Editing, "Appalachia.Core.Editing", "Appalachia.Editing"),
                new(() => Editor, "Appalachia.Editor"),
                new(() => Execution, "Appalachia.Core.Execution"),
                new(() => Extensions, "Appalachia.Prototype.KOC.Application.Extensions"),
                new(() => Filtering, "Appalachia.Core.Filtering"),
                new(() => Fire, "Appalachia.Simulation.Core.Metadata.Fuel", "Appalachia.Simulation.Fire"),
                new(() => Game, "Appalachia.Prototype.KOC.Application.Areas.Game"),
                new(
                    () => Gameplay,
                    "Appalachia.Core.Runtime.Gameplay",
                    "Appalachia.Prototype.KOC.Gameplay"
                ),
                new(() => Globals, "Appalachia.Globals"),
                new(() => HUD, "Appalachia.Prototype.KOC.Application.Areas.HUD"),
                new(() => InGameMenu, "Appalachia.Prototype.KOC.Application.Areas.InGameMenu"),
                new(() => Input, "Appalachia.Prototype.KOC.Application.Input"),
                new(() => Inventory, "Appalachia.Prototype.KOC.Inventory"),
                new(() => Jobs, "Appalachia.Jobs"),
                new(() => KOC, "Appalachia.Prototype.KOC"),
                new(() => Labels, "Appalachia.Core.Labels"),
                new(() => Layers, "Appalachia.Core.Layers"),
                new(() => Lighting, "Appalachia.Rendering.Lighting"),
                new(() => LoadingScreen, "Appalachia.Prototype.KOC.Application.Areas.LoadingScreen"),
                new(() => MainMenu, "Appalachia.Prototype.KOC.Application.Areas.MainMenu"),
                new(() => Maintenance, "Appalachia.Editor.CI.Maintenance"),
                new(() => Math, "Appalachia.Core.Math"),
                new(() => MeshBurial, "Appalachia.Spatial.MeshBurial"),
                new(() => Obi, "Appalachia.Simulation.Obi"),
                new(() => ObjectPooling, "Appalachia.Core.ObjectPooling"),
                new(() => Octree, "Appalachia.Spatial.Octree"),
                new(
                    () => Optimization,
                    "Appalachia.Core.Optimization",
                    "Appalachia.Spatial.Optimization"
                ),
                new(() => Overrides, "Appalachia.Core.Overrides"),
                new(() => PauseMenu, "Appalachia.Prototype.KOC.Application.Areas.PauseMenu"),
                new(() => Playables, "Appalachia.Prototype.KOC.Application.Playables"),
                new(
                    () => PostProcessing,
                    "Appalachia.Core.PostProcessing",
                    "Appalachia.Rendering.PostProcessing"
                ),
                new(() => Prefabs, "Appalachia.Rendering.Prefabs"),
                new(() => Preferences, "Appalachia.Core.Preferences"),
                new(
                    () => ReactionSystem,
                    "Appalachia.Core.ReactionSystem",
                    "Appalachia.Simulation.ReactionSystem"
                ),
                new(() => Rendering, "Appalachia.Core.Rendering", "Appalachia.Rendering"),
                new(() => RuntimeGraphs, "Appalachia.Prototype.KOC.Debugging.RuntimeGraphs"),
                new(() => SDF, "Appalachia.Spatial.SDF"),
                new(() => Scriptables, "Appalachia.Prototype.KOC.Application.Scriptables"),
                new(
                    () => Shading,
                    "Appalachia.Core.Shading",
                    "Appalachia.Rendering.Shading",
                    "Appalachia.Shaders"
                ),
                new(() => Shell, "Appalachia.Editor.Shell"),
                new(() => Simulation, "Appalachia.Core.Simulation", "Appalachia.Simulation"),
                new(() => Spatial, "Appalachia.Core.Spatial", "Appalachia.Spatial"),
                new(() => SplashScreen, "Appalachia.Prototype.KOC.Application.Areas.SplashScreen"),
                new(
                    () => StartEnvironment,
                    "Appalachia.Prototype.KOC.Application.Areas.StartEnvironment"
                ),
                new(() => StartScreen, "Appalachia.Prototype.KOC.Application.Areas.StartScreen"),
                new(() => Styling, "Appalachia.Prototype.KOC.Application.Styling"),
                new(() => Terrain, "Appalachia.Spatial.Terrains"),
                new(() => TouchBend, "Appalachia.Core.Runtime.TouchBend"),
                new(
                    () => Trees,
                    "Appalachia.Core.Runtime.Trees",
                    "Appalachia.Simulation.Core.Metadata.Tree",
                    "Appalachia.Simulation.Trees"
                ),
                new(() => VFX, "Appalachia.Rendering.VFX"),
                new(() => Visualizers, "Appalachia.Spatial.Visualizers"),
                new(() => Volumes, "Appalachia.Core.Volumes"),
                new(() => Voxels, "Appalachia.Spatial.Voxels"),
                new(() => Water, "Appalachia.Simulation.Buoyancy"),
                new(() => Wind, "Appalachia.Simulation.Core.Metadata.Wind", "Appalachia.Simulation.Wind"),
            };
        }

        #region Nested type: ResolutionSet

        private class ResolutionSet
        {
            public ResolutionSet(System.Func<AppaLogContext> c, params string[] args)
            {
                context = c;
                _parts = new List<string>[MAX_PARTS];

                for (var i = 0; i < _parts.Length; i++)
                {
                    _parts[i] = new List<string>();
                }

                AddRange(args);
            }

            #region Static Fields and Autoproperties

            public static int MAX_PARTS = 8;

            #endregion

            #region Fields and Autoproperties

            public readonly System.Func<AppaLogContext> context;
            private readonly List<string>[] _parts;

            #endregion

            public bool CanResolve(string typeName, int dotCount)
            {
                var subarray = _parts[dotCount];

                for (var index = 0; index < subarray.Count; index++)
                {
                    var subpart = subarray[index];

                    if (typeName.StartsWith(subpart))
                    {
                        return true;
                    }
                }

                return false;
            }

            private void Add(string ns)
            {
                var dots = 0;

                for (var i = 0; i < ns.Length; i++)
                {
                    var character = ns[i];

                    if (character == '.')
                    {
                        dots += 1;
                    }
                }

                _parts[dots].Add(ns);
            }

            private void AddRange(params string[] args)
            {
                foreach (var arg in args)
                {
                    Add(arg);
                }
            }
        }

        #endregion
    }
}
