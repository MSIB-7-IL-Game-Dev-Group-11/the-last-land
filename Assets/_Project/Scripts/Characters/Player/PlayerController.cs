using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    public class PlayerController
    {
        private readonly PlayerData _data;
        private const float ZeroF = 0f;
        private InputReader PlayerInput { get; }

        private readonly PlayerStateData _stateData;
        private readonly IPlayerStamina _playerStamina;
        private IInteractable _interactable;

        // Components
        private readonly SpriteRenderer _characterSprite;
        private readonly Rigidbody2D _rigidbody;
        private readonly GroundCheck _groundCheck;

        // Timer
        private readonly IPlayerTimerConfigurator _timerConfigurator;

        public PlayerController()
        {
            ServiceLocator.Global.Get(out Scripts.Player player).Get(out _playerStamina);

            ServiceLocator.For(player).Get(out _data).Get(out _stateData)
                .Get(out PlayerComponent components).Get(out _timerConfigurator);

            PlayerInput = components.PlayerInput;

            _characterSprite = player.GetComponent<SpriteRenderer>();
            _rigidbody = player.GetComponent<Rigidbody2D>();
            _groundCheck = player.GetComponent<GroundCheck>();
        }

        public void RegisterEvents()
        {
            PlayerInput.Jump += OnJump;
            PlayerInput.Move += OnMove;
            PlayerInput.Dash += OnDash;
            PlayerInput.Interact += OnInteract;
            Scripts.Player.OnPlayerInteract += HandleInteraction;
        }

        public void DeregisterEvents()
        {
            PlayerInput.Jump -= OnJump;
            PlayerInput.Move -= OnMove;
            PlayerInput.Dash -= OnDash;
            PlayerInput.Interact -= OnInteract;
            Scripts.Player.OnPlayerInteract -= HandleInteraction;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _stateData.MovementDirection = context.ReadValue<Vector2>();
            _stateData.IsWalking = context.performed;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started when _groundCheck.IsTouching
                                                   && !_timerConfigurator.JumpTimer.IsRunning
                                                   && !_timerConfigurator.JumpCooldownTimer
                                                       .IsRunning
                                                   || _timerConfigurator.JumpCoyoteTimer.IsRunning:
                {
                    if (!_playerStamina.HasSufficientStamina(_data.Jump.StaminaCost)) return;

                    _playerStamina.UseStamina(_data.Jump.StaminaCost);
                    _timerConfigurator.JumpCoyoteTimer.Stop();

                    _timerConfigurator.JumpTimer.Reset(_data.Jump.Duration);
                    _timerConfigurator.JumpTimer.Start();

                    break;
                }
                case InputActionPhase.Canceled when _timerConfigurator.JumpTimer.IsRunning:
                {
                    _timerConfigurator.JumpTimer.Stop();
                    break;
                }
            }
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (!context.started
                || _timerConfigurator.DashTimer.IsRunning
                || _timerConfigurator.DashCooldownTimer.IsRunning) return;
            if (!_playerStamina.HasSufficientStamina(_data.Dash.StaminaCost)) return;

            _playerStamina.UseStamina(_data.Dash.StaminaCost);

            _timerConfigurator.DashTimer.Reset(_data.Dash.Duration);
            _timerConfigurator.DashTimer.Start();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.started || _interactable == null) return;
            _interactable.Interact();
        }

        public void Jump()
        {
            if (!_timerConfigurator.JumpTimer.IsRunning) return;

            _rigidbody.AddForce(_stateData.CurrentJumpVelocity * Vector2.up, ForceMode2D.Force);
        }

        public void Move(float speedModifier)
        {
            var targetSpeed = _stateData.MovementDirection.x
                              * (_timerConfigurator.DashTimer.IsRunning
                                  ? _data.Dash.Force
                                  : _data.BaseSpeed);

            var speedDifference = targetSpeed - _rigidbody.velocity.x;

            var accelerationRate = Mathf.Abs(targetSpeed) > 0.01f
                ? _data.Acceleration
                : _data.Deceleration;

            var mode = _timerConfigurator.DashTimer.IsRunning
                ? ForceMode2D.Impulse
                : ForceMode2D.Force;

            _stateData.CurrentMoveVelocity = Mathf.Pow(
                                                 Mathf.Abs(speedDifference) * accelerationRate,
                                                 speedModifier
                                             )
                                             * Mathf.Sign(speedDifference);
            Debug.Log(_stateData.CurrentMoveVelocity);

            _rigidbody.AddForce(_stateData.CurrentMoveVelocity * Vector2.right, mode);
        }

        public void FlipCharacterSprite()
        {
            _characterSprite.flipX = _stateData.MovementDirection.x switch
            {
                > ZeroF => false,
                < ZeroF => true,
                _ => _characterSprite.flipX
            };
        }

        private void HandleInteraction(Collider2D other, bool isEntering)
        {
            if (!isEntering)
            {
                _interactable = null;
                return;
            }

            _interactable = other.GetComponent<IInteractable>();
        }
    }
}