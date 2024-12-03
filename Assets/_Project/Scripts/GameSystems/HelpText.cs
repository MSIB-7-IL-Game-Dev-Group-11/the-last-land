using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public class HelpText : MonoBehaviour
    {
        public GameObject helpText; // Objek teks UI

        private void Start()
        {
            // Cek apakah helpText null sebelum menyembunyikannya
            if (helpText != null)
            {
                helpText.SetActive(false); // Sembunyikan teks saat awal
            }
            else
            {
                Debug.LogWarning("HelpText: Objek helpText tidak diatur di Inspector!");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && helpText != null)
            {
                helpText.SetActive(true); // Tampilkan teks saat pemain masuk area
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && helpText != null)
            {
                helpText.SetActive(false); // Sembunyikan teks saat pemain keluar area
            }
        }
    }
}
