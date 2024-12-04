using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySo : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;

        [field: SerializeField]
        public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (var i = 0; i < Size; i++) inventoryItems.Add(InventoryItem.GetEmptyItem());
        }

        public int AddItem(ItemSo item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item.IsStackable == false)
                for (var i = 0; i < inventoryItems.Count;)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);

                    InformAboutChange();
                    return quantity;
                }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSo item, int quantity,
            List<ItemParameter> itemState = null)
        {
            var newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState ?? item.DefaultParametersList)
            };

            for (var i = 0; i < inventoryItems.Count; i++)
                if (inventoryItems[i].IsEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }

            return 0;
        }

        private bool IsInventoryFull()
        {
            return inventoryItems.Any(item => item.IsEmpty) == false;
        }

        private int AddStackableItem(ItemSo item, int quantity)
        {
            quantity = AddToExistingStacks(item, quantity);
            quantity = AddToNewStacks(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddToExistingStacks(ItemSo item, int quantity)
        {
            foreach (var inventoryItem in inventoryItems.Where(
                         inventoryItem => !inventoryItem.IsEmpty && inventoryItem.item.ID == item.ID
                     ))
            {
                quantity = AddQuantityToStack(inventoryItem, quantity);
                if (quantity == 0)
                    break;
            }

            return quantity;
        }

        private static int AddQuantityToStack(InventoryItem inventoryItem, int quantity)
        {
            var amountPossibleToTake = inventoryItem.item.MaxStackSize - inventoryItem.quantity;

            if (quantity > amountPossibleToTake)
            {
                inventoryItem.ChangeQuantity(inventoryItem.item.MaxStackSize);
                quantity -= amountPossibleToTake;
            }
            else
            {
                inventoryItem.ChangeQuantity(inventoryItem.quantity + quantity);
                quantity = 0;
            }

            return quantity;
        }

        private int AddToNewStacks(ItemSo item, int quantity)
        {
            while (quantity > 0 && !IsInventoryFull())
            {
                var newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (inventoryItems.Count <= itemIndex) return;
            if (inventoryItems[itemIndex].IsEmpty) return;

            var reminder = inventoryItems[itemIndex].quantity - amount;
            if (reminder <= 0)
                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);

            InformAboutChange();
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            var returnValue = new Dictionary<int, InventoryItem>();

            for (var i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }

            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            (inventoryItems[itemIndex1], inventoryItems[itemIndex2]) = (inventoryItems[itemIndex2],
                inventoryItems[itemIndex1]);
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
}