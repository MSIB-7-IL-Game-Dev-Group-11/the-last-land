using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.StateMachines;
using TheLastLand._Project.Scripts.StateMachines.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class StateMachineConfigurator : IPlayerSmConfigurator
    {
        private readonly PlayerController _playerController;
        private readonly PlayerStateData _stateData;

        private readonly StateMachine _stateMachine;
        private readonly Animator _animator;
        private readonly GroundCheck _groundCheck;

        // Timer
        private readonly IPlayerTimerConfigurator _timerConfigurator;

        public StateMachineConfigurator(Scripts.Player player)
        {
            ServiceLocator.ForSceneOf(player).Get(out _stateData)
                .Get(out _stateMachine).Get(out _playerController).Get(out _timerConfigurator);

            _animator = player.GetComponent<Animator>();
            _groundCheck = player.GetComponent<GroundCheck>();

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