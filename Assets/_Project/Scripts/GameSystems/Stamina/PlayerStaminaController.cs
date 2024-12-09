using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Stamina
{
    public class PlayerStaminaController : IStaminaController
    {
        private readonly StaminaData _staminaData;
        private float _stamina;

        public PlayerStaminaController(StaminaData staminaData)
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