using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.OldInventory.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.OldInventory
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
                Debug.Log($"{item.ItemData.DisplayName}. Total stack is now {item.StackSize}.");
                return;
            }

            var newItem = new InventoryItem(itemData);

            Inventory.Add(newItem);
            InventoryItems.Add(itemData, newItem);
            Debug.Log($"{itemData.DisplayName} for the first time.");
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