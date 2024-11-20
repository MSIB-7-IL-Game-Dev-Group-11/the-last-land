using TheLastLand._Project.Scripts.Characters.Player.Data;
using TheLastLand._Project.Scripts.StateMachines;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States
{
    public abstract class PlayerMovementState : IState
    {
        protected const float ZeroF = 0f;

        protected readonly Scripts.Player Player;
        protected readonly PlayerStateData StateData;
        protected readonly PlayerGroundedData MovementData;

        protected PlayerMovementState(PlayerMovementStateMachine playerMovement)
        {
            Player = playerMovement.PlayerContext;
            StateData = playerMovement.StateData;
            MovementData = Player.Data.GroundedData;
        }

        #region IState

        public virtual void OnEnter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            MoveHorizontal();
            FlipCharacterSprite();
        }

        public virtual void OnExit()
        {
        }

        #endregion

        #region Reusable Methods

        private void MoveHorizontal()
        {
            // TODO: add air acceleration
            // reference https://www.youtube.com/watch?v=KbtcEVCM7bw&t=111s
            var targetSpeed = StateData.MovementDirection.x * MovementData.BaseSpeed;
            var speedDifference = targetSpeed - Player.Rigidbody.velocity.x;

            StateData.CurrentSpeed = Mathf.Pow(
                                         Mathf.Abs(speedDifference) * MovementData.Acceleration,
                                         StateData.VelocityPower
                                     )
                                     * Mathf.Sign(speedDifference);

            Player.Rigidbody.AddForce(StateData.CurrentSpeed * Vector2.right);
        }

        private void FlipCharacterSprite()
        {
            Player.CharacterSprite.flipX = StateData.MovementDirection.x switch
            {
                > ZeroF => false,
                < ZeroF => true,
                _ => Player.CharacterSprite.flipX
            };
        }

        protected void ResetVelocity()
        {
            Player.Rigidbody.velocity = Vector2.zero;
        }

        #endregion
    }
}