using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Backpack;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Health;
using TheLastLand._Project.Scripts.GameSystems.Health.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Stamina;
using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerMediator", menuName = "TheLastLand/PlayerMediator")]
    public class PlayerMediator : ScriptableObject, IPlayerHealth, IPlayerStamina, IPlayerBackpack
    {
        private IStaminaController _staminaController;
        private IHealthController _healthController;
        private IBackpackController _backpackController;

        public event UnityAction<float> OnStaminaUsed = delegate { };
        public event UnityAction<float> OnPlayerDamaged = delegate { };
        public event UnityAction OnPlayerRegenerated = delegate { };

        public void Initialize(PlayerData data)
        {
            _staminaController = new StaminaController(data.Stamina);
            _healthController = new HealthController(data.Health);
            _backpackController = new BackpackController();
        }

        #region IPlayerStamina

        public bool HasSufficientStamina(float staminaThreshold) =>
            _staminaController.Stamina >= staminaThreshold;

        public void UseStamina(float value)
        {
            _staminaController.UseStamina(value);
            OnStaminaUsed?.Invoke(_staminaController.Stamina);
        }

        public void RegenerateStamina()
        {
            _staminaController.RegenerateStamina();
            OnPlayerRegenerated?.Invoke();
        }

        #endregion

        #region IPlayerHealth

        public void TakeDamage(float value)
        {
            _healthController.TakeDamage(value);
            OnPlayerDamaged?.Invoke(_healthController.Health);
        }

        #endregion

        public void InventoryAdd(ItemData itemData, int stackSize) =>
            _backpackController.Add(itemData, stackSize);
    }
}