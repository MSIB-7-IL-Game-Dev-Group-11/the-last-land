using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.OldInventory.Common;

namespace TheLastLand._Project.Scripts.GameSystems.OldInventory
{
    [System.Serializable]
    public class InventoryItem : IInventoryItem
    {
        public ItemData ItemData { get; private set; }

        public InventoryItem(ItemData itemData)
        {
            ItemData = itemData;
            AddToStack();
        }

        public int StackSize { get; private set; }

        public void AddToStack()
        {
            if (ItemData.StackSize > 1)
            {
                StackSize += ItemData.StackSize;
                ItemData.StackSize = 1;
                return;
            }
            
            StackSize++;
        }

        public void RemoveFromStack()
        {
            StackSize--;
        }
    }
}