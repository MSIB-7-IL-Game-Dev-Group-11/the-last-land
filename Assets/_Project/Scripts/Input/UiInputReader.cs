using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace TheLastLand._Project.Scripts.Input
{
    [CreateAssetMenu(fileName = "UiInputReader", menuName = "TheLastLand/UiInputReader")]
    public class UiInputReader : ScriptableObject, IUIActions
    {
        public event Action<bool> BackpackToggleEvent = delegate { };
        public event Action<int> HotbarSlotSelected = delegate { };
        public event Action UseItem = delegate { };

        private PlayerInputActions _playerInputActions;

        private bool _isInventoryOpen;

        private void OnEnable()
        {
            if (_playerInputActions == null)
            {
                _playerInputActions = new PlayerInputActions();
                _playerInputActions.UI.SetCallbacks(this);
            }

            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }

        public void OnBackpack(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            _isInventoryOpen = !_isInventoryOpen;
            BackpackToggleEvent?.Invoke(_isInventoryOpen);
        }

        public void OnHotbarSlot(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            int slotIndex = context.ReadValue<int>();
            HotbarSlotSelected?.Invoke(slotIndex);
        }

        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.started) UseItem?.Invoke();
        }
    }
}