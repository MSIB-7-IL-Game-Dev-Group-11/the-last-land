using TheLastLand._Project.Scripts.Characters.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Health.Common
{
    public interface IHealthController : IDamageable
    {
        /// <summary>
        /// Gets the current health value.
        /// </summary>
        float Health { get; }

        /// <summary>
        /// Regenerates health by a specified amount.
        /// </summary>
        /// <param name="value">The amount of health to regenerate.</param>
        void RegenerateHealth(float value);
    }
}