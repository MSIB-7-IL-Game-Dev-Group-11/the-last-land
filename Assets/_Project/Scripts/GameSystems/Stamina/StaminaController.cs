using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Stamina
{
    [System.Serializable]
    public class StaminaController
    {
        private StaminaData _staminaData;
        private float _stamina;

        public void Initialize(StaminaData staminaData)
        {
            _staminaData = staminaData;
            _stamina = _staminaData.Max;
        }

        public float Stamina
        {
            get => _stamina;
            private set => _stamina = Mathf.Clamp(value, _staminaData.Min, _staminaData.Max);
        }

        public void UseStamina(float value)
        {
            Stamina -= value;
        }

        public void RegenerateStamina()
        {
            if (!(Stamina < _staminaData.Max)) return;

            Stamina += _staminaData.RegenerationRate;
            Stamina = Mathf.Clamp(Stamina, _staminaData.Min, _staminaData.Max);
        }

        public void AddStamina(float value)
        {
            Stamina += value;
        }

        public void SetStamina(float value)
        {
            Stamina = value;
        }
    }
}