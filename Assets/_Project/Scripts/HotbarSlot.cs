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

        private UiInputReader _inputReader;

        private void Awake()
        {
            _inputReader = this.LoadAssetIfNull(
                _inputReader,
                "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            );

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

        private static void OnBackpackToggle(bool isOpen)
        {
            _isBackpackOpen = isOpen;
        }
    }
}