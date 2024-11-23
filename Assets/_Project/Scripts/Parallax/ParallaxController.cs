using UnityEngine;

namespace TheLastLand
{
    public class ParallaxController : MonoBehaviour
    {
        public float parallaxMultiplier = 0.5f; // Intensitas parallax
        public float resetDistance = 20f; // Jarak untuk looping ulang (disesuaikan dengan panjang layer)
        private Transform[] layers; // Semua layer dalam background
        private Vector3 previousCameraPosition; // Posisi kamera sebelumnya

        void Start()
        {
            // Simpan posisi awal kamera
            previousCameraPosition = Camera.main.transform.position;

            // Ambil semua child dari Background
            int childCount = transform.childCount;
            layers = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                layers[i] = transform.GetChild(i);
            }
        }

        void Update()
        {
            // Hitung perubahan posisi kamera
            float deltaX = Camera.main.transform.position.x - previousCameraPosition.x;

            // Loop setiap layer untuk parallax dan looping
            foreach (Transform layer in layers)
            {
                // Terapkan efek parallax
                float depthMultiplier = layer.GetSiblingIndex() * parallaxMultiplier;
                layer.position += new Vector3(deltaX * depthMultiplier, 0, 0);

                // Periksa jika layer keluar dari batas layar untuk looping
                if (Mathf.Abs(Camera.main.transform.position.x - layer.position.x) >= resetDistance)
                {
                    // Geser layer kembali ke posisi awal
                    float offset = resetDistance * Mathf.Sign(Camera.main.transform.position.x - layer.position.x);
                    layer.position += new Vector3(offset, 0, 0);
                }
            }

            // Perbarui posisi kamera sebelumnya
            previousCameraPosition = Camera.main.transform.position;
        }
    }
}
