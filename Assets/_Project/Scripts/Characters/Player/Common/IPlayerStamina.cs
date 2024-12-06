using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerStamina : IStaminaController
    {
        bool HasSufficientStamina(float staminaThreshold);
    }
}