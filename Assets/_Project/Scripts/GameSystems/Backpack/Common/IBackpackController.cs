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
        void Add(ItemData itemData);

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="itemData">The data of the item to remove.</param>
        void Remove(ItemData itemData);
    }
}