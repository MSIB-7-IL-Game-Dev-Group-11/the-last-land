using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastLand._Project.Scripts.MainMenu
{
    public class SampleMainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            GameProgressManager.ClearProgress();
            Debug.Log("Starting new game...");
            PlayerPrefs.DeleteAll(); // Bersihkan progress lama untuk pengujian
            SceneManager.LoadSceneAsync(1); // Load scene pertama (ganti dengan nama atau index level Anda)
        }

        public void ContinueGame()
        {
            Debug.Log("Attempting to continue game...");

            // Cek apakah progress tersimpan
            Debug.Log($"DEBUG: PlayerPositionKey Exists: {PlayerPrefs.HasKey("PlayerPosition")}");
            Debug.Log($"DEBUG: CurrentLevelKey Exists: {PlayerPrefs.HasKey("CurrentLevel")}");

            if (GameProgressManager.HasSavedProgress())
            {
                var (playerPosition, currentLevel) = GameProgressManager.LoadProgress();

                // Debugging data yang dimuat dari PlayerPrefs
                Debug.Log($"DEBUG: Loaded PlayerPosition: {playerPosition}");
                Debug.Log($"DEBUG: Loaded CurrentLevel: {currentLevel}");

                if (!string.IsNullOrEmpty(currentLevel))
                {
                    SceneManager.LoadScene(currentLevel);
                    SceneManager.sceneLoaded += OnSceneLoaded;

                    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
                    {
                        GameObject player = GameObject.FindWithTag("Player");
                        if (player != null && playerPosition.HasValue)
                        {
                            player.transform.position = playerPosition.Value;
                            Debug.Log($"Player position restored to {playerPosition.Value}");
                        }
                        SceneManager.sceneLoaded -= OnSceneLoaded;
                    }
                }
                else
                {
                    Debug.LogWarning("Saved level name is empty!");
                }
            }
            else
            {
                Debug.LogWarning("No saved progress found!");
            }
        }

        public void ExitGame()
        {
            Debug.Log("Exiting game...");
            Application.Quit();
        }
    }
}
