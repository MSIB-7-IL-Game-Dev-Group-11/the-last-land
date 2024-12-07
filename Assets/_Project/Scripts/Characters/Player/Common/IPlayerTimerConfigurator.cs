using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Utils;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerTimerConfigurator : ITimerConfigurator
    {
        CountdownTimer JumpTimer { get; }
        CountdownTimer JumpCooldownTimer { get; }
        CountdownTimer JumpCoyoteTimer { get; }
        CountdownTimer DashTimer { get; }
        CountdownTimer DashCooldownTimer { get; }
        CountdownTimer StaminaRegenTimer { get; }
    }
}