using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using Cinemachine;
using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.StateMachines;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Player : MonoBehaviour
    {
        private const float ZeroF = 0f;

        [SerializeField] private InputReader playerInput;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        private PlayerComponent _playerComponent;
        private PlayerStateData _stateData;
        private PlayerData _data;
        private PlayerMediator _playerMediator;
        private PlayerController _playerController;
        private StateMachine _stateMachine;

        [SerializeField]
        private bool staminaRegeneration = true;

        private ICharacterTimer _timerConfigurator;
        private ICharacterSm _smConfigurator;
        private IPlayerBackpack _playerBackpack;

        private void Awake()
        {
            _playerMediator.Initialize(_data);
            ServiceLocator.ForSceneOf(this).Register(_data).Register(_playerMediator);

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
                _data,
                _playerMediator
            );

            _playerController = new PlayerController(
                _data,
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

            _playerBackpack = _playerMediator;
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
            Item.OnCollected += _playerBackpack.Add;
            _playerController.RegisterEvents();
        }

        private void OnDisable()
        {
            Item.OnCollected -= _playerBackpack.Add;
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

        private void OnValidate()
        {
            _playerMediator = this.LoadAssetIfNull(
                _playerMediator,
                "Assets/_Project/ScriptableObjects/PlayerMediator.asset"
            );

            _data = this.LoadAssetIfNull(
                _data,
                "Assets/_Project/ScriptableObjects/PlayerData.asset"
            );
        }
    }
}