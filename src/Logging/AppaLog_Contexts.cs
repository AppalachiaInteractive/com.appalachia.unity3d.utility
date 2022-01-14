using System;
using Unity.Profiling;

namespace Appalachia.Utility.Logging
{
    public static partial class AppaLog
    {
        #region Nested type: Context

        public static class Context
        {
            #region Static Fields and Autoproperties

            public static AppaLogContext Animal = new(nameof(Animal));
            public static AppaLogContext Animation = new(nameof(Animation));
            public static AppaLogContext Application = new(nameof(Application));
            public static AppaLogContext Area = new(nameof(Area));
            public static AppaLogContext ArrayPooling = new(nameof(ArrayPooling));
            public static AppaLogContext Assets = new(nameof(Assets));
            public static AppaLogContext Audio = new(nameof(Audio));
            public static AppaLogContext Bazooka = new(nameof(Bazooka));
            public static AppaLogContext Behaviours = new(nameof(Behaviours));
            public static AppaLogContext Bootload = new(nameof(Bootload));
            public static AppaLogContext Caching = new(nameof(Caching));
            public static AppaLogContext Character = new(nameof(Character));
            public static AppaLogContext CI = new(nameof(CI));
            public static AppaLogContext Clock = new(nameof(Clock));
            public static AppaLogContext Collections = new(nameof(Collections));
            public static AppaLogContext Components = new(nameof(Components));
            public static AppaLogContext ConvexDecomposition = new(nameof(ConvexDecomposition));
            public static AppaLogContext Core = new(nameof(Core));
            public static AppaLogContext Crafting = new(nameof(Crafting));
            public static AppaLogContext Cursor = new(nameof(Cursor));
            public static AppaLogContext Data = new(nameof(Data));
            public static AppaLogContext Database = new(nameof(Database));
            public static AppaLogContext Dependencies = new(nameof(Dependencies));
            public static AppaLogContext DevConsole = new(nameof(DevConsole));
            public static AppaLogContext DeveloperInterface = new(nameof(DeveloperInterface));
            public static AppaLogContext Editing = new(nameof(Editing));
            public static AppaLogContext Editor = new(nameof(Editor));
            public static AppaLogContext Execution = new(nameof(Execution));
            public static AppaLogContext Extensions = new(nameof(Extensions));
            public static AppaLogContext Filtering = new(nameof(Filtering));
            public static AppaLogContext Fire = new(nameof(Fire));
            public static AppaLogContext FrameEvent = new(nameof(FrameEvent));
            public static AppaLogContext Game = new(nameof(Game));
            public static AppaLogContext Gameplay = new(nameof(Gameplay));
            public static AppaLogContext Globals = new(nameof(Globals));
            public static AppaLogContext HUD = new(nameof(HUD));
            public static AppaLogContext InGameMenu = new(nameof(InGameMenu));
            public static AppaLogContext Initialize = new(nameof(Initialize));
            public static AppaLogContext Input = new(nameof(Input));
            public static AppaLogContext Inventory = new(nameof(Inventory));
            public static AppaLogContext Jobs = new(nameof(Jobs));
            public static AppaLogContext KOC = new(nameof(KOC));
            public static AppaLogContext Labels = new(nameof(Labels));
            public static AppaLogContext Layers = new(nameof(Layers));
            public static AppaLogContext Lifetime = new(nameof(Lifetime));
            public static AppaLogContext Lighting = new(nameof(Lighting));
            public static AppaLogContext LoadingScreen = new(nameof(LoadingScreen));
            public static AppaLogContext Logging = new(nameof(Logging));
            public static AppaLogContext MainMenu = new(nameof(MainMenu));
            public static AppaLogContext Maintenance = new(nameof(Maintenance));
            public static AppaLogContext Math = new(nameof(Math));
            public static AppaLogContext MeshBurial = new(nameof(MeshBurial));
            public static AppaLogContext MeshData = new(nameof(MeshData));
            public static AppaLogContext Obi = new(nameof(Obi));
            public static AppaLogContext Object = new(nameof(Object));
            public static AppaLogContext ObjectPooling = new(nameof(ObjectPooling));
            public static AppaLogContext Octree = new(nameof(Octree));
            public static AppaLogContext Optimization = new(nameof(Optimization));
            public static AppaLogContext Overrides = new(nameof(Overrides));
            public static AppaLogContext PauseMenu = new(nameof(PauseMenu));
            public static AppaLogContext Playables = new(nameof(Playables));
            public static AppaLogContext PostProcessing = new(nameof(PostProcessing));
            public static AppaLogContext PrefabRendering = new(nameof(PrefabRendering));
            public static AppaLogContext Prefabs = new(nameof(Prefabs));
            public static AppaLogContext Preferences = new(nameof(Preferences));
            public static AppaLogContext Prototype = new(nameof(Prototype));
            public static AppaLogContext ReactionSystem = new(nameof(ReactionSystem));
            public static AppaLogContext Rendering = new(nameof(Rendering));
            public static AppaLogContext Repo = new(nameof(Repo));
            public static AppaLogContext RuntimeGraphs = new(nameof(RuntimeGraphs));
            public static AppaLogContext Scriptables = new(nameof(Scriptables));
            public static AppaLogContext SDF = new(nameof(SDF));
            public static AppaLogContext Shading = new(nameof(Shading));
            public static AppaLogContext Shell = new(nameof(Shell));
            public static AppaLogContext Simulation = new(nameof(Simulation));
            public static AppaLogContext Singleton = new(nameof(Singleton));
            public static AppaLogContext Spatial = new(nameof(Spatial));
            public static AppaLogContext SplashScreen = new(nameof(SplashScreen));
            public static AppaLogContext StartEnvironment = new(nameof(StartEnvironment));
            public static AppaLogContext StartScreen = new(nameof(StartScreen));
            public static AppaLogContext Styling = new(nameof(Styling));
            public static AppaLogContext Terrain = new(nameof(Terrain));
            public static AppaLogContext Timeline = new(nameof(Timeline));
            public static AppaLogContext TouchBend = new(nameof(TouchBend));
            public static AppaLogContext Trees = new(nameof(Trees));
            public static AppaLogContext UI => new(nameof(UI));
            public static AppaLogContext Uncategorized = new(nameof(Uncategorized));
            public static AppaLogContext Utility = new(nameof(Utility));
            public static AppaLogContext VFX = new(nameof(VFX));
            public static AppaLogContext Visualizers = new(nameof(Visualizers));
            public static AppaLogContext Volumes = new(nameof(Volumes));
            public static AppaLogContext Voxels = new(nameof(Voxels));
            public static AppaLogContext Water = new(nameof(Water));
            public static AppaLogContext Wind = new(nameof(Wind));

            #endregion

            public static AppaLogContext GetByType(Type t)
            {
                using (_PRF_GetByType.Auto())
                {
                    return AppaLogContextResolver.Get(t);
                }
            }

            public static AppaLogContext GetByType<T>()
            {
                using (_PRF_GetByType.Auto())
                {
                    return GetByType(typeof(T));
                }
            }

            #region Profiling

            private const string _PRF_PFX = nameof(Context) + ".";

            private static readonly ProfilerMarker _PRF_GetByType =
                new ProfilerMarker(_PRF_PFX + nameof(GetByType));

            #endregion
        }

        #endregion
    }
}
