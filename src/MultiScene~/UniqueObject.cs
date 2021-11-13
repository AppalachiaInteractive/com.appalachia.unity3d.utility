using System.Collections.Generic;
using System.Reflection;
using Appalachia.Utility.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.MultiScene
{
    public struct UniqueObject
    {
        private const int CurrentSerializedVersion = 1;

        public AppaSceneReference scene;
        public string fullPath;
        public string componentName;
        public int componentIndex;
        public int editorLocalId;
        public string editorPrefabRelativePath;

        [SerializeField, HideInInspector]
        private int version;

        private static List<Component> _reusableComponentsList = new List<Component>();
        private static Scene? _dontDestroyOnLoadScene = new Scene?();

#if !UNITY_EDITOR
		public UniqueObject( Component component )
		{
			if ( !component )
				throw new System.ArgumentNullException( "component" );

			editorLocalId = 0;
			editorPrefabRelativePath = string.Empty;
			version = CurrentSerializedVersion;

			
			var gameObject = component.gameObject;
			scene = new AppaSceneReference( gameObject.scene );
			fullPath = gameObject.GetFullName();

			
			var compType = component.GetType();
			componentName = compType.AssemblyQualifiedName;

			
			gameObject.GetComponents( compType, _reusableComponentsList );
			componentIndex = _reusableComponentsList.IndexOf( component );
		}
#endif

        private static Scene GetDontDestroyOnLoadScene()
        {
            if (!_dontDestroyOnLoadScene.HasValue)
            {
                var temp = new GameObject("AMS-DontDestroyOnLoad-Finder");
                Object.DontDestroyOnLoad(temp);

                _dontDestroyOnLoadScene = temp.scene;
                Object.DestroyImmediate(temp);
            }

            return _dontDestroyOnLoadScene.Value;
        }

        public Object RuntimeResolve()
        {
            var scene = this.scene.scene;

            if (!scene.IsValid())
            {
                return null;
            }

            var gameObject = scene.FindGameObjectByPath(fullPath);

            if (!gameObject)
            {
                gameObject = GameObject.Find(fullPath);

                if (!gameObject)
                {
                    return null;
                }

                AppaDebug.LogWarning(
                    gameObject,
                    "UniqueObject '{0}' resolved unexpected to '{1}'{2}.  Did you move it manually?",
                    this,
                    gameObject.scene.name,
                    gameObject.GetFullName()
                );
            }

            if (string.IsNullOrEmpty(componentName))
            {
                return gameObject;
            }

            if (version < 1)
            {
                var oldStyleComponent = gameObject.GetComponent(componentName);
                if ((componentIndex < 0) || oldStyleComponent)
                {
                    return oldStyleComponent;
                }
            }

            var type = System.Type.GetType(componentName, false);
            if (type != null)
            {
                gameObject.GetComponents(type, _reusableComponentsList);
                if (componentIndex < _reusableComponentsList.Count)
                {
                    return _reusableComponentsList[componentIndex];
                }
            }

            return null;
        }

        public Object Resolve()
        {
            return RuntimeResolve();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is UniqueObject))
            {
                return base.Equals(obj);
            }

            var other = (UniqueObject) obj;
            if (!scene.Equals(other.scene))
            {
                return false;
            }

            if (editorLocalId == other.editorLocalId)
            {
                return true;
            }

            return (componentIndex == other.componentIndex) &&
                   (componentName == other.componentName) &&
                   (fullPath == other.fullPath);
        }

        public override int GetHashCode()
        {
            if (editorLocalId != 0)
            {
                return editorLocalId;
            }

            return (fullPath + componentName + componentIndex).GetHashCode();
        }

        public override string ToString()
        {
            var type = string.IsNullOrEmpty(componentName) ? null : System.Type.GetType(componentName, false);
            return string.Format(
                "{0}'{1}' ({2} #{3})",
                scene.name,
                fullPath,
                type != null ? type.FullName : "GameObject",
                componentIndex
            );
        }

#if UNITY_EDITOR
        private int GetEditorId(Object obj)
        {
            var editorId = 0;

            PropertyInfo inspectorModeInfo = typeof(UnityEditor.SerializedObject).GetProperty(
                "inspectorMode",
                BindingFlags.NonPublic | BindingFlags.Instance
            );
            UnityEditor.SerializedObject sObj = new UnityEditor.SerializedObject(obj);
            inspectorModeInfo.SetValue(sObj, UnityEditor.InspectorMode.Debug, null);

            UnityEditor.SerializedProperty sProp = sObj.FindProperty("m_LocalIdentfierInFile");
            if (sProp != null)
            {
                editorId = sProp.intValue;
                sProp.Dispose();
            }

            sObj.Dispose();

            return editorId;
        }

        internal Object EditorResolveSlow()
        {
            var scene = this.scene.scene;

            if (!scene.IsValid())
            {
                return null;
            }

            if (!scene.isLoaded)
            {
                return null;
            }

            Object[] allObjs = UnityEditor.EditorUtility.CollectDeepHierarchy(scene.GetRootGameObjects());
            foreach (var obj in allObjs)
            {
                if (!obj)
                {
                    continue;
                }

                var prefabObj = UnityEditor.PrefabUtility.GetPrefabInstanceHandle(obj);
                if (prefabObj)
                {
                    var gameObject = obj as GameObject;
                    if (!gameObject)
                    {
                        continue;
                    }

                    if (!UnityEditor.PrefabUtility.IsAnyPrefabInstanceRoot(gameObject))
                    {
                        continue;
                    }

                    if (editorLocalId != GetEditorId(prefabObj))
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(editorPrefabRelativePath))
                    {
                        var transform = gameObject.transform.Find(editorPrefabRelativePath);
                        if (transform)
                        {
                            gameObject = transform.gameObject;
                        }
                        else
                        {
                            Debug.LogWarningFormat(
                                gameObject,
                                "Tried to perform a slow resolve for {0} and found prefab {1}, but could not resolve the expected sub-path {2} which indicates the Prefab instance path was renamed.",
                                this,
                                gameObject,
                                editorPrefabRelativePath
                            );
                        }
                    }

                    Debug.LogWarningFormat(
                        gameObject,
                        "Performed a slow resolve on {0} due to a rename.  We found a PREFAB with same ID named {1} (but we're not sure). Attempting a resolve with it.",
                        this,
                        gameObject
                    );
                    fullPath = gameObject.GetFullName();
                    return RuntimeResolve();
                }

                if (editorLocalId == GetEditorId(obj))
                {
                    GameObject gameObject = obj.EditorGetGameObjectFromComponent();
                    Debug.LogWarningFormat(
                        obj,
                        "Performed a slow resolve on {0} and found {1} ({2}). Double-check this is correct.",
                        this,
                        gameObject ? gameObject.GetFullName() : "(Non-Existant GameObject)",
                        obj.GetType()
                    );
                    return obj;
                }
            }

            return null;
        }

        public UniqueObject(Object obj)
        {
            scene = new AppaSceneReference();
            fullPath = string.Empty;
            componentName = string.Empty;
            version = CurrentSerializedVersion;
            componentIndex = 0;
            editorLocalId = 0;
            editorPrefabRelativePath = string.Empty;

            if (!obj)
            {
                return;
            }

            GameObject gameObject = GameObjectEx.EditorGetGameObjectFromComponent(obj);
            if (gameObject)
            {
                scene = new AppaSceneReference(gameObject.scene);
                fullPath = gameObject.GetFullName();

                var comp = obj as Component;
                if (comp)
                {
                    componentName = obj.GetType().AssemblyQualifiedName;
                    gameObject.GetComponents(obj.GetType(), _reusableComponentsList);
                    componentIndex = _reusableComponentsList.IndexOf(comp);
                }
            }

            var prefabObj = PrefabUtility.GetPrefabInstanceHandle(obj);
            if (prefabObj)
            {
                editorLocalId = GetEditorId(prefabObj);

                var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(obj);
                editorPrefabRelativePath = TransformEx.GetPathRelativeTo(
                    gameObject.transform,
                    prefabRoot.transform
                );
            }

            if (editorLocalId == 0)
            {
                editorLocalId = GetEditorId(obj);
            }
        }
#endif
    }
}
