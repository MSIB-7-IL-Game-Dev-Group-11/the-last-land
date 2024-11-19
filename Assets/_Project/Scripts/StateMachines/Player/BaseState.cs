using TheLastLand._Project.Scripts.PlayerSystem;
using UnityEngine;

namespace TheLastLand._Project.Scripts.StateMachines.Player
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController PlayerController;
        protected readonly Animator Animator;
        
        protected static readonly int MovementHash = Animator.StringToHash("Movement");
        protected static readonly int JumpingHash = Animator.StringToHash("Jumping");
        
        protected const float CROSS_FADE_DURATION = 0.1f;

        protected BaseState(PlayerController playerController, Animator animator)
        {
            PlayerController = playerController;
            Animator = animator;
        }

        public virtual void OnEnter()
        {
            //
        }

        public virtual void Update()
        {
            //
        }

        public virtual void FixedUpdate()
        {
            //
        }

        public virtual void OnExit()
        {
            //
        }
    }
}