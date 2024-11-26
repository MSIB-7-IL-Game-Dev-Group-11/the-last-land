using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.Setting
{
    public class AudioSetting : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private TextMeshProUGUI volumeText; 
        [SerializeField] private AudioSource musicSource;
        private const string VolumePrefKey = "GameVolume";

        private void Start()
        {
            LoadVolume();
            volumeSlider.onValueChanged.AddListener(SetVolume);
            UpdateVolumeText(volumeSlider.value);
        }

        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
            musicSource.volume = volume;
            PlayerPrefs.SetFloat(VolumePrefKey, volume);
            PlayerPrefs.Save();
            UpdateVolumeText(volume);
        }

        private void LoadVolume()
        {
            if (PlayerPrefs.HasKey(VolumePrefKey))
            {
                float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
                AudioListener.volume = savedVolume;
                musicSource.volume = savedVolume;
                volumeSlider.value = savedVolume;
            }
            else
            {
                AudioListener.volume = 1f;
                musicSource.volume = 1f;
                volumeSlider.value = 1f;
            }
            UpdateVolumeText(volumeSlider.value);
        }

        private void UpdateVolumeText(float volume)
        {
            int volumePercentage = Mathf.RoundToInt(volume * 100);
            volumeText.text = volumePercentage + "%";
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }
    }
}
