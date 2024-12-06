using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackController : IBackpackController
    {
        public static event Action<List<IBackpackItem>> OnBackpackChanged;

        private List<IBackpackItem> Backpack { get; } = new();
        private Dictionary<ItemData, IBackpackItem> BackpackItems { get; } = new();

        public void Add(ItemData itemData, int stackSize)
        {
            if (BackpackItems.TryGetValue(itemData, out var item))
            {
                item.AddToStack(stackSize);
                OnBackpackChanged?.Invoke(Backpack);
                return;
            }

            var newItem = new BackpackItem(itemData, stackSize);

            Backpack.Add(newItem);
            BackpackItems.Add(newItem.ItemData, newItem);
            OnBackpackChanged?.Invoke(Backpack);
        }

        public void Remove(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize != 0) return;
            Backpack.Remove(item);
            BackpackItems.Remove(itemData);
            // OnBackpackChanged?.Invoke(Backpack);
        }
    }
}