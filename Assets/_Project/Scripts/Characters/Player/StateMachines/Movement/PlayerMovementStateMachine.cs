using TheLastLand._Project.Scripts.Characters.Player.Data;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States.Grounded;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement.States.Grounded.Moving;
using TheLastLand._Project.Scripts.StateMachines;

namespace TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement
{
    /// <summary>
    /// Manages the player's movement state machine.
    /// </summary>
    public class PlayerMovementStateMachine
    {
        /// <summary>
        /// Gets the state machine instance.
        /// </summary>
        public StateMachine StateMachine { get; }

        /// <summary>
        /// Gets the player context.
        /// </summary>
        public Scripts.Player PlayerContext { get; }

        /// <summary>
        /// Gets the shared state data.
        /// </summary>
        public PlayerStateData StateData { get; }

        /// <summary>
        /// Represents the grounded state.
        /// </summary>
        private PlayerGroundedState IdlingState { get; set; }
        private PlayerGroundedState DashingState { get; set; }

        /// <summary>
        /// Represents the moving state of the player.
        /// </summary>
        private PlayerMovingState RunningState { get; set; }
        private PlayerMovingState WalkingState { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMovementStateMachine"/> class.
        /// </summary>
        /// <param name="playerContext">The player context.</param>
        public PlayerMovementStateMachine(Scripts.Player playerContext)
        {
            StateMachine = new StateMachine();
            PlayerContext = playerContext;
            StateData = new PlayerStateData();

            SetupStates();
            SetupTransitions();

            StateMachine.SetState(IdlingState);
        }

        private void At(IState from, IState to, IPredicate condition) =>
            StateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) =>
            StateMachine.AddAnyTransition(to, condition);

        private void SetupTransitions()
        {
            Any(IdlingState, new FuncPredicate(() => !StateData.IsWalking));
            At(IdlingState, WalkingState, new FuncPredicate(() => StateData.IsWalking));
            At(
                IdlingState,
                RunningState,
                new FuncPredicate(() => StateData.IsWalking && StateData.IsRunning)
            );
            At(RunningState, WalkingState, new FuncPredicate(() => !StateData.IsRunning));
            At(WalkingState, RunningState, new FuncPredicate(() => StateData.IsRunning));
        }

        /// <summary>
        /// Sets up the states for the state machine.
        /// </summary>
        private void SetupStates()
        {
            IdlingState = new PlayerIdlingState(this);
            RunningState = new PlayerRunningState(this);
            WalkingState = new PlayerWalkingState(this);
            DashingState = new PlayerDashingState(this);
        }
    }
}