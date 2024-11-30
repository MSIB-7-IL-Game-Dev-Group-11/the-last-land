using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory
{
    public class InventoryController : IInventoryController
    {
        public List<IInventoryItem> Inventory { get; } = new();
        public Dictionary<ItemData, IInventoryItem> InventoryItems { get; } = new();

        public void Add(ItemData itemData)
        {
            if (InventoryItems.TryGetValue(itemData, out var item))
            {
                item.AddToStack();
                return;
            }
            
            var newItem = new InventoryItem(itemData);
            
            Inventory.Add(newItem);
            InventoryItems.Add(itemData, newItem);
        }

        public void Remove(ItemData itemData)
        {
            if (!InventoryItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack();

            if (item.StackSize != 0) return;
            Inventory.Remove(item);
            InventoryItems.Remove(itemData);
        }
    }
}