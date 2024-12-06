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
    }
}