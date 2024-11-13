using System.Collections.Generic;
using Cinemachine;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.StateMachines;
using TheLastLand._Project.Scripts.StateMachines.Player;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts.PlayerSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(GroundCheck), typeof(WallCheck))]
    public class PlayerController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader inputReader;

        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Header("Movement Settings")]
        [SerializeField, Min(0)] private float maxSpeed = 10f;

        [SerializeField] private float walkAcceleration = 75f;
        [SerializeField] private float airAcceleration = 30f;
        [SerializeField] private float groundDeceleration = 70f;

        [Header("Jumpp Settings")]
        [SerializeField] private float jumpHeight = 4f;

        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float jumpCooldown = 0.5f;
        [SerializeField] private float jumpDuration = 0.5f;
        [SerializeField] private float gravityMultiplier = 3f;

        private const float ZeroF = 0f;

        private Animator _animator;
        private SpriteRenderer _characterSprite;
        private Rigidbody2D _rigidbody;
        private GroundCheck _groundCheck;
        private WallCheck _wallCheck;

        private StateMachine _stateMachine;

        private List<Timer> _timers;
        private CountdownTimer _jumpTimer;
        private CountdownTimer _jumpCooldownTimer;

        private Vector2 _movement;
        public float Velocity { get; private set; }
        public float JumpVelocity { get; private set; }

        private bool IsDirecting { get; set; }

        private void Awake()
        {
            SetupCamera();
            InitializeComponents();
            SetupTimers();
            SetupStateMachine();
        }

        private void At(IState from, IState to, IPredicate condition) =>
            _stateMachine.AddTransition(from, to, condition);

        private void Any(IState to, IPredicate condition) =>
            _stateMachine.AddAnyTransition(to, condition);

        private void OnEnable()
        {
            inputReader.Jump += OnJump;
        }

        private void OnDisable()
        {
            inputReader.Jump -= OnJump;
        }

        private void OnJump(bool pressed)
        {
            switch (pressed)
            {
                case true when _groundCheck.IsTouching &&
                               !_jumpTimer.IsRunning &&
                               !_jumpCooldownTimer.IsRunning:
                {
                    JumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
                    _jumpTimer.Start();
                    break;
                }
                case false when _jumpTimer.IsRunning:
                {
                    _jumpTimer.Stop();
                    break;
                }
            }
        }

        private void Update()
        {
            _stateMachine.Update();
            _movement = inputReader.Direction;
            IsDirecting = !Mathf.Approximately(_movement.x, ZeroF);
            HandleTimers();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        private void SetupCamera()
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        private void InitializeComponents()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _characterSprite = GetComponent<SpriteRenderer>();
            _groundCheck = GetComponent<GroundCheck>();
            _wallCheck = GetComponent<WallCheck>();
        }

        private void SetupTimers()
        {
            _jumpTimer = new CountdownTimer(jumpDuration);
            _jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            _timers = new List<Timer>(2) { _jumpTimer, _jumpCooldownTimer };

            _jumpTimer.OnStart += () => JumpVelocity = jumpForce;
            _jumpTimer.OnStop += () => _jumpCooldownTimer.Start();
        }

        private void SetupStateMachine()
        {
            _stateMachine = new StateMachine();

            var walkState = new PlayerWalkState(this, _animator);
            var jumpState = new PlayerJumpState(this, _animator);

            At(walkState, jumpState, new FuncPredicate(() => _jumpTimer.IsRunning));
            At(jumpState, walkState,
                new FuncPredicate(() => _groundCheck.IsTouching && !_jumpTimer.IsRunning));

            _stateMachine.SetState(walkState);
        }

        private void HandleTimers()
        {
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void HandleJump()
        {
            switch (_jumpTimer.IsRunning)
            {
                case false when _groundCheck.IsTouching:
                    JumpVelocity = ZeroF;
                    return;
                case false:
                    JumpVelocity += Physics2D.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
                    break;
            }

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpVelocity);
        }

        public void HandleMovement()
        {
            if (_wallCheck.IsTouching)
            {
                MoveHorizontal(ZeroF, groundDeceleration);
                IsDirecting = !IsDirecting;
            }

            if (_jumpTimer.IsRunning)
            {
                MoveHorizontal(maxSpeed, airAcceleration);
            }
            else if (IsDirecting)
            {
                MoveHorizontal(maxSpeed, walkAcceleration);
            }
            else
            {
                MoveHorizontal(ZeroF, groundDeceleration);
            }

            FlipCharacterSprite();
        }

        private void MoveHorizontal(float target, float acceleration)
        {
            Velocity = Mathf.MoveTowards(
                Velocity,
                target * _movement.x,
                acceleration * Time.fixedDeltaTime
            );

            _rigidbody.velocity = new Vector2(Velocity, _rigidbody.velocity.y);
        }

        private void FlipCharacterSprite()
        {
            _characterSprite.flipX = _movement.x switch
            {
                > ZeroF => false,
                < ZeroF => true,
                _ => _characterSprite.flipX
            };
        }
    }
}