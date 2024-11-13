using TheLastLand._Project.Scripts.PlayerSystem;
using UnityEngine;

namespace TheLastLand._Project.Scripts.StateMachines
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController PlayerController;
        protected readonly Animator Animator;
        
        protected static readonly int WalkHash = Animator.StringToHash("Walk");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int XvelocityHash = Animator.StringToHash("HorizontalVelocity");
        protected static readonly int YvelocityHash = Animator.StringToHash("HorizontalVelocity");
        
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