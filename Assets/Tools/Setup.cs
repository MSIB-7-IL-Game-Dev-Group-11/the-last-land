#if UNITY_EDITOR
using UnityEditor;
using static UnityEditor.AssetDatabase;
#endif
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEngine.Application;

namespace TheLastLand.Tools
{
    public static class Setup
    {
#if UNITY_EDITOR
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault(
                "_Project",
                "Animations",
                "Art",
                "Materials",
                "Prefabs",
                "ScriptableObjects",
                "Scripts",
                "Settings"
            );
            Refresh();
        }
#endif

        private static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                var fullpath = Combine(dataPath, root);
                foreach (var folder in folders)
                {
                    var folderPath = Combine(fullpath, folder);
                    if (!Exists(folderPath)) CreateDirectory(folderPath);
                }
            }
        }
    }
}