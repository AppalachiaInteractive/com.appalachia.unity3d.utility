using System.Collections.Generic;
using Appalachia.Utility.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Appalachia.Utility.MultiScene
{
    [System.Serializable]
    public struct RuntimeCrossSceneReference
    {
        public RuntimeCrossSceneReference(
            Object from,
            string fromField,
            UniqueObject to,
            List<GenericData> data)
        {
            DEPRECATED_fromObject = new UniqueObject();

            _sourceObject = from;
            _sourceField = fromField;
            _toObject = to;
            _data = new GenericDataBundle {data = data};

            _toObjectCached = null;
        }

        #region Fields

        [SerializeField] private GenericDataBundle _data;

        [SerializeField] private Object _sourceObject;

        private Object _toObjectCached;

        [FormerlySerializedAs("_fromField")]
        [SerializeField]
        private string _sourceField;

        [SerializeField] private UniqueObject _toObject;

        [FormerlySerializedAs("_fromObject")]
        [SerializeField, HideInInspector]
        private UniqueObject DEPRECATED_fromObject;

        #endregion

        public List<GenericData> data => _data.data;

        public Object fromObject
        {
            get
            {
                if (!_sourceObject && DEPRECATED_fromObject.scene.IsValid())
                {
                    _sourceObject = DEPRECATED_fromObject.Resolve();

#if UNITY_EDITOR
                    if (!_sourceObject && !Application.isPlaying)
                    {
                        _sourceObject = DEPRECATED_fromObject.EditorResolveSlow();
                    }
#endif

                    if (_sourceObject)
                    {
                        DEPRECATED_fromObject = new UniqueObject();
                    }
                }

                return _sourceObject;
            }
        }

        public Object toObject
        {
            get
            {
                if (!_toObjectCached)
                {
                    _toObjectCached = _toObject.Resolve();

#if UNITY_EDITOR

                    if (!_toObjectCached)
                    {
                        _toObjectCached = _toObject.EditorResolveSlow();
                        if (_toObjectCached)
                        {
                            UnityEditor.EditorUtility.SetDirty(_sourceObject);

                            var gameObject = _sourceObject.EditorGetGameObjectFromComponent();
                            var scene = gameObject
                                ? gameObject.scene
                                : default(UnityEngine.SceneManagement.Scene);
                            if (scene.isLoaded && !scene.isDirty)
                            {
                                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
                                UnityEditor.EditorApplication.delayCall += () =>
                                {
                                    AppaDebug.LogWarning(
                                        gameObject,
                                        "Scene {0} needs to be resaved due to AMS entries being moved (see warnings above)",
                                        scene.name
                                    );
                                };
                            }
                        }
                    }
#endif
                }

                return _toObjectCached;
            }
        }

        public string sourceField => _sourceField;

        public AppaSceneReference DEPRECATED_fromScene
        {
            set
            {
                if (!_sourceObject && DEPRECATED_fromObject.scene.IsValid())
                {
                    DEPRECATED_fromObject.scene = value;
                }
            }
        }

        public AppaSceneReference toScene
        {
            get => _toObject.scene;
            set => _toObject.scene = value;
        }

        public override string ToString()
        {
            return string.Format(
                "{0}.{1} => {2}",
                _sourceObject ? _sourceObject.ToString() : "(null)",
                _sourceField,
                _toObject
            );
        }

        public bool IsSameSource(RuntimeCrossSceneReference other)
        {
            try
            {
                return (fromObject == other.fromObject) && (_sourceField == other._sourceField);
            }
            catch (System.Exception ex)
            {
                AppaDebug.Log(
                    null,
                    "IsSameSource: Could not compare: {0} and {1}: {2}",
                    ToString(),
                    other,
                    ex
                );
            }

            return false;
        }

        #region Nested type: GenericDataBundle

        [System.Serializable]
        private struct GenericDataBundle
        {
            #region Fields

            public List<GenericData> data;

            #endregion
        }

        #endregion
    }
}
