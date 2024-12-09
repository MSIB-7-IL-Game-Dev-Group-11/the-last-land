namespace TheLastLand._Project.Scripts.StateMachines.Common
{
    /// <summary>
    /// Interface representing a predicate.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Evaluates the predicate.
        /// </summary>
        /// <returns>True if the predicate is satisfied, false otherwise.</returns>
        bool Evaluate();
    }
}