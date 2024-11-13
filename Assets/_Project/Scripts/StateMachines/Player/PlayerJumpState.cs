using TheLastLand._Project.Scripts.PlayerSystem;
using UnityEngine;

namespace TheLastLand._Project.Scripts.StateMachines.Player
{
    public class PlayerJumpState : BaseState
    {
        public PlayerJumpState(
            PlayerController playerController,
            Animator animator
        ) : base(playerController, animator)
        {
        }

        public override void FixedUpdate()
        {
            PlayerController.HandleJump();
            PlayerController.HandleMovement();
        }
    }
}