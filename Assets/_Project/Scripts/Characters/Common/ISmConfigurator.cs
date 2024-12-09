namespace TheLastLand._Project.Scripts.Characters.Common
{
    /// <summary>
    /// Interface for configuring state machine updates.
    /// </summary>
    public interface ISmConfigurator
    {
        /// <summary>
        /// Updates the state machine.
        /// </summary>
        void Update();

        /// <summary>
        /// Performs fixed updates for the state machine.
        /// </summary>
        void FixedUpdate();
    }
}