using System.Collections.Generic;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems.Backpack;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private GameObject hotbarSlotPrefab;
        [SerializeField] private Transform hotbarSlotContainer;
        [SerializeField] private int hotbarSize = 5;

        private UiInputReader _inputReader;
        private IBackpackController _backpackController;
        private List<HotbarSlot> _hotbarSlots;
        private int _selectedSlotIndex;

        private void OnValidate()
        {
            _inputReader = this.LoadAssetIfNull(
                _inputReader,
                "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            );
        }

        private void OnEnable()
        {
            _inputReader.HotbarSlotSelected += SelectSlot;
            _inputReader.UseItem += UseSelectedItem;
        }
        
        private void Start()
        {
            ServiceLocator.Global.TryGet(out _backpackController);
            InitializeHotbarSlots();
        }

        private void OnDisable()
        {
            _inputReader.HotbarSlotSelected -= SelectSlot;
            _inputReader.UseItem -= UseSelectedItem;
        }

        private void InitializeHotbarSlots()
        {
            _hotbarSlots = new List<HotbarSlot>(hotbarSize);
            for (var i = 0; i < hotbarSize; i++)
            {
                var hotbarSlot = Instantiate(hotbarSlotPrefab, hotbarSlotContainer)
                    .GetComponent<HotbarSlot>();
                hotbarSlot.ClearSlot();
                _hotbarSlots.Add(hotbarSlot);
            }
        }

        private void SelectSlot(int index)
        {
            Debug.Log(index);

            // if (index < 0 || index >= hotbarSize) return;
            // _selectedSlotIndex = index;
            // UpdateHotbarUI();
        }

        private void UseSelectedItem()
        {
            var selectedItem = _hotbarSlots[_selectedSlotIndex].GetItem();
            selectedItem?.Use();
        }

        public void AddItemToHotbar(ItemData itemData, int stackSize)
        {
            foreach (var t in _hotbarSlots)
            {
                if (t.GetItem() != null) continue;
                var newItem = new BackpackItem(itemData, stackSize);
                t.SetItem(newItem);
                _backpackController.Remove(itemData, stackSize);
                break;
            }
        }

        public void RemoveItemFromHotbar(int index)
        {
            var item = _hotbarSlots[index].GetItem();
            if (item != null)
            {
                _backpackController.Add(item.ItemData, item.StackSize);
                _hotbarSlots[index].ClearSlot();
            }
        }

        private void UpdateHotbarUI()
        {
            for (var i = 0; i < _hotbarSlots.Count; i++)
            {
                _hotbarSlots[i].SetSelected(i == _selectedSlotIndex);
            }
        }
    }
}