﻿using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Backpack
{
    public class BackpackController : IBackpackController
    {
        public List<IBackpackItem> Backpack { get; } = new();
        public Dictionary<ItemData, IBackpackItem> BackpackItems { get; } = new();

        public void Add(ItemData itemData, int stackSize)
        {
            if (BackpackItems.TryGetValue(itemData, out var item))
            {
                item.AddToStack(stackSize);
                Debug.Log($"{item.ItemData.DisplayName}. Total stack is now {item.StackSize}.");
                return;
            }

            var newItem = new BackpackItem(itemData, stackSize);

            Backpack.Add(newItem);
            BackpackItems.Add(itemData, newItem);
            Debug.Log($"{itemData.DisplayName} for the first time.");
        }

        public void Remove(ItemData itemData, int stackSize)
        {
            if (!BackpackItems.TryGetValue(itemData, out var item)) return;
            item.RemoveFromStack(stackSize);

            if (item.StackSize != 0) return;
            Backpack.Remove(item);
            BackpackItems.Remove(itemData);
        }
    }
}