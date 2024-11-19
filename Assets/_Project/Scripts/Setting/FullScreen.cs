using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand
{
    public class FullScreen : MonoBehaviour
    {
        [SerializeField] private Toggle fullscreenToggle;

        void Start()
        {
            if (fullscreenToggle != null)
            {
                fullscreenToggle.isOn = Screen.fullScreen;
                fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);
            }
        }

        void ToggleFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        void OnDestroy()
        {
            if (fullscreenToggle != null)
            {
                fullscreenToggle.onValueChanged.RemoveListener(ToggleFullscreen);
            }
        }
    }
}
