namespace TheLastLand._Project.Scripts.GameSystems.Stamina.Common
{
    public interface IStaminaController
    {
        /// <summary>
        /// Gets the current stamina value.
        /// </summary>
        float Stamina { get; }

        /// <summary>
        /// Uses a specified amount of stamina.
        /// </summary>
        /// <param name="value">The amount of stamina to use.</param>
        void UseStamina(float value);

        /// <summary>
        /// Regenerates stamina over time.
        /// </summary>
        void RegenerateStamina();

        /// <summary>
        /// Adds a specified amount of stamina.
        /// </summary>
        /// <param name="value">The amount of stamina to add.</param>
        void AddStamina(float value);

        /// <summary>
        /// Sets the stamina to a specific value.
        /// </summary>
        /// <param name="value">The value to set the stamina to.</param>
        void SetStamina(float value);
    }
}