using Appalachia.Utility.Enums;
using Unity.Profiling;
using UnityEngine;

namespace Appalachia.Utility.Extensions
{
    public static partial class ComponentExtensions
    {
        public static T MarkAsDontSave<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsDontSave.Auto())
            {
                return component.SetHideFlag(HideFlags.DontSave);
            }
        }

        public static T MarkAsDontSaveInBuild<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsDontSaveInBuild.Auto())
            {
                return component.SetHideFlag(HideFlags.DontSaveInBuild);
            }
        }

        public static T MarkAsDontSaveInEditor<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsDontSaveInEditor.Auto())
            {
                return component.SetHideFlag(HideFlags.DontSaveInEditor);
            }
        }

        public static T MarkAsDontUnloadUnusedAsset<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsDontUnloadUnusedAsset.Auto())
            {
                return component.SetHideFlag(HideFlags.DontUnloadUnusedAsset);
            }
        }

        public static T MarkAsEditable<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsEditable.Auto())
            {
                return component.UnsetHideFlag(HideFlags.NotEditable);
            }
        }

        public static T MarkAsHideAndDontSave<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsHideAndDontSave.Auto())
            {
                return component.SetHideFlag(HideFlags.HideAndDontSave);
            }
        }

        public static T MarkAsHideInHierarchy<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsHideInHierarchy.Auto())
            {
                return component.SetHideFlag(HideFlags.HideInHierarchy);
            }
        }

        public static T MarkAsHideInHierarchyAndInspector<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsHideInHierarchyAndInspector.Auto())
            {
                return component.SetHideFlag(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
            }
        }

        public static T MarkAsHideInInspector<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsHideInInspector.Auto())
            {
                return component.SetHideFlag(HideFlags.HideInInspector);
            }
        }

        public static T MarkAsNotEditable<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsNotEditable.Auto())
            {
                return component.SetHideFlag(HideFlags.NotEditable);
            }
        }

        public static T MarkAsSave<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsSave.Auto())
            {
                return component.UnsetHideFlag(HideFlags.DontSave);
            }
        }

        public static T MarkAsSaveInBuild<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsSaveInBuild.Auto())
            {
                return component.UnsetHideFlag(HideFlags.DontSaveInBuild);
            }
        }

        public static T MarkAsSaveInEditor<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsSaveInEditor.Auto())
            {
                return component.UnsetHideFlag(HideFlags.DontSaveInEditor);
            }
        }

        public static T MarkAsShowAndSave<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsShowAndSave.Auto())
            {
                return component.UnsetHideFlag(HideFlags.HideAndDontSave);
            }
        }

        public static T MarkAsShowInHierarchy<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsShowInHierarchy.Auto())
            {
                return component.UnsetHideFlag(HideFlags.HideInHierarchy);
            }
        }

        public static T MarkAsShowInHierarchyAndInspector<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsShowInHierarchyAndInspector.Auto())
            {
                return component.UnsetHideFlag(HideFlags.HideInHierarchy | HideFlags.HideInInspector);
            }
        }

        public static T MarkAsShowInInspector<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsShowInInspector.Auto())
            {
                return component.UnsetHideFlag(HideFlags.HideInInspector);
            }
        }

        public static T MarkAsUnloadUnusedAsset<T>(this T component)
            where T : Component
        {
            using (_PRF_MarkAsUnloadUnusedAsset.Auto())
            {
                return component.UnsetHideFlag(HideFlags.DontUnloadUnusedAsset);
            }
        }

        public static T SetHideFlag<T>(this T component, HideFlags flag)
            where T : Component
        {
            using (_PRF_SetHideFlag.Auto())
            {
                var hideFlags = component.hideFlags;

                var newFlags = hideFlags.SetFlag(flag);

                component.hideFlags = newFlags;

                return component;
            }
        }

        public static T UnsetHideFlag<T>(this T component, HideFlags flag)
            where T : Component
        {
            using (_PRF_UnsetHideFlag.Auto())
            {
                var hideFlags = component.hideFlags;

                var newFlags = hideFlags.UnsetFlag(flag);

                component.hideFlags = newFlags;

                return component;
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
