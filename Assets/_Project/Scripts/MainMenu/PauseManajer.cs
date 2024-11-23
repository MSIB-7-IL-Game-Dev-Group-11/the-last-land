using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastLand
{
    public class PauseManager : MonoBehaviour
    {
        private bool isPaused = false;

        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject player;

        // Tambahkan variabel untuk menyimpan referensi ke helpText
        [SerializeField] private GameObject helpText;

        public void PauseGame()
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                isPaused = true;

                // Nonaktifkan helpText jika aktif
                if (helpText != null && helpText.activeSelf)
                {
                    helpText.SetActive(false);
                }

                pauseMenuUI?.SetActive(true);
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            isPaused = false;
            pauseMenuUI?.SetActive(false);
        }

        public void MainMenu()
        {
            if (player != null)
            {
                string currentLevel = SceneManager.GetActiveScene().name;

                Debug.Log($"Saving progress before returning to Main Menu: PlayerPosition={player.transform.position}, Level={currentLevel}");
                GameProgressManager.SaveProgress(player.transform.position, currentLevel);

                // Debugging untuk memastikan data disimpan
                Debug.Log($"DEBUG: PlayerPositionKey: {PlayerPrefs.GetString("PlayerPosition", "Not Found")}");
                Debug.Log($"DEBUG: CurrentLevelKey: {PlayerPrefs.GetString("CurrentLevel", "Not Found")}");
            }

            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void TogglePause()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
}
