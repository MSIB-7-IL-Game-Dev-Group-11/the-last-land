using Cinemachine;
using TheLastLand._Project.Scripts.Input;
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
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        [Header("References")]
        [SerializeField] private InputReader inputReader;

        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        [Header("Settings")]
        [SerializeField] private float walkAcceleration = 75;

        [SerializeField] private float airAcceleration = 30;
        [SerializeField] private float groundDeceleration = 70;
        [SerializeField] private float jumpHeight = 4;
        [SerializeField, Min(0)] private float maxSpeed = 9f;

        private const float ZeroF = 0f;

        private Animator _animator;
        private SpriteRenderer _characterSprite;
        private Rigidbody2D _rigidbody;
        private GroundCheck _groundCheck;
        private WallCheck _wallCheck;

        private Vector2 _velocity;
        private float _direction;

        private bool IsDirecting { get; set; }

        private void Awake()
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;

            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _characterSprite = GetComponent<SpriteRenderer>();

            _groundCheck = GetComponent<GroundCheck>();
            _wallCheck = GetComponent<WallCheck>();
        }

        private void Update()
        {
            _direction = inputReader.Direction.x;
            IsDirecting = !Mathf.Approximately(_direction, ZeroF);
        }

        private void FixedUpdate()
        {
            HandleMovement();
            HandleAnimation();
        }

        private void HandleAnimation()
        {
            _animator.SetBool(IsWalking, IsDirecting);
            FlipCharacterSprite(_direction);
        }

        private void HandleMovement()
        {
            _velocity.y = _rigidbody.velocity.y;

            if (_wallCheck.IsTouching)
            {
                _velocity.x = ZeroF;
                IsDirecting = !IsDirecting;
            }

            if (IsDirecting)
            {
                _velocity.x = Mathf.MoveTowards(
                    _velocity.x,
                    maxSpeed * _direction,
                    walkAcceleration * Time.fixedDeltaTime
                );
            }
            else
            {
                _velocity.x = Mathf.MoveTowards(
                    _velocity.x,
                    ZeroF,
                    groundDeceleration * Time.fixedDeltaTime
                );
            }

            _rigidbody.velocity = _velocity;
        }

        private void FlipCharacterSprite(float direction)
        {
            _characterSprite.flipX = direction switch
            {
                > ZeroF => false,
                < ZeroF => true,
                _ => _characterSprite.flipX
            };
        }
    }
}