using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Appalachia.Utility.Extensions
{
    public static class AssetReferenceExtensions
    {
        public static AsyncOperationHandle<GameObject> Instantiate(
            this AssetReferenceGameObject assetPrefab,
            Action<GameObject> assignment,
            Action<AsyncOperationHandle<GameObject>> completed = null,
            Transform parent = null)
        {
            var operation = assetPrefab.InstantiateAsync();

            operation.Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Failed)
                {
                    assignment?.Invoke(operation.Result);

                    if (parent != null)
                    {
                        operation.Result.SetParentTo(parent);
                    }
                }
            };

            if (completed != null)
            {
                operation.Completed += completed;
            }

            return operation;
        }

        public static AsyncOperationHandle<GameObject> InstantiateIfNull(
            this AssetReferenceGameObject assetPrefab,
            GameObject target,
            Action<GameObject> assignment,
            Action<AsyncOperationHandle<GameObject>> completed = null,
            Transform parent = null)
        {
            if (target == null)
            {
                return assetPrefab.Instantiate(assignment, completed, parent);
            }

            return default;
        }
    }
}
