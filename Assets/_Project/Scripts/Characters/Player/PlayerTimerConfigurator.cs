using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerTimerConfigurator : ICharacterTimer
    {
        private readonly PlayerStateData _stateData;
        private readonly PlayerComponent _components;
        private readonly PlayerData _data;
        private readonly IPlayerStamina _playerStamina;

        public CountdownTimer JumpTimer { get; }
        public CountdownTimer JumpCooldownTimer { get; }
        public CountdownTimer JumpCoyoteTimer { get; }
        public CountdownTimer DashTimer { get; }
        public CountdownTimer DashCooldownTimer { get; }
        public CountdownTimer StaminaRegenTimer { get; }

        private List<Timer> Timers { get; }

        public PlayerTimerConfigurator(PlayerStateData stateData, PlayerComponent components,
            PlayerData data, IPlayerStamina playerStamina)
        {
            _playerStamina = playerStamina;
            _stateData = stateData;
            _components = components;
            _data = data;

            JumpTimer = new CountdownTimer(0f);
            JumpCooldownTimer = new CountdownTimer(0f);
            JumpCoyoteTimer = new CountdownTimer(0f);
            DashTimer = new CountdownTimer(0f);
            DashCooldownTimer = new CountdownTimer(0f);
            StaminaRegenTimer = new CountdownTimer(0f);

            Timers = new List<Timer>(6)
            {
                JumpTimer, JumpCooldownTimer, JumpCoyoteTimer, DashTimer, DashCooldownTimer,
                StaminaRegenTimer
            };

            Setup();
        }

        public void HandleTimers(float deltaTime)
        {
            foreach (var timer in Timers)
            {
                timer.Tick(deltaTime);
            }

            if (!StaminaRegenTimer.IsRunning && _stateData.IsRegenerationEnabled)
            {
                StaminaRegenTimer.Reset(_data.Stamina.RegenerationTime);
                StaminaRegenTimer.Start();
            }

            CoyoteJumpChecking();
        }

        private void Setup()
        {
            JumpTimer.OnStart += () =>
            {
                _stateData.IsJumping = true;
                var forceDifference = _data.Jump.Force - _components.Rigidbody.velocity.y;
                _stateData.CurrentJumpVelocity = Mathf.Pow(
                                                     Mathf.Abs(forceDifference)
                                                     * _data.Acceleration,
                                                     _data.Jump.Modifier
                                                 )
                                                 * Mathf.Sign(forceDifference);
            };

            JumpTimer.OnStop += () =>
            {
                _stateData.IsJumping = false;
                JumpCooldownTimer.Reset(_data.Jump.Cooldown);
                JumpCooldownTimer.Start();
            };

            DashTimer.OnStart += () =>
            {
                _stateData.IsDashing = true;
                var targetSpeed = _stateData.MovementDirection.x * _data.Dash.Force;
                var forceDifference = targetSpeed - _components.Rigidbody.velocity.x;
                _stateData.CurrentMoveVelocity = Mathf.Pow(
                                                     Mathf.Abs(forceDifference)
                                                     * _data.Acceleration,
                                                     _data.Dash.Modifier
                                                 )
                                                 * Mathf.Sign(forceDifference);
            };

            DashTimer.OnStop += () =>
            {
                _stateData.IsDashing = false;
                _components.Rigidbody.velocity = new Vector2(0f, _components.Rigidbody.velocity.y);

                DashCooldownTimer.Reset(_data.Dash.Cooldown);
                DashCooldownTimer.Start();
            };

            StaminaRegenTimer.OnStart += () => { _playerStamina.RegenerateStamina(); };
        }

        private void CoyoteJumpChecking()
        {
            if (_stateData.WasGrounded
                && !_components.GroundCheck.IsTouching
                && !JumpTimer.IsRunning
                && !JumpCooldownTimer.IsRunning)
            {
                JumpCoyoteTimer.Reset(_data.Jump.CoyoteTime);
                JumpCoyoteTimer.Start();
            }

            _stateData.WasGrounded = _components.GroundCheck.IsTouching;
        }
    }
}