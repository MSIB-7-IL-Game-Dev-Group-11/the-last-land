using System;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackItem : IItem
    {
        public static event Action<IItem> OnStackSizeZero = delegate { };

        public ItemData ItemData { get; }
        public int StackSize { get; private set; }

        public BackpackItem(ItemData itemData, int initialStackSize)
        {
            ItemData = itemData;
            StackSize = initialStackSize;
        }

        public void AddToStack(int stackSize)
        {
            StackSize += stackSize;
        }

        public void RemoveFromStack(int stackSize)
        {
            StackSize -= stackSize;
            
            if (StackSize > 0) return;
            StackSize = 0;
            OnStackSizeZero?.Invoke(this);
        }

        public void Use()
        {
            // Implement the logic for using the item here
            Debug.Log($"Using item: {ItemData.DisplayName}");
        }
    }
}