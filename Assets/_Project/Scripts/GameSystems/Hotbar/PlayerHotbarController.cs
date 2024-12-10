using System;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Hotbar.Common;

namespace TheLastLand._Project.Scripts.GameSystems.Hotbar
{
    public class PlayerHotbarController : IHotbarController
    {
        public int SelectedSlotIndex { get; private set; }

        public int LastSelectedSlotIndex { get; private set; }

        private readonly PlayerBackpackData _playerBackpackData;

        public PlayerHotbarController(PlayerBackpackData playerBackpackData)
        {
            _playerBackpackData = playerBackpackData;
        }

        public void Initialize(Action<int> initCallback)
        {
            for (var i = 0; i < _playerBackpackData.HotbarSize; i++)
            {
                initCallback(i);
            }
        }

        public void SelectSlot(int index)
        {
            LastSelectedSlotIndex = SelectedSlotIndex;
            SelectedSlotIndex = index;
        }

        // private void UseSelectedItem()
        // {
        //     _selectedItem = HotbarSlots[SelectedSlotIndex].Item;
        //     _selectedItem?.Use();
        // }

        public bool IsValidSlotIndex(int index)
        {
            return index >= 0 && index < _playerBackpackData.HotbarSize;
        }
    }
}