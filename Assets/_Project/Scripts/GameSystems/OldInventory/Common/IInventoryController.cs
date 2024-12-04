using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.OldInventory.Common
{
    public interface IInventoryController
    {
        /// <summary>
        /// Gets the list of inventory items.
        /// </summary>
        List<IInventoryItem> Inventory { get; }

        /// <summary>
        /// Gets the dictionary of inventory items with their corresponding item data.
        /// </summary>
        Dictionary<ItemData, IInventoryItem> InventoryItems { get; }

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to add.</param>
        void Add(ItemData itemData);

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to remove.</param>
        void Remove(ItemData itemData);
    }
}