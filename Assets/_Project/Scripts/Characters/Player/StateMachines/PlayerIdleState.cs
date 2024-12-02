using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public class PlayerIdleState : BaseState
    {
        public PlayerIdleState(PlayerController playerController, Animator animator) : base(
            playerController,
            animator
        )
        {
        }
    }
}