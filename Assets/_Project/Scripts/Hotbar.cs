using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.GameSystems.Hotbar;
using TheLastLand._Project.Scripts.GameSystems.Hotbar.Common;
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
        private IHotbarController _hotbarController;

        private void OnValidate()
        {
            _inputReader = this.LoadAssetIfNull(
                _inputReader,
                "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            );
        }

        private void Awake()
        {
            ServiceLocator.Global.RegisterServiceIfNotExists(this);
            _hotbarController = new PlayerHotbarController();
        }

        private void OnEnable()
        {
            _inputReader.HotbarSlotSelected += SelectSlot;
            _inputReader.BackpackToggleEvent += OnBackpackToggled;
            SlotItemBase.ItemSwappedEvent += SwapWrapper;
        }

        private void Start()
        {
            // need to refactor this
            _hotbarController.Initialize(CreateSlotPrefab);
        }

        private void Update()
        {
            _hotbarController.DrawHotbar();
        }

        private void OnDisable()
        {
            _inputReader.HotbarSlotSelected -= SelectSlot;
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

            _hotbarController.HotbarSlots.Add(hotbarSlot);
        }

        private void SelectSlot(int index) => _hotbarController.SelectSlot(index);

        private void SwapWrapper(int fromIndex, int toIndex) =>
            _hotbarController.SwapSlot(fromIndex, toIndex);
    }
}