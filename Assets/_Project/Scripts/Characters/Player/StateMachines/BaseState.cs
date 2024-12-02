using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.StateMachines;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController PlayerController;
        protected readonly PlayerData PlayerData;
        protected readonly Animator Animator;

        public const float ZeroF = 0f;
        
        protected static readonly int MovementHash = Animator.StringToHash("Movement");
        protected static readonly int JumpingHash = Animator.StringToHash("Jumping");
        
        protected const float CROSS_FADE_DURATION = 0.1f;

        protected BaseState(PlayerController playerController, Animator animator)
        {
            PlayerController = playerController;
            Animator = animator;
            PlayerData = PlayerController.data;
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
            
        }

        public virtual void OnExit()
        {
            
        }
    }
}