using TheLastLand._Project.Scripts.Characters.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerStamina : IStamina
    {
        bool HasSufficientStamina(float staminaThreshold);
        void RegenerateStamina();
    }
}