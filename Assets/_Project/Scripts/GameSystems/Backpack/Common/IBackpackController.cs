using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack.Common
{
    public interface IBackpackController
    {
        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to add.</param>
        /// <param name="stackSize">The number of items to add to the stack.</param>
        void Add(ItemData itemData, int stackSize);

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to remove.</param>
        /// <param name="stackSize">The number of items to remove from the stack.</param>
        void Remove(ItemData itemData, int stackSize);

        /// <summary>
        /// Swaps the items at the specified indices in the inventory.
        /// </summary>
        /// <param name="fromIndex">The index of the first item to swap.</param>
        /// <param name="toIndex">The index of the second item to swap.</param>
        void Swap(int fromIndex, int toIndex);
    }
}