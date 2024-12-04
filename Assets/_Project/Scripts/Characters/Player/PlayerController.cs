using System.Collections.Generic;
using Cinemachine;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines;
using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.StateMachines;
using TheLastLand._Project.Scripts.StateMachines.Common;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(GroundCheck), typeof(WallCheck))]
    public class PlayerController : MonoBehaviour
    {
        private const float ZeroF = 0f;

        [field: Header("References"), SerializeField]
        public InputReader PlayerInput { get; private set; }

        [field: SerializeField]
        public CinemachineVirtualCamera VirtualCamera { get; private set; }

        public PlayerData data;
        private PlayerMediator _playerMediator;
        private PlayerStateData StateData { get; set; }
        private IInteractable _interactable;

        // Components
        private Animator _animator;
        private SpriteRenderer _characterSprite;
        private Rigidbody2D _rigidbody;
        private GroundCheck _groundCheck;

        // State Machine
        private StateMachine _stateMachine;

        // Timers
        private List<Timer> _timers;
        private CountdownTimer _jumpTimer;
        private CountdownTimer _jumpCooldownTimer;
        private CountdownTimer _jumpCoyoteTimer;
        private CountdownTimer _dashTimer;
        private CountdownTimer _dashCooldownTimer;

        private void Awake()
        {
            InitializeComponents();
            InitializeCamera();
        }

        private void Start()
        {
            SetupTimers();
            SetupServices();
            SetupStateMachine();
        }

        private void OnEnable()
        {
            PlayerInput.Jump += OnJump;
            PlayerInput.Move += OnMove;
            PlayerInput.Dash += OnDash;
            PlayerInput.Interact += OnInteract;
        }

        private void OnDisable()
        {
            PlayerInput.Jump -= OnJump;
            PlayerInput.Move -= OnMove;
            PlayerInput.Dash -= OnDash;
            PlayerInput.Interact -= OnInteract;
        }

        #region Input Handler

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleInteraction(other, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            HandleInteraction(other, false);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            StateData.MovementDirection = context.ReadValue<Vector2>();
            StateData.IsWalking = context.performed;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started when _groundCheck.IsTouching
                                                   && !_jumpTimer.IsRunning
                                                   && !_jumpCooldownTimer.IsRunning
                                                   || _jumpCoyoteTimer.IsRunning:
                {
                    if (!_playerMediator.HasSufficientStamina(data.Jump.StaminaCost)) return;

                    _playerMediator.UseStamina(data.Jump.StaminaCost);
                    _jumpCoyoteTimer.Stop();

                    _jumpTimer.Reset(data.Jump.Duration);
                    _jumpTimer.Start();

                    break;
                }
                case InputActionPhase.Canceled when _jumpTimer.IsRunning:
                {
                    _jumpTimer.Stop();
                    break;
                }
            }
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (!context.started || _dashTimer.IsRunning || _dashCooldownTimer.IsRunning) return;
            if (!_playerMediator.HasSufficientStamina(data.Dash.StaminaCost)) return;

            _playerMediator.UseStamina(data.Dash.StaminaCost);

            _dashTimer.Reset(data.Dash.Duration);
            _dashTimer.Start();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            if (!context.started || _interactable == null) return;
            _interactable.Interact();
        }

        #endregion

        private void Update()
        {
            StateData.IsWalking = Mathf.Abs(StateData.MovementDirection.x) > ZeroF;
            HandleTimers();
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
            CoyoteJumpChecking();
        }

        private void At(IState from, IState to, IPredicate condition) =>
            _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) =>
            _stateMachine.AddAnyTransition(to, condition);

        private void InitializeComponents()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _characterSprite = GetComponent<SpriteRenderer>();
            _groundCheck = GetComponent<GroundCheck>();

            _stateMachine = new StateMachine();
            StateData = new PlayerStateData();
        }

        private void InitializeCamera()
        {
            VirtualCamera.Follow = transform;
            VirtualCamera.LookAt = transform;
        }

        private void SetupStateMachine()
        {
            var walkState = new PlayerWalkState(this, _animator);
            var jumpState = new PlayerJumpState(this, _animator);
            var idleState = new PlayerIdleState(this, _animator);
            var dashState = new PlayerDashState(this, _animator);

            Any(
                jumpState,
                new FuncPredicate(() => _jumpTimer.IsRunning || _jumpCoyoteTimer.IsRunning)
            );

            At(
                jumpState,
                walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && StateData.IsWalking)
            );
            At(
                jumpState,
                idleState,
                new FuncPredicate(() => _groundCheck.IsTouching && !_jumpTimer.IsRunning)
            );

            At(
                idleState,
                walkState,
                new FuncPredicate(() => StateData.IsWalking && _groundCheck.IsTouching)
            );

            At(
                walkState,
                idleState,
                new FuncPredicate(() => !StateData.IsWalking && _groundCheck.IsTouching)
            );
            At(walkState, dashState, new FuncPredicate(() => _dashTimer.IsRunning));

            At(
                dashState,
                walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && !_dashTimer.IsRunning)
            );

            _stateMachine.SetState(idleState);
        }

        private void SetupServices()
        {
            ServiceLocator.ForSceneOf(this).Get(out _playerMediator);
            ServiceLocator.ForSceneOf(this).Get(out data);
        }

        #region Timer

        private void SetupTimers()
        {
            #region Jump Timer

            _jumpTimer = new CountdownTimer(data.Jump.Duration);
            _jumpCoyoteTimer = new CountdownTimer(data.Jump.CoyoteTime);
            _jumpCooldownTimer = new CountdownTimer(data.Jump.Cooldown);

            _jumpTimer.OnStart += () =>
            {
                StateData.IsJumping = true;
                var forceDifference = data.Jump.Force - _rigidbody.velocity.y;
                StateData.CurrentJumpVelocity = Mathf.Pow(
                                                    Mathf.Abs(forceDifference) * data.Acceleration,
                                                    data.Jump.Modifier
                                                )
                                                * Mathf.Sign(forceDifference);
            };

            _jumpTimer.OnStop += () =>
            {
                StateData.IsJumping = false;
                _jumpCooldownTimer.Reset(data.Jump.Cooldown);
                _jumpCooldownTimer.Start();
            };

            #endregion

            #region Dash Timer

            _dashTimer = new CountdownTimer(data.Dash.Duration);
            _dashCooldownTimer = new CountdownTimer(data.Dash.Cooldown);

            _dashTimer.OnStart += () =>
            {
                StateData.IsDashing = true;
                var targetSpeed = StateData.MovementDirection.x * data.Dash.Force;
                var forceDifference = targetSpeed - _rigidbody.velocity.x;
                StateData.CurrentMoveVelocity = Mathf.Pow(
                                                    Mathf.Abs(forceDifference) * data.Acceleration,
                                                    data.Dash.Modifier
                                                )
                                                * Mathf.Sign(forceDifference);
            };

            _dashTimer.OnStop += () =>
            {
                StateData.IsDashing = false;
                _rigidbody.velocity = new Vector2(ZeroF, _rigidbody.velocity.y);

                _dashCooldownTimer.Reset(data.Dash.Cooldown);
                _dashCooldownTimer.Start();
            };

            #endregion

            _timers = new List<Timer>(5)
            {
                _jumpTimer, _jumpCooldownTimer, _jumpCoyoteTimer,
                _dashTimer, _dashCooldownTimer
            };
        }

        private void HandleTimers()
        {
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        private void CoyoteJumpChecking()
        {
            if (StateData.WasGrounded
                && !_groundCheck.IsTouching
                && !_jumpTimer.IsRunning
                && !_jumpCooldownTimer.IsRunning)
            {
                _jumpCoyoteTimer.Reset(data.Jump.CoyoteTime);
                _jumpCoyoteTimer.Start();
            }

            StateData.WasGrounded = _groundCheck.IsTouching;
        }

        #endregion

        #region Player Movement

        public void Jump()
        {
            if (!_jumpTimer.IsRunning) return;

            _rigidbody.AddForce(StateData.CurrentJumpVelocity * Vector2.up, ForceMode2D.Force);
        }

        public void Move(float speedModifier)
        {
            var targetSpeed = StateData.MovementDirection.x
                              * (_dashTimer.IsRunning ? data.Dash.Force : data.BaseSpeed);

            var speedDifference = targetSpeed - _rigidbody.velocity.x;

            var accelerationRate = Mathf.Abs(targetSpeed) > 0.01f
                ? data.Acceleration
                : data.Deceleration;

            var mode = _dashTimer.IsRunning ? ForceMode2D.Impulse : ForceMode2D.Force;

            StateData.CurrentMoveVelocity = Mathf.Pow(
                                                Mathf.Abs(speedDifference) * accelerationRate,
                                                speedModifier
                                            )
                                            * Mathf.Sign(speedDifference);

            _rigidbody.AddForce(StateData.CurrentMoveVelocity * Vector2.right, mode);
        }

        public void FlipCharacterSprite()
        {
            _characterSprite.flipX = StateData.MovementDirection.x switch
            {
                > ZeroF => false,
                < ZeroF => true,
                _ => _characterSprite.flipX
            };
        }

        #endregion

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