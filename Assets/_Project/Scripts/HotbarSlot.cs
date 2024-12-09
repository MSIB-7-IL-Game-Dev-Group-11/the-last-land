using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems;
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

        protected override void Awake()
        {
            _inputReader = this.LoadAssetIfNull(
                _inputReader,
                "Assets/_Project/ScriptableObjects/UiInputReader.asset"
            );

            base.Awake();
        }

        public override void OnEnable()
        {
            _inputReader.BackpackToggleEvent += OnBackpackToggle;
            base.OnEnable();
        }

        public override void OnDisable()
        {
            _inputReader.BackpackToggleEvent -= OnBackpackToggle;
            base.OnDisable();
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