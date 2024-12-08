﻿using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private GameObject backpackSlotPrefab;
        [SerializeField] private GameObject backpackUI;
        [SerializeField] private Transform backpackSlotContainer;

        private UiInputReader _inputReader;
        private IPlayerBackpack _playerBackpack;
        private List<SlotItemBase> _backpackSlots;

        private void OnValidate()
        {
            _inputReader = this.LoadAssetIfNull(
                _inputReader,
                "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            );
        }

        private void OnEnable()
        {
            SlotItemBase.ItemSwappedEvent += SwapWrapper;
            Item.OnCollected += AddWrapper;
            _inputReader.BackpackToggleEvent += ToggleBackpack;
        }

        private void Start()
        {
            ServiceLocator.Global.TryGet(out _playerBackpack);
            InitializeBackpackSlots();
        }

        private void Update()
        {
            DrawBackpack(_playerBackpack.Backpack);
        }

        private void OnDisable()
        {
            SlotItemBase.ItemSwappedEvent -= SwapWrapper;
            Item.OnCollected -= AddWrapper;
            _inputReader.BackpackToggleEvent -= ToggleBackpack;
        }

        private void ToggleBackpack(bool isOpen)
        {
            backpackUI.SetActive(isOpen);
        }

        private void InitializeBackpackSlots()
        {
            _backpackSlots = new List<SlotItemBase>(_playerBackpack.BackpackSize);
            for (var i = 0; i < _playerBackpack.BackpackSize; i++)
            {
                var backpackSlot = Instantiate(backpackSlotPrefab, backpackSlotContainer)
                    .GetComponent<BackpackSlot>();
                backpackSlot.ClearSlot();
                backpackSlot.Index = i + _playerBackpack.HotbarSize;
                _backpackSlots.Add(backpackSlot);
            }
        }

        private void DrawBackpack(List<IItem> backpack)
        {
            for (var i = 0; i < _backpackSlots.Count; i++)
            {
                if (i < backpack.Count)
                {
                    _backpackSlots[i].DrawSlot(backpack[i + _playerBackpack.HotbarSize]);
                }
                else
                {
                    _backpackSlots[i].ClearSlot();
                }
            }
        }

        private void SwapWrapper(int fromIndex, int toIndex) =>
            _playerBackpack.Swap(fromIndex, toIndex);

        private void AddWrapper(ItemData itemData, int stackSize) =>
            _playerBackpack.Add(itemData, stackSize);
    }
}