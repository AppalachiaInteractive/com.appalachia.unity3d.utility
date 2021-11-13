using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appalachia.Utility.Extensions
{
    public static class SceneExtensions
    {
        #region Profiling

        private const string _PRF_PFX = nameof(SceneExtensions) + ".";

        private static readonly ProfilerMarker _PRF_FindGameObjectByPath =
            new ProfilerMarker(_PRF_PFX + nameof(FindGameObjectByPath));

        #endregion

        public static GameObject FindGameObjectByPath(this Scene scene, string absolutePath)
        {
            using (_PRF_FindGameObjectByPath.Auto())
            {
                var paths = absolutePath.Split('/');
                var numPaths = paths.Length;

                if (numPaths < 1)
                {
                    return null;
                }

                if (numPaths < 2)
                {
                    paths = new[] {"/", paths[0]};
                }

                foreach (var gameObject in scene.GetRootObjectsEvenIfNotLoaded())
                {
                    if (gameObject.name != paths[1])
                    {
                        continue;
                    }

                    var transform = gameObject.transform;
                    for (var deep = 2; (deep < numPaths) && transform; ++deep)
                    {
                        transform = transform.Find(paths[deep]);
                    }

                    if (transform)
                    {
                        return transform.gameObject;
                    }
                }

                return null;
            }
        }
    }
}
