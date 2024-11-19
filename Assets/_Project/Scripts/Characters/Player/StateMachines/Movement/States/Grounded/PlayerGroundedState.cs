using UnityEngine;
using UnityEngine.InputSystem;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States.Grounded
{
    public abstract class PlayerGroundedState : PlayerMovementState
    {
        protected PlayerGroundedState(PlayerMovementStateMachine playerMovement) : base(playerMovement)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            AddInputActionsCallbacks();
        }

        public override void OnExit()
        {
            base.OnExit();
            RemoveInputActionsCallbacks();
        }

        #region Reusable Method

        protected void AddInputActionsCallbacks()
        {
            Player.PlayerInput.Move += OnMove;
            Player.PlayerInput.Run += OnRun;
        }

        protected void RemoveInputActionsCallbacks()
        {
            Player.PlayerInput.Move -= OnMove;
            Player.PlayerInput.Run -= OnRun;
        }

        #endregion
        
        #region Event Handlers

        private void OnMove(InputAction.CallbackContext context)
        {
            StateData.MovementDirection = context.ReadValue<Vector2>();
            StateData.IsWalking = context.performed;
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            StateData.IsRunning = context.performed;
        }

        #endregion
    }
}