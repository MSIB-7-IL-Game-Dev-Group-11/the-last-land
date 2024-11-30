using TheLastLand._Project.Scripts.GameSystems.Inventory.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory
{
    [System.Serializable]
    public class InventoryItem : IInventoryItem
    {
        private ItemData _itemData;

        public InventoryItem(ItemData itemData)
        {
            _itemData = itemData;
        }

        public int StackSize { get; private set; }

        public void AddToStack()
        {
            StackSize++;
        }

        public void RemoveFromStack()
        {
            StackSize--;
        }
    }
}