using UnityEngine;

namespace TheLastLand
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject player; // Referensi ke objek player
        public Vector2 defaultSpawnPosition; // Posisi default jika tidak ada data spawn

        private void Start()
        {
            // Ambil posisi spawn dari PlayerPrefs
            float x = PlayerPrefs.GetFloat("SpawnX", defaultSpawnPosition.x);
            float y = PlayerPrefs.GetFloat("SpawnY", defaultSpawnPosition.y);

            // Pindahkan pemain ke posisi spawn
            if (player != null)
            {
                player.transform.position = new Vector2(x, y);
            }
        }
    }
}
