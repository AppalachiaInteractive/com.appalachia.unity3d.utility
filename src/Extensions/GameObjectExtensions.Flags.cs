using Appalachia.Utility.Enums;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static partial class GameObjectExtensions
    {
        public static GameObject MarkAsDontSave(this GameObject go)
        {
            using (_PRF_MarkAsDontSave.Auto())
            {
                return go.SetHideFlag(HideFlags.DontSave);
            }
        }

        public static GameObject MarkAsDontSaveInBuild(this GameObject go)
        {
            using (_PRF_MarkAsDontSaveInBuild.Auto())
            {
                return go.SetHideFlag(HideFlags.DontSaveInBuild);
            }
        }

        public static GameObject MarkAsDontSaveInEditor(this GameObject go)
        {
            using (_PRF_MarkAsDontSaveInEditor.Auto())
            {
                return go.SetHideFlag(HideFlags.DontSaveInEditor);
            }
        }

        public static GameObject MarkAsDontUnloadUnusedAsset(this GameObject go)
        {
            using (_PRF_MarkAsDontUnloadUnusedAsset.Auto())
            {
                return go.SetHideFlag(HideFlags.DontUnloadUnusedAsset);
            }
        }

        public static GameObject MarkAsEditable(this GameObject go)
        {
            using (_PRF_MarkAsEditable.Auto())
            {
                return go.UnsetHideFlag(HideFlags.NotEditable);
            }
        }

        public static GameObject MarkAsHideAndDontSave(this GameObject go)
        {
            using (_PRF_MarkAsHideAndDontSave.Auto())
            {
                return go.SetHideFlag(HideFlags.HideAndDontSave);
            }
        }

        public static GameObject MarkAsHideInHierarchy(this GameObject go)
        {
            using (_PRF_MarkAsHideInHierarchy.Auto())
            {
                return go.SetHideFlag(HideFlags.HideInHierarchy);
            }
        }

        public static GameObject MarkAsHideInHierarchyAndInspector(this GameObject go)
        {
            using (_PRF_MarkAsHideInHierarchyAndInspector.Auto())
            {
                return go.SetHideFlag(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
            }
        }

        public static GameObject MarkAsHideInInspector(this GameObject go)
        {
            using (_PRF_MarkAsHideInInspector.Auto())
            {
                return go.SetHideFlag(HideFlags.HideInInspector);
            }
        }

        public static GameObject MarkAsNotEditable(this GameObject go)
        {
            using (_PRF_MarkAsNotEditable.Auto())
            {
                return go.SetHideFlag(HideFlags.NotEditable);
            }
        }

        public static GameObject MarkAsSave(this GameObject go)
        {
            using (_PRF_MarkAsSave.Auto())
            {
                return go.UnsetHideFlag(HideFlags.DontSave);
            }
        }

        public static GameObject MarkAsSaveInBuild(this GameObject go)
        {
            using (_PRF_MarkAsSaveInBuild.Auto())
            {
                return go.UnsetHideFlag(HideFlags.DontSaveInBuild);
            }
        }

        public static GameObject MarkAsSaveInEditor(this GameObject go)
        {
            using (_PRF_MarkAsSaveInEditor.Auto())
            {
                return go.UnsetHideFlag(HideFlags.DontSaveInEditor);
            }
        }

        public static GameObject MarkAsShowAndSave(this GameObject go)
        {
            using (_PRF_MarkAsShowAndSave.Auto())
            {
                return go.UnsetHideFlag(HideFlags.HideAndDontSave);
            }
        }

        public static GameObject MarkAsShowInHierarchy(this GameObject go)
        {
            using (_PRF_MarkAsShowInHierarchy.Auto())
            {
                return go.UnsetHideFlag(HideFlags.HideInHierarchy);
            }
        }

        public static GameObject MarkAsShowInHierarchyAndInspector(this GameObject go)
        {
            using (_PRF_MarkAsShowInHierarchyAndInspector.Auto())
            {
                return go.UnsetHideFlag(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
            }
        }

        public static GameObject MarkAsShowInInspector(this GameObject go)
        {
            using (_PRF_MarkAsShowInInspector.Auto())
            {
                return go.UnsetHideFlag(HideFlags.HideInInspector);
            }
        }

        public static GameObject MarkAsUnloadUnusedAsset(this GameObject go)
        {
            using (_PRF_MarkAsUnloadUnusedAsset.Auto())
            {
                return go.UnsetHideFlag(HideFlags.DontUnloadUnusedAsset);
            }
        }

        public static GameObject SetHideFlag(this GameObject go, HideFlags flag)
        {
            using (_PRF_SetHideFlag.Auto())
            {
                var hideFlags = go.hideFlags;

                var newFlags = hideFlags.SetFlag(flag);

                go.hideFlags = newFlags;

                return go;
            }
        }

        public static GameObject UnsetHideFlag(this GameObject go, HideFlags flag)
        {
            using (_PRF_UnsetHideFlag.Auto())
            {
                var hideFlags = go.hideFlags;

                var newFlags = hideFlags.UnsetFlag(flag);

                go.hideFlags = newFlags;

                return go;
            }
        }

        #region Profiling

        private static readonly ProfilerMarker _PRF_MarkAsShowInHierarchyAndInspector =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsShowInHierarchyAndInspector));

        private static readonly ProfilerMarker _PRF_MarkAsHideInHierarchyAndInspector =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsHideInHierarchyAndInspector));

        private static readonly ProfilerMarker _PRF_MarkAsSave =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsSave));

        private static readonly ProfilerMarker _PRF_MarkAsSaveInBuild =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsSaveInBuild));

        private static readonly ProfilerMarker _PRF_MarkAsSaveInEditor =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsSaveInEditor));

        private static readonly ProfilerMarker _PRF_MarkAsUnloadUnusedAsset =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsUnloadUnusedAsset));

        private static readonly ProfilerMarker _PRF_MarkAsShowAndSave =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsShowAndSave));

        private static readonly ProfilerMarker _PRF_MarkAsShowInHierarchy =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsShowInHierarchy));

        private static readonly ProfilerMarker _PRF_MarkAsShowInInspector =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsShowInInspector));

        private static readonly ProfilerMarker _PRF_MarkAsEditable =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsEditable));

        private static readonly ProfilerMarker _PRF_MarkAsDontSaveInEditor =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsDontSaveInEditor));

        private static readonly ProfilerMarker _PRF_MarkAsDontSaveInBuild =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsDontSaveInBuild));

        private static readonly ProfilerMarker _PRF_MarkAsNotEditable =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsNotEditable));

        private static readonly ProfilerMarker _PRF_MarkAsDontUnloadUnusedAsset =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsDontUnloadUnusedAsset));

        private static readonly ProfilerMarker _PRF_MarkAsDontSave =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsDontSave));

        private static readonly ProfilerMarker _PRF_MarkAsHideAndDontSave =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsHideAndDontSave));

        private static readonly ProfilerMarker _PRF_SetHideFlag =
            new ProfilerMarker(_PRF_PFX + nameof(SetHideFlag));

        private static readonly ProfilerMarker _PRF_UnsetHideFlag =
            new ProfilerMarker(_PRF_PFX + nameof(UnsetHideFlag));

        private static readonly ProfilerMarker _PRF_MarkAsHideInHierarchy =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsHideInHierarchy));

        private static readonly ProfilerMarker _PRF_MarkAsHideInInspector =
            new ProfilerMarker(_PRF_PFX + nameof(MarkAsHideInInspector));

        #endregion
    }
}
