using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace TheLastLand._Project.Scripts.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "TheLastLand/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event Action<InputAction.CallbackContext> Move = delegate { };
        public event Action<InputAction.CallbackContext> Jump = delegate { };
        public event Action<InputAction.CallbackContext> Dash = delegate { };
        public event Action<InputAction.CallbackContext> Interact = delegate { };

        private PlayerInputActions _playerInputActions;

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

        public void OnInteract(InputAction.CallbackContext context)
        {
            Interact?.Invoke(context);
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            Dash?.Invoke(context);
        }
    }
}