using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.StateMachines;
using TheLastLand._Project.Scripts.StateMachines.Common;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class StateMachineConfigurator : IPlayerSmConfigurator
    {
        private readonly PlayerStateData _stateData;
        private readonly StateMachine _stateMachine;
        private readonly GroundCheck _groundCheck;

        // Timer
        private readonly IPlayerTimerConfigurator _timerConfigurator;

        public StateMachineConfigurator()
        {
            ServiceLocator.Global.Get(out Scripts.Player player);

            ServiceLocator.For(player).Get(out _stateData).Get(out _stateMachine)
                .Get(out _timerConfigurator);

            _groundCheck = player.GetComponent<GroundCheck>();

            Setup();
        }

        private void Setup()
        {
            var walkState = new PlayerWalkState();
            var jumpState = new PlayerJumpState();
            var idleState = new PlayerIdleState();
            var dashState = new PlayerDashState();

            Any(
                jumpState,
                new FuncPredicate(
                    () => _timerConfigurator.JumpTimer.IsRunning
                          || _timerConfigurator.JumpCoyoteTimer.IsRunning
                )
            );

            At(
                jumpState,
                walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && _stateData.IsWalking)
            );
            At(
                jumpState,
                idleState,
                new FuncPredicate(
                    () => _groundCheck.IsTouching && !_timerConfigurator.JumpTimer.IsRunning
                )
            );

            At(
                idleState,
                walkState,
                new FuncPredicate(() => _stateData.IsWalking && _groundCheck.IsTouching)
            );
            At(
                walkState,
                idleState,
                new FuncPredicate(() => !_stateData.IsWalking && _groundCheck.IsTouching)
            );
            At(
                walkState,
                dashState,
                new FuncPredicate(() => _timerConfigurator.DashTimer.IsRunning)
            );
            At(
                dashState,
                walkState,
                new FuncPredicate(
                    () => _groundCheck.IsTouching && !_timerConfigurator.DashTimer.IsRunning
                )
            );

            _stateMachine.SetState(idleState);
        }

        public void Update() => _stateMachine.Update();
        public void FixedUpdate() => _stateMachine.FixedUpdate();

        private void At(IState from, IState to, IPredicate condition) =>
            _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) =>
            _stateMachine.AddAnyTransition(to, condition);
    }
}