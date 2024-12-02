using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerDashState : BaseState
    {
        public PlayerDashState(PlayerController playerController, Animator animator) : base(
            playerController,
            animator
        )
        {
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            PlayerController.Move(PlayerData.Dash.Modifier);
            PlayerController.FlipCharacterSprite();
        }
    }
}