using UnityEngine;

namespace TheLastLand._Project.Scripts.MainMenu
{
    public static class GameProgressManager
    {
        private const string PlayerPositionKey = "PlayerPosition";
        private const string CurrentLevelKey = "CurrentLevel";

        public static void SaveProgress(Vector3 playerPosition, string currentLevel)
        {
            PlayerPrefs.SetString(PlayerPositionKey, $"{playerPosition.x},{playerPosition.y},{playerPosition.z}");
            PlayerPrefs.SetString(CurrentLevelKey, currentLevel);
            PlayerPrefs.Save();

            Debug.Log($"DEBUG: Saved PlayerPosition: {PlayerPrefs.GetString(PlayerPositionKey)}");
            Debug.Log($"DEBUG: Saved CurrentLevel: {PlayerPrefs.GetString(CurrentLevelKey)}");
        }

        public static (Vector3?, string) LoadProgress()
        {
            Debug.Log("Loading progress from PlayerPrefs...");

            if (PlayerPrefs.HasKey(PlayerPositionKey) && PlayerPrefs.HasKey(CurrentLevelKey))
            {
                Debug.Log($"DEBUG: PlayerPositionKey Data: {PlayerPrefs.GetString(PlayerPositionKey)}");
                Debug.Log($"DEBUG: CurrentLevelKey Data: {PlayerPrefs.GetString(CurrentLevelKey)}");

                string[] positionData = PlayerPrefs.GetString(PlayerPositionKey).Split(',');
                if (positionData.Length == 3 &&
                    float.TryParse(positionData[0], out float x) &&
                    float.TryParse(positionData[1], out float y) &&
                    float.TryParse(positionData[2], out float z))
                {
                    Vector3 playerPosition = new Vector3(x, y, z);
                    string currentLevel = PlayerPrefs.GetString(CurrentLevelKey);

                    Debug.Log($"DEBUG: Loaded Position: {playerPosition}, Loaded Level: {currentLevel}");
                    return (playerPosition, currentLevel);
                }

                Debug.LogError("Failed to parse PlayerPositionKey data.");
            }

            Debug.LogWarning("No progress found in PlayerPrefs.");
            return (null, null);
        }

        public static bool HasSavedProgress()
        {
            return PlayerPrefs.HasKey(PlayerPositionKey) && PlayerPrefs.HasKey(CurrentLevelKey);
        }

        public static void ClearProgress()
        {
            PlayerPrefs.DeleteKey(PlayerPositionKey);
            PlayerPrefs.DeleteKey(CurrentLevelKey);
        }
    }
}
