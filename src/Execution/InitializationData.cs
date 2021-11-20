using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Execution
{
    [Serializable]
    public struct InitializationData
    {
        #region Constants and Static Readonly

        private const string GROUP = "Internal";

        #endregion

        #region Fields and Autoproperties

        [NonSerialized] private HashSet<string> _tagsHash;

        [HideInInspector, SerializeField]
        private List<string> _tags;

        [NonSerialized] private IInitializable _object;

        [HideInInspector, SerializeField]
        private string _resetToken;

        public string ResetToken => _resetToken;

        #endregion

        public bool HasInitialized(string tag)
        {
            using (_PRF_HasInitialized.Auto())
            {
                InitializeInternal();

                return _tagsHash.Contains(tag);
            }
        }

        public void Initialize<T>(T obj, string tag, Action action)
        where T : UnityEngine.Object, IInitializable
        {
            using (_PRF_Initialize.Auto())
            {
                _object = obj;

                if (HasInitialized(tag))
                {
                    return;
                }

                action();

                MarkInitialized(tag);

                SetDirty(obj);
            }
        }

        public void Initialize<T>(T obj, string tag, bool force, Action action)
            where T : UnityEngine.Object, IInitializable
        {
            using (_PRF_Initialize.Auto())
            {
                _object = obj;

                if (!force && HasInitialized(tag))
                {
                    return;
                }

                action();

                MarkInitialized(tag);

                SetDirty(obj);
            }
        }

        public void MarkInitialized(string tag)
        {
            using (_PRF_MarkInitialized.Auto())
            {
                InitializeInternal();

                if (!_tagsHash.Contains(tag))
                {
                    _tagsHash.Add(tag);
                    _tags.Add(tag);
                }
            }
        }

        public void Reset<T>(T obj, string token)
            where T : UnityEngine.Object, IInitializable
        {
            using (_PRF_Reset.Auto())
            {
                _object = obj;

                if (ResetToken == token)
                {
                    return;
                }

                _resetToken = token;

                ResetDirectly(obj);
            }
        }

        private void InitializeInternal()
        {
            using (_PRF_InitializeInternal.Auto())
            {
                _tags ??= new List<string>();
                _tagsHash ??= new HashSet<string>(_tags);
            }
        }

        private void ResetDirectly(IInitializable obj)
        {
            using (_PRF_ResetDirectly.Auto())
            {
                _tags = new List<string>();
                _tagsHash = new HashSet<string>();

                SetDirty(obj);
            }
        }

        /*[GUIColor(nameof(GetGUIColor))]*/
        [Button, ButtonGroup(GROUP)]
        private void ResetInitializationData()
        {
            using (_PRF_Reset.Auto())
            {
                ResetDirectly(_object);
            }
        }

        /*private Color GetGUIColor()
        {
            return Appalachia.Utility.Colors.Colors.Appalachia.Yellow;
        }*/

        private void SetDirty(IInitializable obj)
        {
            _object = obj;
#if UNITY_EDITOR
            if ((obj != null) && obj is UnityEngine.Object tobj)
            {
                UnityEditor.EditorUtility.SetDirty(tobj);
            }
#endif
        }

        #region Profiling

        private const string _PRF_PFX = nameof(InitializationData) + ".";

        private static readonly ProfilerMarker _PRF_ResetDirectly =
            new ProfilerMarker(_PRF_PFX + nameof(ResetDirectly));

        private static readonly ProfilerMarker _PRF_InitializeInternal =
            new ProfilerMarker(_PRF_PFX + nameof(InitializeInternal));

        private static readonly ProfilerMarker _PRF_Reset =
            new ProfilerMarker(_PRF_PFX + nameof(ResetInitializationData));

        private static readonly ProfilerMarker _PRF_Initialize =
            new ProfilerMarker(_PRF_PFX + nameof(Initialize));

        private static readonly ProfilerMarker _PRF_HasInitialized =
            new ProfilerMarker(_PRF_PFX + nameof(HasInitialized));

        private static readonly ProfilerMarker _PRF_MarkInitialized =
            new ProfilerMarker(_PRF_PFX + nameof(MarkInitialized));

        #endregion
    }
}
