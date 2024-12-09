using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack.Common
{
    /// <summary>
    /// Interface representing a backpack controller.
    /// </summary>
    public interface IBackpackController
    {
        /// <summary>
        /// Gets the list of items in the backpack.
        /// </summary>
        List<IItem> Backpack { get; }
        
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

        /// <summary>
        /// Drops a specified number of items from the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to drop.</param>
        /// <param name="stackSize">The number of items to drop from the stack.</param>
        void Drop(ItemData itemData, int stackSize);
    }
}