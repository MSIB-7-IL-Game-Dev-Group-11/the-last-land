using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerDashState : BaseState
    {
        public override void OnEnter()
        {
            Animator.CrossFadeInFixedTime(MovementHash, CrossFadeDuration);
        }

        public override void FixedUpdate()
        {
            PlayerController.Move(PlayerData.Dash.Modifier);
            PlayerController.FlipCharacterSprite();
            Animator.SetFloat(Speed, Mathf.Abs(PlayerStateData.CurrentMoveVelocity));
        }
    }
}