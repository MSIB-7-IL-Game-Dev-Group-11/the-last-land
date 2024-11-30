using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Common
{
    public interface IInventoryController
    {
        List<IInventoryItem> Inventory { get; }
        Dictionary<ItemData, IInventoryItem> InventoryItems { get; }
        void Add(ItemData itemData);
        void Remove(ItemData itemData);
    }
}