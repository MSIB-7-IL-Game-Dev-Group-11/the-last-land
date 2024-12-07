using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackController : IBackpackController
    {
        public static event Action<List<IBackpackItem>> OnBackpackChanged;

        private PlayerBackpackData PlayerBackpackData { get; }
        private List<IBackpackItem> Backpack { get; }
        private Dictionary<ItemData, IBackpackItem> BackpackItems { get; }

        public BackpackController(PlayerBackpackData playerBackpackData)
        {
            PlayerBackpackData = playerBackpackData;
            Backpack = new List<IBackpackItem>(PlayerBackpackData.Size);
            BackpackItems = new Dictionary<ItemData, IBackpackItem>(PlayerBackpackData.Size);

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

            OnBackpackChanged?.Invoke(Backpack);
        }

        public void Remove(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize != 0) return;
            Backpack.Remove(item);
            BackpackItems.Remove(itemData);
            OnBackpackChanged?.Invoke(Backpack);
        }

        public void Swap(int fromIndex, int to)
        {
            (Backpack[fromIndex], Backpack[to]) = (Backpack[to], Backpack[fromIndex]);
            OnBackpackChanged?.Invoke(Backpack);
        }
        
        public void Drop(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize == 0)
            {
                Backpack.Remove(item);
                BackpackItems.Remove(itemData);
            }

            OnBackpackChanged?.Invoke(Backpack);
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