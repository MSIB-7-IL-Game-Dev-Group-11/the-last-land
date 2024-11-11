﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

namespace TheLastLand._Project.Scripts.Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "TheLastLand/InputReader")]
    public class InputReader : ScriptableObject, IPlayerActions
    {
        public event UnityAction<Vector2> Move = delegate { };

        private PlayerInputActions _playerInputActions;

        public Vector2 Direction => _playerInputActions.Player.Move.ReadValue<Vector2>();
        public bool IsJumpPressed => _playerInputActions.Player.Jump.triggered;
        public bool IsRunPressed => _playerInputActions.Player.Run.triggered;
        public bool IsDashPressed => _playerInputActions.Player.Dash.triggered;

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
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            //
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            //
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            //
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            //
        }
    }
}