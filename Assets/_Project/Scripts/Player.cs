using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using Cinemachine;
using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.StateMachines;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Player : MonoBehaviour
    {
        private const float ZeroF = 0f;

        [field: Header("References"), SerializeField]
        private PlayerData data;

        [SerializeField] private InputReader playerInput;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private PlayerComponent _playerComponent;
        private PlayerStateData _stateData;

        private PlayerMediator _playerMediator;
        private PlayerController _playerController;
        private StateMachine _stateMachine;

        [SerializeField]
        private bool staminaRegeneration = true;

        private ICharacterTimer _timerConfigurator;
        private ICharacterSm _smConfigurator;

        private void Awake()
        {
            _playerMediator = ScriptableObject.CreateInstance<PlayerMediator>();
            _playerMediator.Initialize(data);
            ServiceLocator.ForSceneOf(this).Register(data).Register(_playerMediator);

            var animator = GetComponent<Animator>();
            var characterSprite = GetComponent<SpriteRenderer>();
            var rb2D = GetComponent<Rigidbody2D>();
            var groundCheck = GetComponent<GroundCheck>();

            _playerComponent = new PlayerComponent(
                playerInput,
                virtualCamera,
                animator,
                characterSprite,
                rb2D,
                groundCheck
            );

            _stateData = new PlayerStateData();
            _stateMachine = new StateMachine();

            _timerConfigurator = new PlayerTimerConfigurator(
                _stateData,
                _playerComponent,
                data,
                _playerMediator
            );

            _playerController = new PlayerController(
                data,
                _playerMediator,
                _playerComponent,
                _stateData,
                _timerConfigurator as PlayerTimerConfigurator
            );

            _smConfigurator = new StateMachineConfigurator(
                _playerController,
                _stateMachine,
                _playerComponent,
                _stateData,
                _timerConfigurator as PlayerTimerConfigurator
            );
        }

        private void Update()
        {
            _stateData.IsWalking = Mathf.Abs(_stateData.MovementDirection.x) > ZeroF;
            _stateData.IsRegenerationEnabled = staminaRegeneration;

            _timerConfigurator.HandleTimers(Time.deltaTime);
            _smConfigurator.Update();
        }

        private void FixedUpdate()
        {
            _smConfigurator.FixedUpdate();
        }

        private void OnEnable()
        {
            _playerController.RegisterEvents();
        }

        private void OnDisable()
        {
            _playerController.DeregisterEvents();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<ICollectible>()?.Collect();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _playerController.HandleInteraction(other, true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _playerController.HandleInteraction(other, false);
        }
    }
}