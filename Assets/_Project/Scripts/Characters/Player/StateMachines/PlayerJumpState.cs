using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerJumpState : BaseState
    {
        private static readonly int XvelocityHash = Animator.StringToHash("HorizontalVelocity");

        public PlayerJumpState(Scripts.Player player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Player.UseStamina(Player.Data.Jump.StaminaCost);
            Animator.SetFloat(XvelocityHash, ZeroF);
            Animator.CrossFade(JumpingHash, CROSS_FADE_DURATION);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Player.Jump();
            Player.Move(Player.Data.Walk.Modifier);
            Player.FlipCharacterSprite();
        }
    }
}