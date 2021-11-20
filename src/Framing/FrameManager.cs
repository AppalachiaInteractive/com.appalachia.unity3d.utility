using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Appalachia.Utility.Framing.Extensions;
using Appalachia.Utility.Framing.Targets;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Framing
{
    internal static class FrameManager
    {
        #region Constants and Static Readonly

        private const float QUAT_ROT_CHECK = 1023.5f / 1024.0f;

        #endregion

        #region Static Fields and Autoproperties

        private static List<FramingInput> _inputs;
        private static List<Vector3> framingVectorForwards;
        private static List<Vector3> framingVectorUps;

        #endregion

        public static void CalculateFraming(
            FramedSet set,
            FrameTarget frameTarget,
            FramingDirection direction,
            FramingPerspective perspective,
            bool adjustViewingAngle)
        {
            using (_PRF_CalculateFraming.Auto())
            {
                Initialize();

                var view = FrameableFactory.GetFrameView(frameTarget);
                var viewCamera = view.camera;
                var viewTransform = viewCamera.transform;

                var up = direction switch
                {
                    FramingDirection.Top    => Vector3.forward,
                    FramingDirection.Bottom => Vector3.back,
                    _                       => Vector3.up
                };

                var viewRotation = viewTransform.rotation;
                var aggregatedBounds = new Bounds();

                var isUI = false;

                for (var i = 0; i < set.targets.Count; i++)
                {
                    var go = set.targets[i];

                    ExpandBoundsToFitObject(
                        go,
                        adjustViewingAngle,
                        direction,
                        viewCamera,
                        up,
                        _inputs,
                        ref aggregatedBounds,
                        out var subIsUI
                    );

                    isUI |= subIsUI;
                }

                var parameters = GetFramingForBounds(
                    _inputs,
                    adjustViewingAngle,
                    viewRotation,
                    aggregatedBounds,
                    isUI
                );

                view.LookAt(parameters.position, parameters.direction, parameters.size, perspective);
            }
        }

        private static void ExpandBoundsToFitObject(
            GameObject go,
            bool adjustViewingAngle,
            FramingDirection direction,
            Camera viewCamera,
            Vector3 up,
            List<FramingInput> framingInputs,
            ref Bounds aggregateBounds,
            out bool isUI)
        {
            using (_PRF_ExpandBoundsToFitObject.Auto())
            {
                var objectBounds = go.GetRenderingBounds(out isUI);

                aggregateBounds.Encapsulate(objectBounds);

                if (adjustViewingAngle)
                {
                    var lookDirection = GetLookAtDirection(viewCamera, go.transform, direction, isUI);

                    framingInputs.Add(new FramingInput(lookDirection, up));
                }
            }
        }

        private static FramingParameters GetFramingForBounds(
            List<FramingInput> framingInputs,
            bool adjustViewingAngle,
            Quaternion rotation,
            Bounds bounds,
            bool isUI)
        {
            using (_PRF_GetFramingForBounds.Auto())
            {
                if (adjustViewingAngle)
                {
                    var forward = framingInputs.AverageForward();
                    var up = framingInputs.AverageUp();

                    rotation = Quaternion.LookRotation(forward, up);
                }

                var target = bounds.center;
                var size = bounds.size.y;

                if (isUI)
                {
                    size *= .5f;
                }

                return new FramingParameters(target, rotation, size);
            }
        }

        [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
        private static Vector3 GetLookAtDirection(Camera c, Transform t, FramingDirection dir, bool isUI)
        {
            var current = c.transform.forward;

            using (_PRF_GetLookAtDirection.Auto())
            {
                return dir switch
                {
                    FramingDirection.Current  => current,
                    FramingDirection.Opposite => current * -1,
                    FramingDirection.Front    => (isUI ? -1 : 1) * -t.forward,
                    FramingDirection.Right    => -t.right,
                    FramingDirection.Top      => -t.up,
                    FramingDirection.Back     => (isUI ? -1 : 1) * t.forward,
                    FramingDirection.Left     => t.right,
                    FramingDirection.Bottom   => t.up,
                    _                         => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
                };
            }
        }

        private static void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                _inputs ??= new List<FramingInput>(96);

                _inputs.Clear();
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(FrameManager) + ".";

        private static readonly ProfilerMarker _PRF_Initialize =
            new ProfilerMarker(_PRF_PFX + nameof(Initialize));

        private static readonly ProfilerMarker _PRF_GetLookAtDirection =
            new ProfilerMarker(_PRF_PFX + nameof(GetLookAtDirection));

        private static readonly ProfilerMarker _PRF_ExpandBoundsToFitObject =
            new ProfilerMarker(_PRF_PFX + nameof(ExpandBoundsToFitObject));

        private static readonly ProfilerMarker _PRF_GetFramingForBounds =
            new ProfilerMarker(_PRF_PFX + nameof(GetFramingForBounds));

        private static readonly ProfilerMarker _PRF_CalculateFraming =
            new ProfilerMarker(_PRF_PFX + nameof(CalculateFraming));

        #endregion
    }
}
