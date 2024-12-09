using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.GameSystems.Hotbar.Common;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.SeviceLocator;

namespace TheLastLand._Project.Scripts.GameSystems.Hotbar
{
    public class PlayerHotbarController : IHotbarController
    {
        public IList<SlotItemBase> HotbarSlots { get; }
        public int SelectedSlotIndex { get; private set; }
        private IItem _selectedItem;

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
            if (!IsValidSlotIndex(index)) return;

            var lastIndex = SelectedSlotIndex;
            UpdateSelectedSlotIndex(index);
            UpdateSelectedItem();

            if (lastIndex != SelectedSlotIndex)
            {
                DeselectSlot(lastIndex);
            }
        }

        public void DrawHotbar()
        {
            for (var i = 0; i < HotbarSlots.Count; i++)
            {
                DrawSlot(i);
            }
        }

        public void SwapSlot(int from, int to)
        {
            _playerBackpack.Swap(from, to);
        }

        private void UseSelectedItem()
        {
            _selectedItem = HotbarSlots[SelectedSlotIndex].Item;
            _selectedItem?.Use();
        }

        private void DrawSlot(int index)
        {
            var slot = HotbarSlots[index];

            if (index < _playerBackpack.Backpack.Count)
            {
                slot.DrawSlot(_playerBackpack.Backpack[index]);

                if (index != SelectedSlotIndex) return;
                slot.SelectSlot(true);
            }
            else
            {
                slot.ClearSlot();
            }
        }

        private bool IsValidSlotIndex(int index)
        {
            return index >= 0 && index < _playerBackpack.HotbarSize;
        }

        private void UpdateSelectedSlotIndex(int index)
        {
            SelectedSlotIndex = index;
            HotbarSlots[SelectedSlotIndex].SelectSlot(true);
        }

        private void UpdateSelectedItem()
        {
            _selectedItem = HotbarSlots[SelectedSlotIndex].Item;
        }

        private void DeselectSlot(int lastIndex)
        {
            HotbarSlots[lastIndex].SelectSlot(false);
        }
    }
}