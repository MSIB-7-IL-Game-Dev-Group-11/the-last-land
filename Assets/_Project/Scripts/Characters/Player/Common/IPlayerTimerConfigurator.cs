using TheLastLand._Project.Scripts.Common;
using TheLastLand._Project.Scripts.Utils;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    /// <summary>
    /// Interface representing a player's timer configurator, inheriting from ITimerConfigurator.
    /// </summary>
    public interface IPlayerTimerConfigurator : ITimerConfigurator
    {
        /// <summary>
        /// Gets the jump timer.
        /// </summary>
        CountdownTimer JumpTimer { get; }

        /// <summary>
        /// Gets the jump cooldown timer.
        /// </summary>
        CountdownTimer JumpCooldownTimer { get; }

        /// <summary>
        /// Gets the jump coyote timer.
        /// </summary>
        CountdownTimer JumpCoyoteTimer { get; }

        /// <summary>
        /// Gets the dash timer.
        /// </summary>
        CountdownTimer DashTimer { get; }

        /// <summary>
        /// Gets the dash cooldown timer.
        /// </summary>
        CountdownTimer DashCooldownTimer { get; }

        /// <summary>
        /// Gets the stamina regeneration timer.
        /// </summary>
        CountdownTimer StaminaRegenTimer { get; }
    }
}