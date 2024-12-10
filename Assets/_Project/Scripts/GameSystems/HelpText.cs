using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public class HelpText : MonoBehaviour
    {
        public GameObject helpText;

        private void Start()
        {
            if (helpText != null)
            {
                helpText.SetActive(false);
            }
            else
            {
                Debug.LogWarning("HelpText: Objek helpText tidak diatur di Inspector!");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() && helpText != null)
            {
                helpText.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() && helpText != null)
            {
                helpText.SetActive(false);
            }
        }
    }
}
