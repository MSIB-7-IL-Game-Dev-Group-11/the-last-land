using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.GameSystems.Hotbar.Common;
using TheLastLand._Project.Scripts.SeviceLocator;

namespace TheLastLand._Project.Scripts.GameSystems.Hotbar
{
    public class PlayerHotbarController : IHotbarController
    {
        public IList<SlotItemBase> HotbarSlots { get; }
        public int SelectedSlotIndex { get; private set; }

        private readonly IPlayerBackpack _playerBackpack;

        public PlayerHotbarController()
        {
            ServiceLocator.Global.Get(out _playerBackpack);
            HotbarSlots = new List<SlotItemBase>(_playerBackpack.HotbarSize);
        }

        public void Initialize(Action<int> initCallback)
        {
            for (var i = 0; i < _playerBackpack.HotbarSize; i++)
            {
                initCallback(i);
            }
        }

        public void SelectSlot(int index)
        {
            if (index < 0 || index >= _playerBackpack.HotbarSize) return;
            SelectedSlotIndex = index;
            UpdateHotbarUI();
        }

        public void DrawHotbar()
        {
            for (var i = 0; i < HotbarSlots.Count; i++)
            {
                if (i < _playerBackpack.Backpack.Count)
                {
                    HotbarSlots[i].DrawSlot(_playerBackpack.Backpack[i]);
                }
                else
                {
                    HotbarSlots[i].ClearSlot();
                }
            }
        }

        public void SwapSlot(int from, int to)
        {
            _playerBackpack.Swap(from, to);
        }

        public void UseSelectedItem()
        {
            var selectedItem = HotbarSlots[SelectedSlotIndex].Item;
            selectedItem?.Use();
        }

        private void UpdateHotbarUI()
        {
            for (var i = 0; i < HotbarSlots.Count; i++)
            {
                HotbarSlots[i].SelectSlot(i == SelectedSlotIndex);
            }
        }
    }
}