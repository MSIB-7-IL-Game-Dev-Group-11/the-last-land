using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackController : IBackpackController
    {
        public List<IItem> Backpack { get; }

        private PlayerBackpackData PlayerBackpackData { get; }
        private Dictionary<ItemData, IItem> BackpackItems { get; }

        public BackpackController(PlayerBackpackData playerBackpackData)
        {
            PlayerBackpackData = playerBackpackData;
            var totalSize = PlayerBackpackData.Size + PlayerBackpackData.HotbarSize;
            Backpack = new List<IItem>(totalSize);
            BackpackItems = new Dictionary<ItemData, IItem>(totalSize);

            for (int i = 0; i < Backpack.Capacity; i++)
            {
                Backpack.Add(null);
            }
        }

        public void Add(ItemData itemData, int stackSize)
        {
            if (BackpackItems.TryGetValue(itemData, out var item))
            {
                item.AddToStack(stackSize);
            }
            else
            {
                AddNewItem(itemData, stackSize);
            }
        }

        public void Remove(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize != 0) return;
            Backpack.Remove(item);
            BackpackItems.Remove(itemData);
        }

        public void Swap(int fromIndex, int to)
        {
            (Backpack[fromIndex], Backpack[to]) = (Backpack[to], Backpack[fromIndex]);
        }

        public void Drop(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize != 0) return;
            Backpack.Remove(item);
            BackpackItems.Remove(itemData);
        }

        private void AddNewItem(ItemData itemData, int stackSize)
        {
            for (int i = 0; i < Backpack.Count; i++)
            {
                if (Backpack[i] != null) continue;
                var newItem = new BackpackItem(itemData, stackSize);
                Backpack[i] = newItem;
                BackpackItems.Add(newItem.ItemData, newItem);
                break;
            }
        }
    }
}