using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public class LevelMove : MonoBehaviour
    {
        public string targetScene = "Desa_B"; // Nama scene tujuan
        private bool _isPlayerInRange; // Cek apakah player dalam jangkauan

        private void Update()
        {
            // Jika player berada dalam jangkauan dan menekan tombol 'M'
            if (_isPlayerInRange && UnityEngine.Input.GetKeyDown(KeyCode.M))
            {
                // Pindah ke scene tujuan
                SceneManager.LoadScene(targetScene);
            }
        }

        // Ketika pemain masuk dalam collider gapura
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isPlayerInRange = true;
            }
        }

        // Ketika pemain keluar dari collider gapura
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _isPlayerInRange = false;
            }
        }
    }
}