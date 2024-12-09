namespace TheLastLand._Project.Scripts.StateMachines.Common
{
    /// <summary>
    /// Interface representing a transition between states.
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// Gets the target state of the transition.
        /// </summary>
        IState TargetState { get; }

        /// <summary>
        /// Gets the condition that triggers the transition.
        /// </summary>
        IPredicate Condition { get; }
    }
}