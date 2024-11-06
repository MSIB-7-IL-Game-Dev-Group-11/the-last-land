using System.IO;
using UnityEditor;
using UnityEngine;

namespace TheLastLand.Tools
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault("_Project", "Animations", "Art", "Materials", "Prefabs",
                "ScriptableObjects", "Scripts", "Settings");
            AssetDatabase.Refresh();
        }

        private static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                var fullpath = Path.Combine(Application.dataPath, root);
                foreach (var folder in folders)
                {
                    var folderPath = Path.Combine(fullpath, folder);
                    if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                }
            }
        }
    }
}