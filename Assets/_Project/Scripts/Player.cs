using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : MonoBehaviour, IStamina, IDamagable
    {
        [field: SerializeField]
        public PlayerData Data { get; private set; }

        public PlayerStateData StateData { get; private set; }
        private PlayerController _playerController;

        #region Player Attributes

        private float _stamina, _health;

        private float Stamina
        {
            get => _stamina;
            set => _stamina = Mathf.Clamp(value, Data.Stamina.Min, Data.Stamina.Max);
        }

        private float Health
        {
            get => _health;
            set => _health = Mathf.Clamp(value, Data.Stamina.Min, Data.Stamina.Max);
        }

        #endregion

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            StateData = _playerController.StateData;
        }

        public void Move(float speedModifier) => _playerController.Move(speedModifier);
        public void Jump() => _playerController.Jump();
        public void FlipCharacterSprite() => _playerController.FlipCharacterSprite();

        public void UseStamina(float value)
        {
            Stamina -= value;
        }

        public void TakeDamage(float value)
        {
            Health -= value;
        }
    }
}