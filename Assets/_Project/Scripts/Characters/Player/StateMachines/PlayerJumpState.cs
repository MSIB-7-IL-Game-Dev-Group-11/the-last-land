using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerJumpState : BaseState
    {
        private static readonly int XvelocityHash = Animator.StringToHash("HorizontalVelocity");

        public PlayerJumpState(PlayerController playerController, Animator animator) : base(
            playerController,
            animator
        )
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Animator.SetFloat(XvelocityHash, ZeroF);
            Animator.CrossFade(JumpingHash, CROSS_FADE_DURATION);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            PlayerController.Jump();
            PlayerController.Move(PlayerData.Walk.Modifier);
            PlayerController.FlipCharacterSprite();
        }
    }
}