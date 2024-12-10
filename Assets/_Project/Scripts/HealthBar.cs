using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class HealthBar : UIBarBase
    {
        private IPlayerHealth _playerHealth;

        private void Start()
        {
            ServiceLocator.Global.TryGetWithStatus(out Player player);
            ServiceLocator.For(player).TryGetWithStatus(out PlayerData playerData)
                .TryGetWithStatus(out _playerHealth);

            Initialize(_playerHealth.Health, playerData.Health.Max);
        }

        private void OnEnable()
        {
            PlayerMediator.OnHealthChanged += SetValue;
        }

        private void OnDisable()
        {
            PlayerMediator.OnHealthChanged += SetValue;
        }

        [ContextMenu("TakeDamage")]
        private void TakeDamage()
        {
            _playerHealth.TakeDamage(10);
        }
    }
}