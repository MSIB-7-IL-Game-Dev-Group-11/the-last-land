using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private GameObject hotbarSlotPrefab;
        [SerializeField] private GameObject hotbarUI;
        [SerializeField] private Transform hotbarSlotContainer;

        private UiInputReader _inputReader;
        private List<SlotItemBase> _hotbarSlots;
        private IPlayerBackpack _playerBackpack;
        private int _selectedSlotIndex;
        private Transform _itemTransform;

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
            _inputReader.BackpackToggleEvent += OnBackpackToggled;
            SlotItemBase.ItemSwappedEvent += SwapWrapper;
        }

        private void Start()
        {
            ServiceLocator.Global.TryGet(out _playerBackpack);
            InitializeHotbarSlots();
        }

        private void Update()
        {
            DrawHotbar(_playerBackpack.Backpack);
        }

        private void OnDisable()
        {
            _inputReader.HotbarSlotSelected -= SelectSlot;
            _inputReader.UseItem -= UseSelectedItem;
            _inputReader.BackpackToggleEvent -= OnBackpackToggled;
            SlotItemBase.ItemSwappedEvent -= SwapWrapper;
        }

        private void OnBackpackToggled(bool isOpen)
        {
            hotbarUI.SetActive(!isOpen);
        }

        private void InitializeHotbarSlots()
        {
            _hotbarSlots = new List<SlotItemBase>(_playerBackpack.HotbarSize);
            for (var i = 0; i < _playerBackpack.HotbarSize; i++)
            {
                var hotbarSlot = Instantiate(hotbarSlotPrefab, hotbarSlotContainer)
                    .GetComponent<HotbarSlot>();
                hotbarSlot.ClearSlot();
                hotbarSlot.Index = i;
                _hotbarSlots.Add(hotbarSlot);
            }
        }

        private void DrawHotbar(List<IItem> backpack)
        {
            for (var i = 0; i < _hotbarSlots.Count; i++)
            {
                if (i < backpack.Count)
                {
                    _hotbarSlots[i].DrawSlot(backpack[i]);
                }
                else
                {
                    _hotbarSlots[i].ClearSlot();
                }
            }
        }

        private void SelectSlot(int index)
        {
            if (index < 0 || index >= _playerBackpack.HotbarSize) return;
            _selectedSlotIndex = index;
            UpdateHotbarUI();
        }

        private void UseSelectedItem()
        {
            var selectedItem = _hotbarSlots[_selectedSlotIndex].Item;
            selectedItem?.Use();
        }

        private void UpdateHotbarUI()
        {
            for (var i = 0; i < _hotbarSlots.Count; i++)
            {
                _hotbarSlots[i].SelectSlot(i == _selectedSlotIndex);
            }
        }

        private void SwapWrapper(int fromIndex, int toIndex)
        {
            Debug.Log($"Swapping {fromIndex} and {toIndex}");
            _playerBackpack.Swap(fromIndex, toIndex);
        }
    }
}