#region

using System.Collections.Generic;
using System.Linq;
using Appalachia.Utility.Framing.Extensions;
using Unity.Profiling;
using UnityEngine;

#endregion

namespace Appalachia.Utility.Framing
{
    public static class FramingExtensions
    {
        #region Profiling

        private const string _PRF_PFX = nameof(FramingExtensions) + ".";

        private static readonly ProfilerMarker _PRF_Frame = new ProfilerMarker(_PRF_PFX + nameof(Frame));

        private static readonly ProfilerMarker _PRF_GetRenderingBounds =
            new ProfilerMarker(_PRF_PFX + nameof(GetRenderingBounds));

        #endregion

        #region Fields

        private static Dictionary<int, Bounds> _cachedBounds;
        private static Dictionary<int, bool> _cachedIsUI;
        private static Dictionary<int, int> _cachedChildCount;
        private static Dictionary<int, Matrix4x4> _cachedMatrices;

        private static Vector3[] _cornersArray;

        #endregion

        public static void Frame(
            this GameObject go,
            FrameTarget frameTarget,
            bool adjustViewingAngle = true,
            FramingDirection direction = FramingDirection.Current,
            FramingPerspective perspective = FramingPerspective.Current)
        {
            using (_PRF_Frame.Auto())
            {
                FrameManager.CalculateFraming(
                    go.ToFramedSet(),
                    frameTarget,
                    direction,
                    perspective,
                    adjustViewingAngle
                );
            }
        }

        public static void Frame(
            this GameObject[] gos,
            FrameTarget frameTarget,
            bool adjustViewingAngle = true,
            FramingDirection direction = FramingDirection.Current,
            FramingPerspective perspective = FramingPerspective.Current)
        {
            using (_PRF_Frame.Auto())
            {
                FrameManager.CalculateFraming(
                    gos.ToFramedSet(),
                    frameTarget,
                    direction,
                    perspective,
                    adjustViewingAngle
                );
            }
        }

        public static void Frame(
            this IEnumerable<GameObject> gos,
            FrameTarget frameTarget,
            bool adjustViewingAngle = true,
            FramingDirection direction = FramingDirection.Current,
            FramingPerspective perspective = FramingPerspective.Current)
        {
            using (_PRF_Frame.Auto())
            {
                FrameManager.CalculateFraming(
                    gos.ToFramedSet(),
                    frameTarget,
                    direction,
                    perspective,
                    adjustViewingAngle
                );
            }
        }

        public static void Frame(
            this IReadOnlyList<GameObject> gos,
            FrameTarget frameTarget,
            bool adjustViewingAngle = true,
            FramingDirection direction = FramingDirection.Current,
            FramingPerspective perspective = FramingPerspective.Current)
        {
            using (_PRF_Frame.Auto())
            {
                FrameManager.CalculateFraming(
                    gos.ToFramedSet(),
                    frameTarget,
                    direction,
                    perspective,
                    adjustViewingAngle
                );
            }
        }

        public static Bounds GetRenderingBounds(this GameObject gameObject, out bool isUI)
        {
            using (_PRF_GetRenderingBounds.Auto())
            {
                var instanceId = gameObject.GetInstanceID();
                var matrix = gameObject.transform.localToWorldMatrix;
                var childCount = gameObject.transform.childCount;

                Initialize();

                if (_cachedBounds.ContainsKey(instanceId))
                {
                    var cachedMatrix = _cachedMatrices[instanceId];
                    var cachedChildCount = _cachedChildCount[instanceId];

                    if ((matrix == cachedMatrix) && (childCount == cachedChildCount))
                    {
                        isUI = _cachedIsUI[instanceId];
                        return _cachedBounds[instanceId];
                    }
                }
                else
                {
                    _cachedMatrices.Add(instanceId, matrix);
                    _cachedChildCount.Add(instanceId, childCount);
                }

                Bounds bounds = default;

                var rectTransforms = gameObject.GetComponentsInChildren<RectTransform>();

                if ((rectTransforms != null) && (rectTransforms.Length != 0))
                {
                    isUI = true;
                    foreach (var rectTransform in rectTransforms)
                    {
                        rectTransform.GetWorldCorners(_cornersArray);

                        bounds.Encapsulate(_cornersArray[0]);
                        bounds.Encapsulate(_cornersArray[1]);
                        bounds.Encapsulate(_cornersArray[2]);
                        bounds.Encapsulate(_cornersArray[3]);
                    }
                }
                else
                {
                    isUI = false;
                    var renderers = gameObject.GetComponentsInChildren<Renderer>();
                    var colliders = gameObject.GetComponentsInChildren<Collider>().Where(c => !c.isTrigger);

                    bounds = new Bounds {center = gameObject.transform.position, size = Vector3.zero};

                    foreach (var renderer in renderers)
                    {
                        bounds.Encapsulate(renderer.bounds);
                    }

                    foreach (var collider in colliders)
                    {
                        bounds.Encapsulate(collider.bounds);
                    }
                }

                _cachedIsUI.Add(instanceId, isUI);
                _cachedBounds.Add(instanceId, bounds);

                return bounds;
            }
        }

        private static void Initialize()
        {
            _cachedIsUI ??= new Dictionary<int, bool>();
            _cachedBounds ??= new Dictionary<int, Bounds>();
            _cachedMatrices ??= new Dictionary<int, Matrix4x4>();
            _cachedChildCount ??= new Dictionary<int, int>();
            _cornersArray ??= new Vector3[4];
        }
    }
}
