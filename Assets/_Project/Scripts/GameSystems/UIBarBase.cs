using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems
{
    public abstract class UIBarBase : MonoBehaviour
    {
        [SerializeField] protected Image barImage;

        private float _maxValue;
        private float _currentValue;

        protected void Initialize(float currentValue, float maxValue)
        {
            _maxValue = maxValue;
            _currentValue = currentValue;
            UpdateUI();
        }

        protected void SetValue(float value)
        {
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (barImage)
            {
                barImage.fillAmount = _currentValue / _maxValue;
            }
        }
    }
}