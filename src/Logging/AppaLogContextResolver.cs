using System.Collections.Generic;

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

            for (var dotCount = ResolutionSet.MAX_PARTS - 1; dotCount >= 1; dotCount--)
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

            return AppaLog.Context.Uncategorized;
        }

        private static void InitializeSettings()
        {
            if (_settings != null)
            {
                return;
            }

            _settings = new ResolutionSet[]
            {
                new(
                    () => AppaLog.Context.Behaviours,
                    "Appalachia.Core.Objects.Root.AppalachiaBehaviour",
                    "Appalachia.Core.Objects.Root.SingletonAppalachiaBehaviour",
                    "Appalachia.Core.Behaviours",
                    "Appalachia.Prototype.KOC.Application.Behaviours"
                ),
                new(() => AppaLog.Context.Repo, "Appalachia.Core.Objects.Root.AppalachiaRepository"),
                new(
                    () => AppaLog.Context.Object,
                    "Appalachia.Core.Objects.Root.AppalachiaObject",
                    "Appalachia.Core.Objects.Root.SingletonAppalachiaObject"
                ),
                new(() => AppaLog.Context.Application, "Appalachia.Prototype.KOC.Application"),
                new(() => AppaLog.Context.Area, "Appalachia.Prototype.KOC.Application.Areas"),
                new(() => AppaLog.Context.ArrayPooling, "Appalachia.Core.ArrayPooling"),
                new(() => AppaLog.Context.Assets, "Appalachia.Core.Assets"),
                new(() => AppaLog.Context.Assets, "Appalachia.Editor.AssetGeneration"),
                new(() => AppaLog.Context.Audio, "Appalachia.Audio"),
                new(() => AppaLog.Context.CI, "Appalachia.CI", "Appalachia.Editor.CI"),
                new(() => AppaLog.Context.Caching, "Appalachia.Core.Caching"),
                new(() => AppaLog.Context.Character, "Appalachia.Prototype.KOC.Character"),
                new(
                    () => AppaLog.Context.Collections,
                    "Appalachia.Core.Collections",
                    "Appalachia.Prototype.KOC.Application.Collections"
                ),
                new(() => AppaLog.Context.Components, "Appalachia.Prototype.KOC.Application.Components"),
                new(() => AppaLog.Context.ConvexDecomposition, "Appalachia.Spatial.ConvexDecomposition"),
                new(() => AppaLog.Context.Crafting, "Appalachia.Prototype.KOC.Crafting"),
                new(() => AppaLog.Context.Data, "Appalachia.Data", "Appalachia.Prototype.KOC.Data"),
                new(
                    () => AppaLog.Context.Database,
                    "Appalachia.Data.AccessLayer",
                    "Appalachia.Data.Core.AccessLayer",
                    "Appalachia.Data.Core.Databases"
                ),
                new(
                    () => AppaLog.Context.DeveloperInterface,
                    "Appalachia.Prototype.KOC.Application.Areas.DeveloperInterface"
                ),
                new(
                    () => AppaLog.Context.DevConsole,
                    "Appalachia.Prototype.KOC.Debugging.DebugConsole",
                    "Appalachia.Prototype.KOC.Debugging.DevConsole"
                ),
                new(() => AppaLog.Context.Editing, "Appalachia.Core.Editing", "Appalachia.Editing"),
                new(() => AppaLog.Context.Editor, "Appalachia.Editor"),
                new(() => AppaLog.Context.Execution, "Appalachia.Core.Execution"),
                new(() => AppaLog.Context.Extensions, "Appalachia.Prototype.KOC.Application.Extensions"),
                new(() => AppaLog.Context.Filtering, "Appalachia.Core.Filtering"),
                new(
                    () => AppaLog.Context.Fire,
                    "Appalachia.Simulation.Core.Metadata.Fuel",
                    "Appalachia.Simulation.Fire"
                ),
                new(() => AppaLog.Context.Game, "Appalachia.Prototype.KOC.Application.Areas.Game"),
                new(
                    () => AppaLog.Context.Gameplay,
                    "Appalachia.Core.Runtime.Gameplay",
                    "Appalachia.Prototype.KOC.Gameplay"
                ),
                new(() => AppaLog.Context.Globals, "Appalachia.Globals"),
                new(() => AppaLog.Context.HUD, "Appalachia.Prototype.KOC.Application.Areas.HUD"),
                new(
                    () => AppaLog.Context.InGameMenu,
                    "Appalachia.Prototype.KOC.Application.Areas.InGameMenu"
                ),
                new(() => AppaLog.Context.Input, "Appalachia.Prototype.KOC.Application.Input"),
                new(() => AppaLog.Context.Inventory, "Appalachia.Prototype.KOC.Inventory"),
                new(() => AppaLog.Context.Jobs, "Appalachia.Jobs"),
                new(() => AppaLog.Context.KOC, "Appalachia.Prototype.KOC"),
                new(() => AppaLog.Context.Labels, "Appalachia.Core.Labels"),
                new(() => AppaLog.Context.Layers, "Appalachia.Core.Layers"),
                new(() => AppaLog.Context.Lighting, "Appalachia.Rendering.Lighting"),
                new(
                    () => AppaLog.Context.LoadingScreen,
                    "Appalachia.Prototype.KOC.Application.Areas.LoadingScreen"
                ),
                new(() => AppaLog.Context.MainMenu, "Appalachia.Prototype.KOC.Application.Areas.MainMenu"),
                new(() => AppaLog.Context.Maintenance, "Appalachia.Editor.CI.Maintenance"),
                new(() => AppaLog.Context.Math, "Appalachia.Core.Math"),
                new(() => AppaLog.Context.MeshBurial, "Appalachia.Spatial.MeshBurial"),
                new(() => AppaLog.Context.Obi, "Appalachia.Simulation.Obi"),
                new(() => AppaLog.Context.ObjectPooling, "Appalachia.Core.ObjectPooling"),
                new(() => AppaLog.Context.Octree, "Appalachia.Spatial.Octree"),
                new(
                    () => AppaLog.Context.Optimization,
                    "Appalachia.Core.Optimization",
                    "Appalachia.Spatial.Optimization"
                ),
                new(() => AppaLog.Context.Overrides, "Appalachia.Core.Overrides"),
                new(() => AppaLog.Context.PauseMenu, "Appalachia.Prototype.KOC.Application.Areas.PauseMenu"),
                new(() => AppaLog.Context.Playables, "Appalachia.Prototype.KOC.Application.Playables"),
                new(
                    () => AppaLog.Context.PostProcessing,
                    "Appalachia.Core.PostProcessing",
                    "Appalachia.Rendering.PostProcessing"
                ),
                new(() => AppaLog.Context.Prefabs, "Appalachia.Rendering.Prefabs"),
                new(() => AppaLog.Context.Preferences, "Appalachia.Core.Preferences"),
                new(
                    () => AppaLog.Context.ReactionSystem,
                    "Appalachia.Core.ReactionSystem",
                    "Appalachia.Simulation.ReactionSystem"
                ),
                new(() => AppaLog.Context.Rendering, "Appalachia.Core.Rendering", "Appalachia.Rendering"),
                new(() => AppaLog.Context.RuntimeGraphs, "Appalachia.Prototype.KOC.Debugging.RuntimeGraphs"),
                new(() => AppaLog.Context.SDF, "Appalachia.Spatial.SDF"),
                new(() => AppaLog.Context.Scriptables, "Appalachia.Prototype.KOC.Application.Scriptables"),
                new(
                    () => AppaLog.Context.Shading,
                    "Appalachia.Core.Shading",
                    "Appalachia.Rendering.Shading",
                    "Appalachia.Shaders"
                ),
                new(() => AppaLog.Context.Shell, "Appalachia.Editor.Shell"),
                new(() => AppaLog.Context.Simulation, "Appalachia.Core.Simulation", "Appalachia.Simulation"),
                new(() => AppaLog.Context.Spatial, "Appalachia.Core.Spatial", "Appalachia.Spatial"),
                new(
                    () => AppaLog.Context.SplashScreen,
                    "Appalachia.Prototype.KOC.Application.Areas.SplashScreen"
                ),
                new(
                    () => AppaLog.Context.StartEnvironment,
                    "Appalachia.Prototype.KOC.Application.Areas.StartEnvironment"
                ),
                new(
                    () => AppaLog.Context.StartScreen,
                    "Appalachia.Prototype.KOC.Application.Areas.StartScreen"
                ),
                new(() => AppaLog.Context.Styling, "Appalachia.Prototype.KOC.Application.Styling"),
                new(() => AppaLog.Context.Terrain, "Appalachia.Spatial.Terrains"),
                new(() => AppaLog.Context.TouchBend, "Appalachia.Core.Runtime.TouchBend"),
                new(
                    () => AppaLog.Context.Trees,
                    "Appalachia.Core.Runtime.Trees",
                    "Appalachia.Simulation.Core.Metadata.Tree",
                    "Appalachia.Simulation.Trees"
                ),
                new(() => AppaLog.Context.VFX, "Appalachia.Rendering.VFX"),
                new(() => AppaLog.Context.Visualizers, "Appalachia.Spatial.Visualizers"),
                new(() => AppaLog.Context.Volumes, "Appalachia.Core.Volumes"),
                new(() => AppaLog.Context.Voxels, "Appalachia.Spatial.Voxels"),
                new(() => AppaLog.Context.Water, "Appalachia.Simulation.Buoyancy"),
                new(
                    () => AppaLog.Context.Wind,
                    "Appalachia.Simulation.Core.Metadata.Wind",
                    "Appalachia.Simulation.Wind"
                ),
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
