namespace TheLastLand._Project.Scripts.Characters.Common
{
    /// <summary>
    /// Interface for configuring timer-related updates.
    /// </summary>
    public interface ITimerConfigurator
    {
        /// <summary>
        /// Handles timer updates.
        /// </summary>
        /// <param name="deltaTime">The time elapsed since the last update.</param>
        void HandleTimers(float deltaTime);
    }
}