using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.GameSystems.Backpack;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Backpack : MonoBehaviour
    {
        [SerializeField] private GameObject backpackSlotPrefab;
        [SerializeField] private GameObject backpackUI;
        [SerializeField] private UiInputReader inputReader;

        private Transform _backpackSlotContainer;

        private IPlayerBackpack _playerBackpack;
        private List<BackpackSlot> _backpackSlots;

        private void Start()
        {
            _playerBackpack = ServiceLocator.ForSceneOf(this).Get<PlayerMediator>();
            _backpackSlotContainer = GameObject.Find("SlotContainer").transform;
            InitializeBackpackSlots();
        }

        private void OnEnable()
        {
            BackpackController.OnBackpackChanged += DrawBackpack;
            inputReader.BackpackToggleEvent += ToggleBackpack;
        }

        private void OnDisable()
        {
            BackpackController.OnBackpackChanged -= DrawBackpack;
            inputReader.BackpackToggleEvent -= ToggleBackpack;
        }

        private void ToggleBackpack(bool isOpen)
        {
            backpackUI.SetActive(isOpen);
        }

        private void InitializeBackpackSlots()
        {
            _backpackSlots = new List<BackpackSlot>(_playerBackpack.BackpackSize);
            for (var i = 0; i < _playerBackpack.BackpackSize; i++)
            {
                var backpackSlot = Instantiate(backpackSlotPrefab, _backpackSlotContainer)
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