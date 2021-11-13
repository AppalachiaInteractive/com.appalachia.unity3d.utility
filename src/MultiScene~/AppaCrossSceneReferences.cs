using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

#endif

namespace Appalachia.Utility.MultiScene
{
    [ExecuteInEditMode]
    public sealed class AppaCrossSceneReferences : MonoBehaviour
#if UNITY_EDITOR
                                                  ,
                                                  ISerializationCallbackReceiver
#endif
    {
#if UNITY_2018_3_OR_NEWER
        private const int CurrentSerializedVersion = 2;
#else
		const int CurrentSerializedVersion = 1;
#endif
        [SerializeField, HideInInspector]
        private int _version = CurrentSerializedVersion;

        [SerializeField]
        private List<RuntimeCrossSceneReference> _crossSceneReferences =
            new List<RuntimeCrossSceneReference>();

        [SerializeField, HideInInspector]
        private List<GameObject> _realSceneRootsForPostBuild = new List<GameObject>();

        [SerializeField, HideInInspector]
        private List<AppaSceneReference> _mergedScenes = new List<AppaSceneReference>();

        private static Dictionary<AppaSceneReference, AppaSceneReference> _activeMergedScenes =
            new Dictionary<AppaSceneReference, AppaSceneReference>();

        private List<RuntimeCrossSceneReference>
            _referencesToResolve = new List<RuntimeCrossSceneReference>();

        public static event System.Action<RuntimeCrossSceneReference> CrossSceneReferenceRestored;

        public static AppaCrossSceneReferences GetSceneSingleton(Scene scene, bool bCreateIfNotFound)
        {
            var multiSceneSetup =
                GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(scene, bCreateIfNotFound);
            if (!multiSceneSetup)
            {
                return null;
            }

            var existing = multiSceneSetup.gameObject.GetComponent<AppaCrossSceneReferences>();
            if (existing)
            {
                return existing;
            }

            if (bCreateIfNotFound)
            {
                return multiSceneSetup.gameObject.AddComponent<AppaCrossSceneReferences>();
            }

            return null;
        }

        public void AddReference(RuntimeCrossSceneReference reference)
        {
            var index = _crossSceneReferences.FindIndex(reference.IsSameSource);
            if (index >= 0)
            {
                _crossSceneReferences[index] = reference;
            }
            else
            {
                _crossSceneReferences.Add(reference);
            }
        }

        public void ResetCrossSceneReferences(Scene toScene)
        {
            _crossSceneReferences.RemoveAll(x => global::x.toScene.scene == toScene);
        }

        private void Awake()
        {
            AppaDebug.Log(
                this,
                "{0}.Awake() Scene: {1}. IsLoaded: {2}. Path: {3}. Frame: {4}. Root Count: {5}",
                GetType().Name,
                gameObject.scene.name,
                gameObject.scene.isLoaded,
                gameObject.scene.path,
                Time.frameCount,
                gameObject.scene.rootCount
            );

            if (_version < CurrentSerializedVersion)
            {
                var numCrossSceneRefs = _crossSceneReferences.Count;
                ConditionalResolveReferences(_crossSceneReferences);

                if (numCrossSceneRefs != _crossSceneReferences.Count)
                {
                    AppaDebug.LogWarning(
                        this,
                        "{0} was upgraded. {1} cross-scene references will be re-created on next save.",
                        name,
                        numCrossSceneRefs - _crossSceneReferences.Count
                    );
                }
                else
                {
                    AppaDebug.LogWarning(
                        this,
                        "{0} needs upgrading.  Please resave {1}",
                        name,
                        gameObject.scene.name
                    );
                }

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var gameObject = this.gameObject;
                    EditorApplication.delayCall += () =>
                    {
                        if (gameObject)
                        {
                            EditorSceneManager.MarkSceneDirty(gameObject.scene);
                        }
                    };
                }
#endif
            }

            var thisScene = new AppaSceneReference(gameObject.scene);
            foreach (var prevScene in _mergedScenes)
            {
                _activeMergedScenes.Add(prevScene, thisScene);
            }

            _referencesToResolve.Clear();
            _referencesToResolve.AddRange(_crossSceneReferences);

            ResolvePendingCrossSceneReferences();
        }

        private void Start()
        {
            AppaDebug.Log(
                this,
                "{0}.Start() Scene: {1}. IsLoaded: {2}. Path: {3}. Frame: {4}. Root Count: {5}",
                GetType().Name,
                gameObject.scene.name,
                gameObject.scene.isLoaded,
                gameObject.scene.path,
                Time.frameCount,
                gameObject.scene.rootCount
            );

            PerformPostBuildCleanup();

            ResolvePendingCrossSceneReferences();

            AppaMultiSceneSetup.OnStart -= HandleNewSceneLoaded;
            AppaMultiSceneSetup.OnStart += HandleNewSceneLoaded;
            AppaMultiSceneSetup.OnDestroyed -= HandleSceneDestroyed;
            AppaMultiSceneSetup.OnDestroyed += HandleSceneDestroyed;
        }

        private void OnDestroy()
        {
            AppaMultiSceneSetup.OnStart -= HandleNewSceneLoaded;
            AppaMultiSceneSetup.OnDestroyed -= HandleSceneDestroyed;

            foreach (var prevScene in _mergedScenes)
            {
                _activeMergedScenes.Remove(prevScene);
            }
        }

        private void HandleNewSceneLoaded(AppaMultiSceneSetup sceneSetup)
        {
            var loadedScene = sceneSetup.gameObject.scene;
            if (!loadedScene.isLoaded)
            {
                Debug.LogErrorFormat(
                    this,
                    "{0} Received HandleNewSceneLoaded from scene {1} which isn't considered loaded.  The scene MUST be considered loaded by this point",
                    GetType().Name,
                    loadedScene.name
                );
            }

            for (var i = 0; i < _crossSceneReferences.Count; ++i)
            {
                var xRef = _crossSceneReferences[i];

                if (!xRef.fromObject)
                {
                    AppaDebug.LogWarning(
                        this,
                        "xRef Index {0} had Null source (probably stale), Consider removing (via right-click on entry)",
                        i
                    );
                    continue;
                }

                if (!_referencesToResolve.Contains(xRef) && (xRef.toScene.scene == loadedScene))
                {
                    _referencesToResolve.Add(xRef);
                }
            }

            if (_referencesToResolve.Count > 0)
            {
                AppaDebug.Log(
                    this,
                    "Scene {0} Loaded. {1} Cross-Scene References (in total) from Cross-Scene Manager in {2} are queued for resolve.",
                    loadedScene.name,
                    _referencesToResolve.Count,
                    gameObject.scene.name
                );
                ConditionalResolveReferences(_referencesToResolve);
            }
        }

        private void HandleSceneDestroyed(AppaMultiSceneSetup sceneSetup)
        {
            var destroyedScene = sceneSetup.gameObject.scene;
            if (!destroyedScene.IsValid())
            {
                return;
            }

            if (destroyedScene == gameObject.scene)
            {
                return;
            }

            _referencesToResolve.RemoveAll(x => x.toScene.scene == destroyedScene);

            var allRelevantRefs = _crossSceneReferences.Where(x => x.toScene.scene == destroyedScene);
            _referencesToResolve.AddRange(allRelevantRefs);
        }

        private System.Collections.IEnumerator CoWaitForSceneLoadThenResolveReferences(Scene scene)
        {
            if (!Application.isPlaying)
            {
                Debug.LogErrorFormat(
                    this,
                    "CoWaitForSceneLoadThenResolveReferences called, but we're not playing. Co-routines do not work reliably in the Editor!"
                );
                yield break;
            }

            if (!scene.IsValid())
            {
                yield break;
            }

            while (!scene.isLoaded)
            {
                yield return null;
            }

            ResolvePendingCrossSceneReferences();
        }

        [ContextMenu("Retry Pending Resolves")]
        public void ResolvePendingCrossSceneReferences()
        {
            ConditionalResolveReferences(_referencesToResolve);
        }

        [ContextMenu("Retry ALL Resolves")]
        private void RetryAllResolves()
        {
            _referencesToResolve.Clear();
            _referencesToResolve.AddRange(_crossSceneReferences);

            ResolvePendingCrossSceneReferences();
        }

        private void ConditionalResolveReferences(List<RuntimeCrossSceneReference> references)
        {
            var fromScene = gameObject.scene;
            for (var i = references.Count - 1; i >= 0; --i)
            {
                var xRef = references[i];

                if (!xRef.fromObject)
                {
                    AppaDebug.LogWarning(
                        this,
                        "Missing Source Object for xRef at (possible index) {0}: {1}",
                        i,
                        xRef
                    );
                    continue;
                }

                try
                {
                    var toScene = xRef.toScene;
                    if (!toScene.IsValid())
                    {
                        AppaSceneReference mergedSceneRedirect;
                        if (_activeMergedScenes.TryGetValue(toScene, out mergedSceneRedirect))
                        {
                            AppaDebug.Log(
                                this,
                                "Redirecting cross scene reference {0} from original target scene {1} to scene {2}",
                                xRef,
                                toScene.name,
                                mergedSceneRedirect.name
                            );
                            toScene = mergedSceneRedirect;
                            xRef.toScene = mergedSceneRedirect;
                        }
                    }

                    AppaDebug.Log(
                        this,
                        "{0}.ConditionalResolveReferences() Scene: {1}. xRef: {2}. fromSceneLoaded: {3}. toSceneLoaded: {4}.",
                        GetType().Name,
                        fromScene.name,
                        xRef,
                        fromScene.isLoaded,
                        toScene.isLoaded
                    );

                    if (toScene.isLoaded)
                    {
                        references.RemoveAt(i);

                        AppaDebug.Log(this, "Restoring Cross-Scene Reference {0}", xRef);
                        AppaCrossSceneReferenceResolver.Resolve(xRef);

                        if (CrossSceneReferenceRestored != null)
                        {
                            CrossSceneReferenceRestored(xRef);
                        }
                    }
                }
                catch (ResolveException ex)
                {
                    var message = ex.Message;

                    var context = xRef.fromObject;
                    if (!context)
                    {
                        context = this;
                    }

                    Debug.LogErrorFormat(
                        context,
                        "{0} in {1}: {2}",
                        GetType().Name,
                        gameObject.scene.name,
                        message
                    );
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex, this);
                }
            }
        }

        private void PerformPostBuildCleanup()
        {
            if (Application.isEditor && !Application.isPlaying && (_realSceneRootsForPostBuild.Count > 0))
            {
                var newSceneRoots = gameObject.scene.GetRootGameObjects();
                foreach (var root in newSceneRoots)
                {
                    if (!_realSceneRootsForPostBuild.Contains(root))
                    {
                        AppaDebug.LogWarning(
                            this,
                            "Destroying '{0}/{1}' since we've determined it's a temporary for a cross-scene reference",
                            gameObject.scene.name,
                            root.name
                        );
                        DestroyImmediate(root);
                    }
                }

                _realSceneRootsForPostBuild.Clear();
            }
        }

#if UNITY_EDITOR

        public void OnAfterDeserialize()
        {
            AppaMultiSceneSetup.OnStart -= HandleNewSceneLoaded;
            AppaMultiSceneSetup.OnStart += HandleNewSceneLoaded;

            AppaMultiSceneSetup.OnDestroyed -= HandleSceneDestroyed;
            AppaMultiSceneSetup.OnDestroyed += HandleSceneDestroyed;
        }

        public void OnBeforeSerialize()
        {
            _version = CurrentSerializedVersion;

            if (!BuildPipeline.isBuildingPlayer)
            {
                return;
            }

            if (SceneManager.sceneCount < 2)
            {
                return;
            }

            ResolvePendingCrossSceneReferences();

            gameObject.scene.GetRootGameObjects(_realSceneRootsForPostBuild);
        }

        public static void EditorBuildPipelineMergeScene(
            AppaMultiSceneSetup sourceSceneSetup,
            AppaMultiSceneSetup destSceneSetup)
        {
            var amsFromSceneRef = new AppaSceneReference(sourceSceneSetup.gameObject.scene);
            amsFromSceneRef.editorPath = sourceSceneSetup.scenePath;

            var amsIntoSceneRef = new AppaSceneReference(destSceneSetup.gameObject.scene);
            amsIntoSceneRef.editorPath = destSceneSetup.scenePath;

            var srcCrossSceneRefs = GetSceneSingleton(sourceSceneSetup.gameObject.scene, false);
            if (!srcCrossSceneRefs)
            {
                return;
            }

            var destCrossSceneRefs = GetSceneSingleton(destSceneSetup.gameObject.scene, true);

            for (var i = 0; i < srcCrossSceneRefs._crossSceneReferences.Count; ++i)
            {
                var xRef = srcCrossSceneRefs._crossSceneReferences[i];
                if (!srcCrossSceneRefs._referencesToResolve.Contains(xRef))
                {
                    AppaDebug.Log(srcCrossSceneRefs, "Already resolved xRef {0}. No need to merge it.", xRef);
                    continue;
                }

                AppaDebug.Log(destSceneSetup, "Merging {0} into Scene {1}", xRef, amsIntoSceneRef.editorPath);
                xRef.DEPRECATED_fromScene = amsIntoSceneRef;
                destCrossSceneRefs.AddReference(xRef);
            }

            destCrossSceneRefs._mergedScenes.Add(amsFromSceneRef);

            DestroyImmediate(srcCrossSceneRefs.gameObject, false);
        }

        public bool EditorWarnOnUnresolvedCrossSceneReferences()
        {
            foreach (var xRef in _referencesToResolve)
            {
                Debug.LogWarningFormat("Did not resolve Cross-Scene Reference during build: {0}", xRef);
            }

            return _referencesToResolve.Count > 0;
        }
#endif
    }
}
