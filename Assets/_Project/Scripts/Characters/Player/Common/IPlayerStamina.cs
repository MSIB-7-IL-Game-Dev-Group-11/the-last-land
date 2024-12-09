using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    /// <summary>
    /// Interface representing a player's stamina, inheriting from IStaminaController.
    /// </summary>
    public interface IPlayerStamina : IStaminaController
    {
        /// <summary>
        /// Checks if the player has sufficient stamina.
        /// </summary>
        /// <param name="staminaThreshold">The threshold of stamina to check against.</param>
        /// <returns>True if the player has sufficient stamina, otherwise false.</returns>
        bool HasSufficientStamina(float staminaThreshold);
    }
}