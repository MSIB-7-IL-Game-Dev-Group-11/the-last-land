using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
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

        [SerializeField] private UiInputReader _inputReader;
        private IList<SlotItemBase> HotbarSlots { get; set; }
        private IPlayerBackpack _playerBackpack;
        private IPlayerHotbar _playerHotbar;

        // private void OnValidate()
        // {
        //     _inputReader = this.LoadAssetIfNull(
        //         _inputReader,
        //         "Assets/_Project/ScriptableObjects/UiInputReader.asset"
        //     );
        // }

        private void Awake()
        {
            ServiceLocator.Global.RegisterServiceIfNotExists(this);
        }

        private void OnEnable()
        {
            _inputReader.HotbarSlotSelectedEvent += OnSlotSelected;
            _inputReader.BackpackToggleEvent += OnBackpackToggled;
            SlotItemBase.ItemSwappedEvent += SwapWrapper;
        }

        private void Start()
        {
            ServiceLocator.Global.TryGetWithStatus(out _playerBackpack)
                .TryGetWithStatus(out _playerHotbar);

            HotbarSlots = new List<SlotItemBase>(_playerHotbar.HotbarSize);

            // need to refactor this
            _playerHotbar.Initialize(CreateSlotPrefab);
        }

        private void Update()
        {
            DrawHotbar();
        }

        private void OnDisable()
        {
            _inputReader.HotbarSlotSelectedEvent -= OnSlotSelected;
            _inputReader.BackpackToggleEvent -= OnBackpackToggled;
            SlotItemBase.ItemSwappedEvent -= SwapWrapper;
        }

        private void OnBackpackToggled(bool isOpen)
        {
            hotbarUI.SetActive(!isOpen);
        }

        private void CreateSlotPrefab(int index)
        {
            var hotbarSlot = Instantiate(hotbarSlotPrefab, hotbarSlotContainer)
                .GetComponent<HotbarSlot>();

            hotbarSlot.ClearSlot();
            hotbarSlot.Index = index;
            HotbarSlots.Add(hotbarSlot);
        }

        private void OnSlotSelected(int index)
        {
            if (!_playerHotbar.IsValidSlotIndex(index)) return;
            _playerHotbar.SelectSlot(index);
        }

        private void SwapWrapper(int fromIndex, int toIndex) =>
            _playerBackpack.Swap(fromIndex, toIndex);

        private void DrawSlot(int index)
        {
            var slot = HotbarSlots[index];

            if (index < _playerBackpack.Backpack.Count)
            {
                slot.DrawSlot(_playerBackpack.Backpack[index]);
                slot.SelectSlot(index == _playerHotbar.SelectedSlotIndex);
                _playerHotbar.SelectedItem =
                    _playerBackpack.Backpack[_playerHotbar.SelectedSlotIndex];
            }
            else
            {
                slot.ClearSlot();
            }
        }

        private void DrawHotbar()
        {
            for (var i = 0; i < HotbarSlots.Count; i++)
            {
                DrawSlot(i);
            }
        }
    }
}