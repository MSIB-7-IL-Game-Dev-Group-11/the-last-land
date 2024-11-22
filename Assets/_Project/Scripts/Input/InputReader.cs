using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace TheLastLand._Project.Scripts.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "TheLastLand/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<InputAction.CallbackContext> Move = delegate { };
        public event UnityAction<InputAction.CallbackContext> Jump = delegate { };
        public event UnityAction<InputAction.CallbackContext> Dash = delegate { };

        private PlayerInputActions _playerInputActions;
        private Vector2 _moveInput;
        private bool _isRunning;

        private void OnEnable()
        {
            if (_playerInputActions == null)
            {
                _playerInputActions = new PlayerInputActions();
                _playerInputActions.Player.SetCallbacks(this);
            }

            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context);
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            //
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            Jump?.Invoke(context);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            Dash?.Invoke(context);
        }
    }
}