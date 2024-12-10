using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheLastLand._Project.Scripts
{
    public class HotbarSlot : SlotItemBase
    {
        [SerializeField] private GameObject backpackPrefab;
        private static bool _isBackpackOpen;

        [SerializeField] private UiInputReader _inputReader;

        private void Awake()
        {
            // _inputReader = this.LoadAssetIfNull(
            //     _inputReader,
            //     "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            // );

            SlotUIManager = new SlotUIManager(icon, amount, select, counter);
        }

        private void OnEnable()
        {
            _inputReader.BackpackToggleEvent += OnBackpackToggle;
        }

        private void OnDisable()
        {
            _inputReader.BackpackToggleEvent -= OnBackpackToggle;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isBackpackOpen) return;

            base.OnBeginDrag(eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (!_isBackpackOpen) return;

            base.OnDrop(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isBackpackOpen) return;
            
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (!_isBackpackOpen) return;
            
            base.OnPointerExit(eventData);
        }

        public override void SelectSlot(bool isSelected)
        {
            if (_isBackpackOpen) return;
            
            if (Item != null && isSelected)
            {
                SlotUIManager.ActivateSlotComponents();
                return;
            }

            SlotUIManager.ActivateSlotComponents(false);
            base.SelectSlot(isSelected);
        }

        private static void OnBackpackToggle(bool isOpen)
        {
            _isBackpackOpen = isOpen;
        }
    }
}