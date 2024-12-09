using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.SeviceLocator;

namespace TheLastLand._Project.Scripts
{
    public class StaminaBar : UIBarBase
    {
        private void Start()
        {
            ServiceLocator.Global.TryGet(out Player player);
            ServiceLocator.For(player).TryGet(out PlayerData playerData);
            ServiceLocator.For(player).TryGet(out IPlayerStamina playerStamina);

            Initialize(playerStamina.Stamina, playerData.Stamina.Max);
        }

        private void OnEnable()
        {
            PlayerMediator.OnStaminaChanged += SetValue;
        }

        private void OnDisable()
        {
            PlayerMediator.OnStaminaChanged += SetValue;
        }
    }
}