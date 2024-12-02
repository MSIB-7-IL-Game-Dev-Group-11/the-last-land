namespace TheLastLand._Project.Scripts.GameSystems.Health.Common
{
    public interface IHealthController
    {
        /// <summary>
        /// Gets the current health value.
        /// </summary>
        float Health { get; }

        /// <summary>
        /// Applies damage to the health controller.
        /// </summary>
        /// <param name="value">The amount of damage to apply.</param>
        void TakeDamage(float value);

        /// <summary>
        /// Regenerates health by a specified amount.
        /// </summary>
        /// <param name="value">The amount of health to regenerate.</param>
        void RegenerateHealth(float value);
    }
}