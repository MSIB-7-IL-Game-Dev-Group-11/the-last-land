using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Health;
using TheLastLand._Project.Scripts.GameSystems.Health.Common;
using TheLastLand._Project.Scripts.GameSystems.Stamina;
using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerMediator : ScriptableObject, IDamageable, IStamina
    {
        private IStaminaController _staminaController;
        private IHealthController _healthController;

        public event UnityAction<float> OnStaminaUsed = delegate { };
        public event UnityAction<float> OnPlayerDamaged = delegate { };
        public event UnityAction OnPlayerRegenerated = delegate { };

        public void Initialize(PlayerData data)
        {
            _staminaController = new StaminaController(data.Stamina);
            _healthController = new HealthController(data.Health);
        }

        public bool HasSufficientStamina(float staminaThreshold) =>
            _staminaController.Stamina >= staminaThreshold;

        public void UseStamina(float value)
        {
            _staminaController.UseStamina(value);
            OnStaminaUsed?.Invoke(_staminaController.Stamina);
        }

        public void TakeDamage(float value)
        {
            _healthController.TakeDamage(value);
            OnPlayerDamaged?.Invoke(_healthController.Health);
        }

        public void RegenerateStamina()
        {
            _staminaController.RegenerateStamina();
            OnPlayerRegenerated?.Invoke();
        }
    }
}