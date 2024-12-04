using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.StateMachines;
using TheLastLand._Project.Scripts.StateMachines.Common;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class StateMachineConfigurator : ICharacterSm
    {
        private readonly PlayerController _playerController;
        private readonly StateMachine _stateMachine;
        private readonly Animator _animator;
        private readonly GroundCheck _groundCheck;
        private readonly PlayerStateData _stateData;
        private readonly CountdownTimer _jumpTimer;
        private readonly CountdownTimer _jumpCoyoteTimer;
        private readonly CountdownTimer _dashTimer;

        public StateMachineConfigurator(PlayerController playerController,
            StateMachine stateMachine, PlayerComponent components, PlayerStateData stateData,
            PlayerTimerConfigurator timerConfigurator)
        {
            _stateData = stateData;
            _playerController = playerController;
            _stateMachine = stateMachine;
            _animator = components.Animator;
            _groundCheck = components.GroundCheck;

            _jumpTimer = timerConfigurator.JumpTimer;
            _jumpCoyoteTimer = timerConfigurator.JumpCoyoteTimer;
            _dashTimer = timerConfigurator.DashTimer;

            Setup();
        }

        private void Setup()
        {
            var walkState = new PlayerWalkState(_playerController, _animator);
            var jumpState = new PlayerJumpState(_playerController, _animator);
            var idleState = new PlayerIdleState(_playerController, _animator);
            var dashState = new PlayerDashState(_playerController, _animator);

            Any(
                jumpState,
                new FuncPredicate(() => _jumpTimer.IsRunning || _jumpCoyoteTimer.IsRunning)
            );

            At(
                jumpState,
                walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && _stateData.IsWalking)
            );
            At(
                jumpState,
                idleState,
                new FuncPredicate(() => _groundCheck.IsTouching && !_jumpTimer.IsRunning)
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
            At(walkState, dashState, new FuncPredicate(() => _dashTimer.IsRunning));
            At(
                dashState,
                walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && !_dashTimer.IsRunning)
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