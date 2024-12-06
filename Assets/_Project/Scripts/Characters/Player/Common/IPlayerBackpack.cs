﻿using TheLastLand._Project.Scripts.GameSystems.Item;

namespace TheLastLand._Project.Scripts.Characters.Player.Common
{
    public interface IPlayerBackpack
    {
        int BackpackSize { get; }
        void InventoryAdd(ItemData itemData, int stackSize);
    }
}