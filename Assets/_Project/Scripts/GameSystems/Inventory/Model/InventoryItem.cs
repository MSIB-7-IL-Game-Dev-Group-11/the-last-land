using System;
using System.Collections.Generic;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Model
{
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSo item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
        {
            return new InventoryItem
            {
                item = null,
                quantity = 0,
                itemState = new List<ItemParameter>()
            };
        }
    }
}