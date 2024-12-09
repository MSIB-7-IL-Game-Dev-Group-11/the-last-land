using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.StateMachines.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines
{
    /// <summary>
    /// Abstract base class representing a state in the player's state machine.
    /// </summary>
    public abstract class BaseState : IState
    {
        /// <summary>
        /// Reference to the player controller.
        /// </summary>
        protected readonly PlayerController PlayerController;

        /// <summary>
        /// Reference to the player data.
        /// </summary>
        protected readonly PlayerData PlayerData;

        /// <summary>
        /// Reference to the animator.
        /// </summary>
        protected readonly Animator Animator;

        /// <summary>
        /// Constant representing zero float value.
        /// </summary>
        public const float ZeroF = 0f;
        
        /// <summary>
        /// Hash for the movement animation parameter.
        /// </summary>
        protected static readonly int MovementHash = Animator.StringToHash("Movement");

        /// <summary>
        /// Hash for the jumping animation parameter.
        /// </summary>
        protected static readonly int JumpingHash = Animator.StringToHash("Jumping");
        
        /// <summary>
        /// Duration for cross-fade transitions.
        /// </summary>
        protected const float CROSS_FADE_DURATION = 0.1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseState"/> class.
        /// </summary>
        /// <param name="playerController">The player controller.</param>
        /// <param name="animator">The animator.</param>
        protected BaseState(PlayerController playerController, Animator animator)
        {
            PlayerController = playerController;
            Animator = animator;
            PlayerData = PlayerController.Data;
        }

        /// <summary>
        /// Method called when entering the state.
        /// </summary>
        public virtual void OnEnter()
        {
            
        }

        /// <summary>
        /// Method called to update the state.
        /// </summary>
        public virtual void Update()
        {
            
        }

        /// <summary>
        /// Method called to perform fixed updates for the state.
        /// </summary>
        public virtual void FixedUpdate()
        {
            
        }

        /// <summary>
        /// Method called when exiting the state.
        /// </summary>
        public virtual void OnExit()
        {
            
        }
    }
}