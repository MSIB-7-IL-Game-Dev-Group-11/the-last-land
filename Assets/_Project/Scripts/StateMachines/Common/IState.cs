namespace TheLastLand._Project.Scripts.StateMachines.Common
{
    /// <summary>
    /// Interface representing a state.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Called to update the state.
        /// </summary>
        void Update();

        /// <summary>
        /// Called to perform fixed updates for the state.
        /// </summary>
        void FixedUpdate();

        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        void OnExit();
    }
}