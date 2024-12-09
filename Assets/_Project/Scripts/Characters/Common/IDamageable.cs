namespace TheLastLand._Project.Scripts.Characters.Common
{
    /// <summary>
    /// Interface representing a damageable entity.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Method to apply damage to the entity.
        /// </summary>
        /// <param name="value">The amount of damage to apply.</param>
        void TakeDamage(float value);
    }
}