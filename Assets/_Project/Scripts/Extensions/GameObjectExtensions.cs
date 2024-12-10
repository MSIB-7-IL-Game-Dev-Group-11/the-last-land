using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheLastLand._Project.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Returns the object itself if it exists, null otherwise.
        /// </summary>
        /// <remarks>
        /// This method helps differentiate between a null reference and a destroyed Unity object. Unity's "== null" check
        /// can incorrectly return true for destroyed objects, leading to misleading behaviour. The OrNull method use
        /// Unity's "null check", and if the object has been marked for destruction, it ensures an actual null reference is returned,
        /// aiding in correctly chaining operations and preventing NullReferenceExceptions.
        /// </remarks>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object being checked.</param>
        /// <returns>The object itself if it exists and not destroyed, null otherwise.</returns>
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;

        /// <summary>
        /// Loads an asset from the specified path if the provided asset is null.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load, which must be a ScriptableObject.</typeparam>
        /// <param name="obj">The object calling this extension method.</param>
        /// <param name="asset">The asset to check and potentially load.</param>
        /// <param name="path">The path to load the asset from if it is null.</param>
        /// <returns>The loaded asset if it was null, otherwise the original asset.</returns>
        /// <remarks>
        /// This method helps ensure that a ScriptableObject asset is loaded from a specified path if it is not already assigned.
        /// If the asset is null, it attempts to load it from the given path using the AssetDatabase.
        /// If the asset cannot be loaded, an error message is logged.
        /// </remarks>
#if UNITY_EDITOR
        public static T LoadAssetIfNull<T>(this Object obj, T asset, string path)
            where T : ScriptableObject
        {
            if (asset != null) return asset;

            asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
            {
                Debug.LogError($"Failed to load {typeof(T).Name} from {path}!");
            }

            return asset;
        }
#endif
    }
}