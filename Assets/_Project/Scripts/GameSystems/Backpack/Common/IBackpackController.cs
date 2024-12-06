using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack.Common
{
    public interface IBackpackController
    {
        /// <summary>
        /// Gets the list of inventory items.
        /// </summary>
        List<IBackpackItem> Backpack { get; }

        /// <summary>
        /// Gets the dictionary of inventory items with their corresponding item data.
        /// </summary>
        Dictionary<ItemData, IBackpackItem> BackpackItems { get; }

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
    }
}