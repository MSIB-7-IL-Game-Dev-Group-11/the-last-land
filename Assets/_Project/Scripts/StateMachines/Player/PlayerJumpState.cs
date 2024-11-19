using TheLastLand._Project.Scripts.PlayerSystem;
using UnityEngine;

namespace TheLastLand._Project.Scripts.StateMachines.Player
{
    public class PlayerJumpState : BaseState
    {
        private static readonly int XvelocityHash = Animator.StringToHash("HorizontalVelocity");

        public PlayerJumpState(
            PlayerController playerController,
            Animator animator
        ) : base(playerController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Animator.SetFloat(XvelocityHash, PlayerController.ZeroF);
            Animator.CrossFade(PlayerController.IsPlayerJumping ? JumpingHash : MovementHash,
                CROSS_FADE_DURATION);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            PlayerController.HandleJump();
            PlayerController.HandleMovement();
        }
    }
}