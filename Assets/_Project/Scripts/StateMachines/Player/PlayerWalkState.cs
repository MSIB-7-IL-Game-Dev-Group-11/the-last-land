using TheLastLand._Project.Scripts.PlayerSystem;
using UnityEngine;

namespace TheLastLand._Project.Scripts.StateMachines.Player
{
    public class PlayerWalkState : BaseState
    {
        public PlayerWalkState(
            PlayerController playerController,
            Animator animator
        ) : base(playerController, animator)
        {
        }

        public override void FixedUpdate()
        {
            PlayerController.HandleMovement();
        }
    }
}