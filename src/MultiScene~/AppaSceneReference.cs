using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    [System.Serializable]
    public struct AppaSceneReference
    {
        public AppaSceneReference(Scene scene) : this(scene.path)
        {
        }

        public AppaSceneReference(string scenePath)
        {
            var scene = SceneManager.GetSceneByPath(scenePath);
#if UNITY_EDITOR
            editorAssetGUID = UnityEditor.AssetDatabase.AssetPathToGUID(scenePath);
#else
			editorAssetGUID = "";
#endif

            name = scene.name;
            _path = scene.path;
        }

        #region Fields

        public string editorAssetGUID;
        public string name;

        [UnityEngine.Serialization.FormerlySerializedAs("path")]
        [SerializeField]
        private string _path;

        #endregion

        public bool isLoaded => scene.isLoaded;

        public Scene scene
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    var scene = SceneManager.GetSceneByPath(editorPath);
                    if (scene.IsValid())
                    {
                        name = scene.name;
                        _path = scene.path;

                        return scene;
                    }

                    if (BuildPipeline.isBuildingPlayer)
                    {
                        var allMultiSceneSetups = Resources.FindObjectsOfTypeAll<AppaMultiSceneSetup>();
                        foreach (var sceneSetup in allMultiSceneSetups)
                        {
                            if (sceneSetup.scenePath == editorPath)
                            {
                                return sceneSetup.gameObject.scene;
                            }
                        }
                    }

                    return scene;
                }
#endif

                var editorScene = SceneManager.GetSceneByPath(editorPath);
                if (editorScene.IsValid())
                {
                    return editorScene;
                }

                return SceneManager.GetSceneByPath(runtimePath);
            }
        }

        public string runtimePath
        {
            get
            {
                var startIndex = 0;
                var endIndex = _path.Length;

                if (_path.StartsWith("Assets/"))
                {
                    startIndex = "Assets/".Length;
                    endIndex -= startIndex;
                }

                if (_path.EndsWith(".unity"))
                {
                    endIndex -= ".unity".Length;
                }

                return _path.Substring(startIndex, endIndex);
            }
        }

        public string editorPath
        {
            get
            {
#if UNITY_EDITOR

                if (!string.IsNullOrEmpty(editorAssetGUID))
                {
                    return AssetDatabase.GUIDToAssetPath(editorAssetGUID);
                }
#endif

                return _path;
            }

            set { _path = value; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AppaSceneReference))
            {
                return false;
            }

            var otherScene = (AppaSceneReference) obj;
            return editorAssetGUID == otherScene.editorAssetGUID;
        }

        public override int GetHashCode()
        {
            return editorAssetGUID.GetHashCode();
        }

        public bool IsValid()
        {
            return scene.IsValid();
        }
    }
}
