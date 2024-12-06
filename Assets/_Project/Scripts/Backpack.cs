using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems.Backpack;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private GameObject backpackSlotPrefab;
        [SerializeField] private GameObject backpackUI;
        [SerializeField] private UiInputReader inputReader;
        [SerializeField] private Transform backpackSlotContainer;

        private PlayerMediator _playerMediator;
        private List<BackpackSlot> _backpackSlots;

        private void Start()
        {
            InitializeBackpackSlots();
        }

        private void OnEnable()
        {
            BackpackController.OnBackpackChanged += DrawBackpack;
            BackpackSlot.ItemSwappedEvent += _playerMediator.Swap;
            inputReader.BackpackToggleEvent += ToggleBackpack;
        }

        private void OnDisable()
        {
            BackpackController.OnBackpackChanged -= DrawBackpack;
            BackpackSlot.ItemSwappedEvent -= _playerMediator.Swap;
            inputReader.BackpackToggleEvent -= ToggleBackpack;
        }

        private void OnValidate()
        {
            _playerMediator = this.LoadAssetIfNull(
                _playerMediator,
                "Assets/_Project/ScriptableObjects/PlayerMediator.asset"
            );
        }

        private void ToggleBackpack(bool isOpen)
        {
            backpackUI.SetActive(isOpen);
        }

        private void InitializeBackpackSlots()
        {
            _backpackSlots = new List<BackpackSlot>(_playerMediator.BackpackSize);
            for (var i = 0; i < _playerMediator.BackpackSize; i++)
            {
                var backpackSlot = Instantiate(backpackSlotPrefab, backpackSlotContainer)
                    .GetComponent<BackpackSlot>();
                backpackSlot.ClearSlot();
                _backpackSlots.Add(backpackSlot);
            }
        }

        private void DrawBackpack(List<IBackpackItem> backpack)
        {
            for (var i = 0; i < _backpackSlots.Count; i++)
            {
                if (i < backpack.Count)
                {
                    _backpackSlots[i].DrawSlot(backpack[i]);
                }
                else
                {
                    _backpackSlots[i].ClearSlot();
                }
            }
        }
    }
}