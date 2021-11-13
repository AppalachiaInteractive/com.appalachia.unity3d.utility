using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using FormerlySerializedAs = UnityEngine.Serialization.FormerlySerializedAsAttribute;

#if UNITY_EDITOR

#endif

namespace Appalachia.Utility.MultiScene
{
    [ExecuteInEditMode]
    public class AppaMultiSceneSetup : MonoBehaviour, ISerializationCallbackReceiver
    {
        [Serializable]
        public enum LoadMethod
        {
            Baked,
            Additive,
            AdditiveAsync,
            DontLoad
        }

        [Serializable]
        public class SceneEntry
        {
#if UNITY_EDITOR

            public SceneEntry(SceneSetup sceneSetup)
            {
                scene = new AppaSceneReference(sceneSetup.path);

                loadInEditor = sceneSetup.isLoaded;
                loadMethod = LoadMethod.Additive;
            }
#endif

            #region Fields

            [BeginReadonly] public AppaSceneReference scene;

            [Tooltip("Should this be automatically loaded in the Editor?")]
            [FormerlySerializedAs("isLoaded")]
            public bool loadInEditor;

            [EndReadonly]
            [Tooltip("How should we load this scene at Runtime?")]
            public LoadMethod loadMethod;

            public AsyncOperation asyncOp { get; set; }

            #endregion

            public override bool Equals(object obj)
            {
                if (this == obj)
                {
                    return true;
                }

                var other = obj as SceneEntry;
                if (other == null)
                {
                    return false;
                }

                return scene.Equals(other.scene) &&
                       (loadInEditor == other.loadInEditor) &&
                       (loadMethod == other.loadMethod) &&
                       (asyncOp == other.asyncOp);
            }

            public override int GetHashCode()
            {
                return (scene.GetHashCode() * 4) +
                       (loadInEditor.GetHashCode() * 2) +
                       loadMethod.GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(
                    "{0} loadInEditor: {1} loadMethod: {2}",
                    scene.name,
                    loadInEditor,
                    loadMethod
                );
            }
        }

        public enum SceneSetupManagement
        {
            Automatic,
            Manual,
            Disabled
        }

        [Tooltip("When do we save the SceneSetup? See the README.txt (or help above) for more information.")]
        [SerializeField]
        private SceneSetupManagement _sceneSetupMode = SceneSetupManagement.Automatic;

        [SerializeField] private List<SceneEntry> _sceneSetup = new List<SceneEntry>();

        [Obsolete(
            "This variable is deprecated as of AMS v0.91. Please view the README.txt for more information."
        )]
        [SerializeField, HideInInspector]
        private bool _isMainScene = true;

        private const int CurrentVersion = 1;

        [SerializeField, HideInInspector]
        private int _version;

        [Readonly, SerializeField]
        private string _thisScenePath;

#if UNITY_EDITOR
        public SceneSetupManagement sceneSetupMode
        {
            get => _sceneSetupMode;
            set => _sceneSetupMode = value;
        }

        private Coroutine _waitingToBake;
        private List<SceneEntry> _bakedScenesLoading = new List<SceneEntry>();
#endif
        private List<SceneEntry> _bakedScenesMerged = new List<SceneEntry>();

        public static Action<AppaMultiSceneSetup> OnAwake;
        public static Action<AppaMultiSceneSetup> OnStart;
        public static Action<AppaMultiSceneSetup> OnDestroyed;

#if UNITY_EDITOR

        public string scenePath => _thisScenePath;
#endif

#if UNITY_METRO && !UNITY_UWP
		public IList<SceneEntry>	GetSceneSetup()
		{
			return _sceneSetup;
		}
#else
        public System.Collections.ObjectModel.ReadOnlyCollection<SceneEntry> GetSceneSetup()
        {
            return _sceneSetup.AsReadOnly();
        }
#endif

        private void Awake()
        {
            AppaDebug.Log(
                this,
                "{0}.Awake() (Scene {1}). IsLoaded: {2}. Frame: {3}",
                GetType().Name,
                gameObject.scene.name,
                gameObject.scene.isLoaded,
                Time.frameCount
            );

#if UNITY_EDITOR
            CheckVersion();

            if (!BuildPipeline.isBuildingPlayer)
            {
                _thisScenePath = gameObject.scene.path;
            }
#endif

            if (OnAwake != null)
            {
                OnAwake(this);
            }

            if (!Application.isEditor || gameObject.scene.isLoaded || (Time.frameCount > 1))
            {
                LoadSceneSetup();
            }
        }

        private void OnDestroy()
        {
            if (OnDestroyed != null)
            {
                OnDestroyed(this);
            }
        }

        private void Start()
        {
            AppaDebug.Log(
                this,
                "{0}.Start() Scene: {1}. Frame: {2}",
                GetType().Name,
                gameObject.scene.name,
                Time.frameCount
            );

            if (OnStart != null)
            {
                OnStart(this);
            }

            LoadSceneSetup();
        }

        private void CheckVersion()
        {
            if (_version >= CurrentVersion)
            {
                return;
            }

#pragma warning disable 618

            if (!_isMainScene)
            {
                _sceneSetup.Clear();
            }

            _isMainScene = false;
            _version = CurrentVersion;
#pragma warning restore 618
        }

        private void LoadSceneSetup()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                LoadSceneSetupInEditor();
            }
            else
            {
                LoadSceneSetupAtRuntime();
            }
#else
			LoadSceneSetupAtRuntime();
#endif
        }

        private void LoadSceneSetupAtRuntime()
        {
            var sceneSetup = new List<SceneEntry>(_sceneSetup);
            foreach (var entry in sceneSetup)
            {
                LoadEntryAtRuntime(entry);
            }
        }

        private void LoadEntryAtRuntime(SceneEntry entry)
        {
            if (entry.loadMethod == LoadMethod.DontLoad)
            {
                return;
            }

            var existingScene = SceneManager.GetSceneByPath(entry.scene.editorPath);

            if (!existingScene.IsValid())
            {
                existingScene = SceneManager.GetSceneByPath(entry.scene.runtimePath);
            }

#if UNITY_EDITOR

            if (!existingScene.IsValid())
            {
                existingScene = SceneManager.GetSceneByName(entry.scene.runtimePath);
            }

            if (Application.isEditor && (entry.loadMethod == LoadMethod.Baked))
            {
                if (_bakedScenesLoading.Contains(entry) || _bakedScenesMerged.Contains(entry))
                {
                    return;
                }

                _bakedScenesLoading.Add(entry);

                if (!existingScene.IsValid())
                {
#if UNITY_2018_3_OR_NEWER
                    EditorSceneManager.LoadSceneInPlayMode(
                        entry.scene.editorPath,
                        new LoadSceneParameters(LoadSceneMode.Additive)
                    );
#else
					EditorApplication.LoadLevelAdditiveInPlayMode( entry.scene.editorPath );
#endif
                }

                if (_waitingToBake != null)
                {
                    StopCoroutine(_waitingToBake);
                }

                _waitingToBake = StartCoroutine(CoWaitAndBake());
                return;
            }
#endif

            if (existingScene.IsValid())
            {
                return;
            }

            if (entry.loadMethod == LoadMethod.AdditiveAsync)
            {
                AppaDebug.Log(
                    this,
                    "Loading {0} Asynchronously from {1}",
                    entry.scene.name,
                    gameObject.scene.name
                );
                entry.asyncOp = SceneManager.LoadSceneAsync(entry.scene.runtimePath, LoadSceneMode.Additive);
                return;
            }

            if (entry.loadMethod == LoadMethod.Additive)
            {
                AppaDebug.Log(this, "Loading {0} from {1}", entry.scene.name, gameObject.scene.name);
                SceneManager.LoadScene(entry.scene.runtimePath, LoadSceneMode.Additive);
            }
        }

        private void Reset()
        {
            transform.SetAsFirstSibling();
        }

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (!this || !gameObject || BuildPipeline.isBuildingPlayer || Application.isPlaying)
            {
                return;
            }

            if (gameObject.scene.IsValid())
            {
                _thisScenePath = gameObject.scene.path;
            }
#endif
        }

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        private static void OnEditorInit()
        {
            EditorSceneManager.sceneSaving -= OnSceneSaving;
            EditorSceneManager.sceneSaving += OnSceneSaving;
        }

        public static void OnSceneSaving(Scene scene, string path)
        {
            if (!scene.IsValid() || !scene.isLoaded)
            {
                return;
            }

            var instance = scene.GetSceneSingleton<AppaMultiSceneSetup>(true);
            if (!instance)
            {
                return;
            }

            instance.OnBeforeSerialize();

            var isSceneSetupManual = instance._sceneSetupMode == SceneSetupManagement.Manual;
            if (isSceneSetupManual)
            {
                return;
            }

            var newSceneSetup = new List<SceneEntry>();
            var bForceDirty = false;

            var activeScene = SceneManager.GetActiveScene();
            var isSceneSetupAuto = (instance._sceneSetupMode == SceneSetupManagement.Automatic) &&
                                   (activeScene == scene);
            if (isSceneSetupAuto)
            {
                var editorSceneSetup = EditorSceneManager.GetSceneManagerSetup();
                for (var i = 0; i < editorSceneSetup.Length; ++i)
                {
                    var editorEntry = editorSceneSetup[i];
                    if (editorEntry.path == activeScene.path)
                    {
                        continue;
                    }

                    var newEntry = new SceneEntry(editorEntry);
                    newSceneSetup.Add(newEntry);

                    var oldEntry = instance._sceneSetup.Find(x => newEntry.scene.Equals(x.scene));
                    if (oldEntry != null)
                    {
                        newEntry.loadMethod = oldEntry.loadMethod;

                        bForceDirty = bForceDirty ||
                                      (newEntry.scene.runtimePath != oldEntry.scene.runtimePath);
                    }
                }
            }

            if (bForceDirty || !newSceneSetup.SequenceEqual(instance._sceneSetup))
            {
                instance._sceneSetup = newSceneSetup;
                EditorUtility.SetDirty(instance);
                EditorSceneManager.MarkSceneDirty(scene);

                AppaDebug.Log(
                    instance,
                    "SceneSetup for {0} has been updated. If this is unexpected, click here to double-check the entries!",
                    activeScene.name
                );
            }
        }

        private void LoadSceneSetupInEditor()
        {
            foreach (var entry in _sceneSetup)
            {
                LoadEntryInEditor(entry);
            }
        }

        private void LoadEntryInEditor(SceneEntry entry)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                return;
            }

            if (string.IsNullOrEmpty(entry.scene.editorPath) ||
                (entry.scene.editorPath == gameObject.scene.path))
            {
                return;
            }

            var bShouldLoad = entry.loadInEditor && AppaPreferences.AllowAutoload;
            var scene = entry.scene.scene;

            try
            {
                if (!scene.IsValid())
                {
                    if (bShouldLoad)
                    {
                        AppaDebug.Log(
                            this,
                            "Scene {0} is loading Scene {1} in Editor",
                            gameObject.scene.name,
                            entry.scene.name
                        );
                        EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.Additive);
                    }
                    else
                    {
                        AppaDebug.Log(
                            this,
                            "Scene {0} is opening Scene {1} (without loading) in Editor",
                            gameObject.scene.name,
                            entry.scene.name
                        );
                        EditorSceneManager.OpenScene(
                            entry.scene.editorPath,
                            OpenSceneMode.AdditiveWithoutLoading
                        );
                    }
                }
                else if (bShouldLoad != scene.isLoaded)
                {
                    if (bShouldLoad && !scene.isLoaded)
                    {
                        AppaDebug.Log(
                            this,
                            "Scene {0} is loading existing Scene {1} in Editor",
                            gameObject.scene.name,
                            entry.scene.name
                        );
                        EditorSceneManager.OpenScene(entry.scene.editorPath, OpenSceneMode.Additive);
                    }
                    else
                    {
                        AppaDebug.Log(
                            this,
                            "Scene {0} is closing Scene {1} in Editor",
                            gameObject.scene.name,
                            entry.scene.name
                        );
                        EditorSceneManager.CloseScene(scene, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex, this);
            }
        }

        private System.Collections.IEnumerator CoWaitAndBake()
        {
            var bAllLoaded = false;
            while (!bAllLoaded)
            {
                bAllLoaded = true;
                foreach (var entry in _sceneSetup)
                {
                    bAllLoaded = bAllLoaded &&
                                 ((entry.loadMethod == LoadMethod.DontLoad) ||
                                  entry.scene.isLoaded ||
                                  _bakedScenesMerged.Contains(entry));
                }

                if (!bAllLoaded)
                {
                    yield return null;
                }
            }

            foreach (var entry in _sceneSetup)
            {
                if (CanMerge(entry))
                {
                    PreMerge(entry);
                }
            }

            foreach (var entry in _sceneSetup)
            {
                if (CanMerge(entry))
                {
                    MergeScene(entry);
                    _bakedScenesMerged.Add(entry);
                }
            }
        }

        private bool CanMerge(SceneEntry entry)
        {
            if (entry.loadMethod != LoadMethod.Baked)
            {
                return false;
            }

            var scene = entry.scene.scene;
            if (!scene.IsValid())
            {
                return false;
            }

            var activeScene = SceneManager.GetActiveScene();
            if ((scene == activeScene) || (scene == gameObject.scene))
            {
                return false;
            }

            if (!gameObject.scene.isLoaded)
            {
                return false;
            }

            return true;
        }

        private void PreMerge(SceneEntry entry)
        {
            var scene = entry.scene.scene;

            var crossSceneRefs = AppaCrossSceneReferences.GetSceneSingleton(scene, false);
            if (crossSceneRefs)
            {
                crossSceneRefs.ResolvePendingCrossSceneReferences();
            }
        }

        private void MergeScene(SceneEntry entry)
        {
            var scene = entry.scene.scene;

            var sourceSetup = GameObjectEx.GetSceneSingleton<AppaMultiSceneSetup>(scene, false);
            if (sourceSetup)
            {
                Destroy(sourceSetup.gameObject);
            }

            AppaDebug.Log(this, "Merging {0} into {1}", scene.path, gameObject.scene.path);
            SceneManager.MergeScenes(scene, gameObject.scene);
        }
#endif
    }
}
