namespace TheLastLand._Project.Scripts.GameSystems.Item.Common
{
    /// <summary>
    /// Interface representing a stackable item.
    /// </summary>
    public interface IStackable
    {
        /// <summary>
        /// Gets the size of the stack.
        /// </summary>
        int StackSize { get; }

        /// <summary>
        /// Adds items to the stack.
        /// </summary>
        /// <param name="stackSize">The number of items to add to the stack.</param>
        void AddToStack(int stackSize);

        /// <summary>
        /// Removes items from the stack.
        /// </summary>
        /// <param name="stackSize">The number of items to remove from the stack.</param>
        void RemoveFromStack(int stackSize);
    }
}