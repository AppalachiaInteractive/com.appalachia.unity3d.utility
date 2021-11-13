using System;
using System.Collections.Generic;
using System.Data;
using Unity.Profiling;

namespace Appalachia.Utility.Framing.Targets
{
    public class FrameableFactory
    {
        #region Profiling

        private const string _PRF_PFX = nameof(FrameableFactory) + ".";

        private static readonly ProfilerMarker _PRF_GetFrameView =
            new ProfilerMarker(_PRF_PFX + nameof(GetFrameView));

        private static readonly ProfilerMarker
            _PRF_Register = new ProfilerMarker(_PRF_PFX + nameof(Register));

        private static readonly ProfilerMarker _PRF_Initialize =
            new ProfilerMarker(_PRF_PFX + nameof(Initialize));

        #endregion

        #region Fields

        private static Dictionary<FrameTarget, Func<IFrameable>> _generators;

        #endregion

        public static IFrameable GetFrameView(FrameTarget frameTarget)
        {
            using (_PRF_GetFrameView.Auto())
            {
                Initialize();

                if (_generators.ContainsKey(frameTarget))
                {
                    return _generators[frameTarget]();
                }

                return frameTarget switch
                {
#if UNITY_EDITOR
                    FrameTarget.SceneView => new SceneViewFrameable(),
#endif
                    FrameTarget.MainCamera => throw new NotImplementedException(),
                    _ => throw new ArgumentOutOfRangeException(nameof(frameTarget), frameTarget, null)
                };
            }
        }

        public static void Register(FrameTarget target, Func<IFrameable> generator)
        {
            using (_PRF_Register.Auto())
            {
                Initialize();
                if (_generators.ContainsKey(target))
                {
                    throw new DuplicateNameException(target.ToString());
                }

                _generators.Add(target, generator);
            }
        }

        private static void Initialize()
        {
            using (_PRF_Initialize.Auto())
            {
                _generators ??= new Dictionary<FrameTarget, Func<IFrameable>>();
            }
        }
    }
}
