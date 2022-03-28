#region

using System;
using System.Collections.Generic;
using System.Text;
using Appalachia.Utility.Strings;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

namespace Appalachia.Utility.Extensions
{
    public static class TransformExtensions
    {
        #region Constants and Static Readonly

        private static readonly Matrix4x4 _matrix_trs_zero = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.identity,
            Vector3.one
        );

        private static readonly Matrix4x4 _matrix_zero = Matrix4x4.zero;

        #endregion

        public static void DestroyChildren(this Transform transform)
        {
            using (_PRF_DestroyChildren.Auto())
            {
                if (transform == null)
                {
                    return;
                }

                for (var i = 0; i < transform.childCount; ++i)
                {
                    var child = transform.GetChild(i);

                    // Deactivate first because destroy doesn't take effect until the end of the frame...
                    child.gameObject.SetActive(false);
                    Object.Destroy(child.gameObject);
                }
            }
        }

        public static T FindInParents<T>(this Transform transform, bool bIncludeSelf = true)
            where T : Component
        {
            using (_PRF_FindInParents.Auto())
            {
                var current = bIncludeSelf ? transform : transform.parent;
                for (; current != null; current = current.parent)
                {
                    var comp = current.GetComponent<T>();
                    if (comp)
                    {
                        return comp;
                    }
                }

                return null;
            }
        }

        public static string GetFullPath(this Transform transform)
        {
            using (_PRF_FullPath.Auto())
            {
                var sb = new StringBuilder();

                while (transform != null)
                {
                    sb.Insert(0, transform.name);
                    sb.Insert(0, '/');
                    transform = transform.parent;
                }

#if UNITY_EDITOR
                if (transform && EditorUtility.IsPersistent(transform))
                {
                    sb.Append(" (Asset)");
                }
#endif

                return sb.ToString();
            }
        }

        /// <summary>
        ///     Searches for a <see cref="Component" /> or <see cref="MonoBehaviour" /> on the current <see cref="GameObject" />.
        ///     If it exists, it will be assigned to the <paramref name="component" /> argument.
        ///     If it does not exist, it will be created and then assigned to the <paramref name="component" /> argument.
        /// </summary>
        /// <param name="obj">The current game object.</param>
        /// <param name="component">The field we will assign the result to.</param>
        /// <typeparam name="T">The <see cref="Component" /> or <see cref="MonoBehaviour" /> type to search for.</typeparam>
        public static void GetOrAddComponent<T>(this Transform obj, ref T component)
            where T : Component
        {
            using (_PRF_GetOrAddComponent.Auto())
            {
                if (component == null)
                {
                    component = obj.GetComponent<T>();

                    if (component == null)
                    {
                        component = obj.gameObject.AddComponent<T>();
                    }
                }
            }
        }

        public static string GetPathRelativeTo(this GameObject current, GameObject relativeTo)
        {
            using (_PRF_GetPathRelativeTo.Auto())
            {
                return current.transform.GetPathRelativeTo(relativeTo.transform);
            }
        }

        public static string GetPathRelativeTo(this Transform current, Transform relativeTo)
        {
            using (_PRF_GetPathRelativeTo.Auto())
            {
                if (current == relativeTo)
                {
                    return "";
                }

                if (current.IsChildOf(relativeTo))
                {
                    return current.GetFullPath()[(relativeTo.GetFullPath().Length + 1)..];
                }

                return current.GetFullPath();
            }
        }

        public static float4x4 localToParentMatrix(this Transform t)
        {
            using (_PRF_localToParentMatrix.Auto())
            {
                if (t.parent == null)
                {
                    return float4x4.identity;
                }

                return float4x4.TRS(t.localPosition, t.localRotation, t.localScale);
            }
        }

        public static void SetMatrix4x4ToTransform(this Transform t, Matrix4x4 matrix)
        {
            using (_PRF_SetMatrix4x4ToTransform.Auto())
            {
                if (matrix == _matrix_zero)
                {
                    throw new NotSupportedException(ZString.Format("Default matrix for {0}.", t.gameObject.name));
                }

                t.position = matrix.GetColumn(3);
                t.localScale = new Vector3(
                    matrix.GetColumn(0).magnitude,
                    matrix.GetColumn(1).magnitude,
                    matrix.GetColumn(2).magnitude
                );
                t.rotation = Quaternion.LookRotation(matrix.GetColumn(2), matrix.GetColumn(1));
            }
        }

        public static void SetToOrigin(this Transform transform)
        {
            using (_PRF_SetToOrigin.Auto())
            {
                transform.SetMatrix4x4ToTransform(Matrix4x4.identity);
            }
        }

        public static void SortChildren(this Transform transform, Comparison<Transform> comparison = default)
        {
            using (_PRF_SortChildren.Auto())
            {
                var sorted = new List<Transform>();

                for (var childIndex = 0; childIndex < transform.childCount; childIndex++)
                {
                    var child = transform.GetChild(childIndex);

                    sorted.Add(child);
                }

                if (comparison == default)
                {
                    comparison = (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal);
                }

                sorted.Sort(comparison);

                for (var childIndex = 0; childIndex < sorted.Count; childIndex++)
                {
                    var child = sorted[childIndex];

                    child.SetSiblingIndex(childIndex);
                }
            }
        }

        #region Profiling

        private const string _PRF_PFX = nameof(TransformExtensions) + ".";

        private static readonly ProfilerMarker _PRF_GetOrAddComponent =
            new ProfilerMarker(_PRF_PFX + nameof(GetOrAddComponent));

        private static readonly ProfilerMarker _PRF_DestroyChildren =
            new ProfilerMarker(_PRF_PFX + nameof(DestroyChildren));

        private static readonly ProfilerMarker _PRF_FindInParents =
            new ProfilerMarker(_PRF_PFX + nameof(FindInParents));

        private static readonly ProfilerMarker _PRF_FullPath = new ProfilerMarker(_PRF_PFX + nameof(GetFullPath));

        private static readonly ProfilerMarker _PRF_GetPathRelativeTo =
            new ProfilerMarker(_PRF_PFX + nameof(GetPathRelativeTo));

        private static readonly ProfilerMarker _PRF_localToParentMatrix =
            new ProfilerMarker(_PRF_PFX + nameof(localToParentMatrix));

        private static readonly ProfilerMarker _PRF_SetMatrix4x4ToTransform =
            new ProfilerMarker(_PRF_PFX + nameof(SetMatrix4x4ToTransform));

        private static readonly ProfilerMarker _PRF_SortChildren = new ProfilerMarker(_PRF_PFX + nameof(SortChildren));
        private static readonly ProfilerMarker _PRF_SetToOrigin = new ProfilerMarker(_PRF_PFX + nameof(SetToOrigin));

        #endregion
    }
}
