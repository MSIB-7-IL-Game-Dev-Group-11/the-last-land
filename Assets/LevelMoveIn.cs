using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastLand
{
    public class LevelMoveIn : MonoBehaviour
    {
        public string targetScene = "Desa_A"; // Nama scene tujuan
        public Vector2 spawnPosition; // Posisi spawn di scene baru

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Cek jika objek yang masuk adalah Player
            if (collision.CompareTag("Player"))
            {
                // Simpan posisi spawn ke PlayerPrefs
                PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
                PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);

                // Pindah ke scene tujuan
                SceneManager.LoadScene(targetScene);
            }
        }
    }
}
