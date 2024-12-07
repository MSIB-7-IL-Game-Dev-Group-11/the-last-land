using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackItem : IBackpackItem
    {
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
        }
        
        public void Use()
        {
            // Implement the logic for using the item here
            Debug.Log($"Using item: {ItemData.DisplayName}");
        }
    }
}