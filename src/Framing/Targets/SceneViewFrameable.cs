using System;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Framing.Targets
{
#if UNITY_EDITOR
    internal class SceneViewFrameable : IFrameable
    {
        #region Profiling

        private const string _PRF_PFX = nameof(SceneViewFrameable) + ".";
        private static readonly ProfilerMarker _PRF_LookAt = new ProfilerMarker(_PRF_PFX + nameof(LookAt));

        #endregion

        public SceneViewFrameable()
        {
            _sceneView = UnityEditor.SceneView.lastActiveSceneView;
        }

        #region Fields

        private readonly UnityEditor.SceneView _sceneView;

        #endregion

        #region IFrameable Members

        public Camera camera => _sceneView.camera;

        public void LookAt(Vector3 point, Quaternion direction, float newSize, FramingPerspective perspective)
        {
            using (_PRF_LookAt.Auto())
            {
                _sceneView.LookAt(point, direction, newSize);

                switch (perspective)
                {
                    case FramingPerspective.Perspective:
                        _sceneView.orthographic = false;
                        break;
                    case FramingPerspective.Orthographic:
                        _sceneView.orthographic = true;
                        break;
                    case FramingPerspective.Current:
                        break;
                    case FramingPerspective.Opposite:
                        _sceneView.orthographic = !_sceneView.orthographic;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(perspective), perspective, null);
                }
            }
        }

        #endregion
    }
#endif
}
