using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.SeviceLocator;
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
        /// Reference to the player component and module.
        /// </summary>
        protected readonly PlayerData PlayerData;
        protected readonly PlayerStateData PlayerStateData;
        protected readonly PlayerController PlayerController;
        protected readonly Animator Animator;

        /// <summary>
        /// Hash for the animation parameter.
        /// </summary>
        protected static readonly int MovementHash = Animator.StringToHash("Move");
        protected static readonly int Jump = Animator.StringToHash("Jump");
        protected static readonly int Speed = Animator.StringToHash("Speed");

        /// <summary>
        /// Duration for cross-fade transitions.
        /// </summary>
        protected const float CrossFadeDuration = 0.2f;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseState"/> class.
        /// </summary>
        protected BaseState()
        {
            ServiceLocator.Global.Get(out Scripts.Player player);
            ServiceLocator.For(player).TryGetWithStatus(out PlayerStateData)
                .TryGetWithStatus(out PlayerController).TryGetWithStatus(out PlayerData);

            Animator = player.GetComponent<Animator>();
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