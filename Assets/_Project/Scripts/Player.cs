using Cinemachine;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Data;
using TheLastLand._Project.Scripts.Characters.Player.StateMachines.Movement;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(GroundCheck), typeof(WallCheck))]
    public class Player : MonoBehaviour
    {
        [field: Header("References"), SerializeField]
        public InputReader PlayerInput { get; private set; }

        [field: SerializeField]
        public LayerMask WallLayer { get; private set; }

        [field: SerializeField]
        public CinemachineVirtualCamera VirtualCamera { get; private set; }

        [field: Header("Player Data"), SerializeField]
        public PlayerData Data { get; private set; }

        public Animator Animator { get; private set; }
        public SpriteRenderer CharacterSprite { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public GroundCheck GroundCheck { get; private set; }
        public WallCheck WallCheck { get; private set; }

        private PlayerMovementStateMachine _playerMovement;

        private void Awake()
        {
            InitializeComponents();
            SetupCamera();
            InitializeStateMachine();
        }

        private void Update()
        {
            _playerMovement.StateMachine.Update();
        }

        private void FixedUpdate()
        {
            _playerMovement.StateMachine.FixedUpdate();
        }

        private void InitializeComponents()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            CharacterSprite = GetComponent<SpriteRenderer>();
            GroundCheck = GetComponent<GroundCheck>();
            WallCheck = GetComponent<WallCheck>();
        }

        private void SetupCamera()
        {
            VirtualCamera.Follow = transform;
            VirtualCamera.LookAt = transform;
        }

        private void InitializeStateMachine()
        {
            _playerMovement = new PlayerMovementStateMachine(this);
        }
    }
}