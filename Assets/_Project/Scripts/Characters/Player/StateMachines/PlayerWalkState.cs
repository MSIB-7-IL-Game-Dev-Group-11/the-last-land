using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerWalkState : BaseState
    {
        private static readonly int XvelocityHash = Animator.StringToHash("HorizontalVelocity");

        public PlayerWalkState(PlayerController playerController, Animator animator) : base(
            playerController,
            animator
        )
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Animator.CrossFade(MovementHash, CROSS_FADE_DURATION);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            PlayerController.Move(PlayerData.Walk.Modifier);
            PlayerController.FlipCharacterSprite();
        }
    }
}