using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    internal static class AppaScenePostProcessor
    {
        [UnityEditor.Callbacks.PostProcessScene(-1000)]
        internal static void LoadScenesForMerging()
        {
            var activeScene = new Scene();
            AppaMultiSceneSetup activeSetup = null;
            var bakedScenes = new List<AppaMultiSceneSetup.SceneEntry>();

            GetCommonParameters(ref activeScene, ref activeSetup, bakedScenes);
            if (bakedScenes.Count < 1)
            {
                return;
            }

            AppaDebug.Log(null, "Running LoadScenesForBaking on Scene {0}", activeScene.name);

            foreach (var entry in bakedScenes)
            {
                var realScene = entry.scene.scene;
                if (!realScene.IsValid())
                {
                    realScene = EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.Additive);

                    if (!realScene.IsValid())
                    {
                        AppaDebug.LogError(
                            activeSetup,
                            "BakeScene: Scene {0} ({1}) referenced from Multi-Scene Setup in {2} is invalid.",
                            entry.scene.editorPath,
                            entry.scene.name,
                            activeScene.name
                        );
                        continue;
                    }
                }

                if (!realScene.isLoaded)
                {
                    realScene = EditorSceneManager.OpenScene(realScene.path, OpenSceneMode.Additive);

                    if (!realScene.isLoaded)
                    {
                        AppaDebug.LogError(
                            activeSetup,
                            "BakeScene: Scene {0} ({1}) referenced from Multi-Scene Setup in {2} could not load.",
                            entry.scene.editorPath,
                            entry.scene.name,
                            activeScene.name
                        );
                    }
                }
            }
        }

        private static void GetCommonParameters(
            ref Scene activeScene,
            ref AppaMultiSceneSetup activeSceneSetup,
            List<AppaMultiSceneSetup.SceneEntry> bakedScenes)
        {
            if (!BuildPipeline.isBuildingPlayer)
            {
                return;
            }

            activeScene = SceneManager.GetActiveScene();
            activeSceneSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(activeScene, false);
            if (!activeSceneSetup)
            {
                return;
            }

            var scenesInSetup = activeSceneSetup.GetSceneSetup();
            foreach (var entry in scenesInSetup)
            {
                var bShouldBake = entry.loadMethod == AppaMultiSceneSetup.LoadMethod.Baked;
                if (bShouldBake)
                {
                    bakedScenes.Add(entry);
                }
            }
        }

        [UnityEditor.Callbacks.PostProcessScene(-500)]
        private static void MergeScenes()
        {
            var activeScene = new Scene();
            AppaMultiSceneSetup activeSetup = null;
            var bakedScenes = new List<AppaMultiSceneSetup.SceneEntry>();

            GetCommonParameters(ref activeScene, ref activeSetup, bakedScenes);
            if (bakedScenes.Count < 1)
            {
                return;
            }

            AppaDebug.Log(
                null,
                "Running AMS MergeScenes on Scene {0} ({1})",
                activeScene.name,
                activeSetup.scenePath
            );

            foreach (var entry in bakedScenes)
            {
                if (!entry.scene.isLoaded)
                {
                    AppaDebug.LogError(activeSetup, "Could not merge non-loaded scene: {0}", entry.scene.name);
                    continue;
                }

                var bakedSceneSetup =
                    GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(entry.scene.scene, false);
                if (bakedSceneSetup)
                {
                    AppaCrossSceneReferences.EditorBuildPipelineMergeScene(bakedSceneSetup, activeSetup);
                }

                AppaDebug.Log(
                    null,
                    "Running Unity MergeScenes for {0} into {1}",
                    entry.scene.name,
                    activeScene.name
                );
                SceneManager.MergeScenes(entry.scene.scene, activeScene);
            }
        }

        [UnityEditor.Callbacks.PostProcessScene(-750)]
        private static void RestoreCrossSceneReferences()
        {
            var activeScene = new Scene();
            AppaMultiSceneSetup activeSetup = null;
            var bakedScenes = new List<AppaMultiSceneSetup.SceneEntry>();

            GetCommonParameters(ref activeScene, ref activeSetup, bakedScenes);
            if (bakedScenes.Count < 1)
            {
                return;
            }

            AppaDebug.Log(null, "Running RestoreCrossSceneReferences on Scene {0}", activeScene.name);

            var targetCrossRefs = AppaCrossSceneReferences.GetSceneSingleton(activeScene, false);
            if (targetCrossRefs)
            {
                targetCrossRefs.ResolvePendingCrossSceneReferences();
            }

            foreach (var entry in bakedScenes)
            {
                if (!entry.scene.isLoaded)
                {
                    AppaDebug.LogError(
                        activeSetup,
                        "Could not restore cross-scene references for non-loaded scene: {0}",
                        entry.scene.name
                    );
                    continue;
                }

                var sourceCrossRefs = AppaCrossSceneReferences.GetSceneSingleton(entry.scene.scene, false);
                if (sourceCrossRefs)
                {
                    sourceCrossRefs.ResolvePendingCrossSceneReferences();
                }
            }
        }

        [UnityEditor.Callbacks.PostProcessScene(1)]
        private static void WarnOnAllMissingCrossSceneRefs()
        {
            var activeScene = new Scene();
            AppaMultiSceneSetup activeSetup = null;
            var bakedScenes = new List<AppaMultiSceneSetup.SceneEntry>();

            GetCommonParameters(ref activeScene, ref activeSetup, bakedScenes);
            if (!activeSetup)
            {
                return;
            }

            var crossSceneReferences = activeScene.GetSceneSingleton<AppaCrossSceneReferences>(false);
            if (crossSceneReferences && crossSceneReferences.EditorWarnOnUnresolvedCrossSceneReferences())
            {
                Debug.LogWarningFormat(
                    "Previous Cross-Scene Reference Errors were in {0}",
                    activeSetup.scenePath
                );
            }
        }
    }
}
