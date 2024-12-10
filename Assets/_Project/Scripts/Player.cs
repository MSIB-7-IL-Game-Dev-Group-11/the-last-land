using System;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.Input;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using UnityEngine;
using Cinemachine;
using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Common;
using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using StateMachine = TheLastLand._Project.Scripts.StateMachines.StateMachine;

namespace TheLastLand._Project.Scripts
{
    public class Player : MonoBehaviour
    {
        public static event Action<Collider2D, bool> OnPlayerInteract = delegate { };
        private static bool _isInitialized;
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

        private ITimerConfigurator _timerConfigurator;
        private ISmConfigurator _smConfigurator;

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

        private void Awake()
        {
            Initialize();
            RegisterServices();
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
            _playerController?.RegisterEvents();
        }

        private void OnDisable()
        {
            _playerController?.DeregisterEvents();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<ICollectible>()?.Collect();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnPlayerInteract?.Invoke(other, true);
            other.gameObject.GetComponent<IPlantable>()?.Plant();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnPlayerInteract?.Invoke(other, false);
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _playerMediator.Initialize(_data);

            _stateData = new PlayerStateData();
            _stateMachine = new StateMachine();
            _playerComponent = new PlayerComponent(playerInput);

            _isInitialized = true;
        }

        private void RegisterServices()
        {
            ServiceLocator.For(this).Register(_data).Register(_stateData).Register(_stateMachine)
                .Register(_playerComponent);

            ServiceLocator.Global.RegisterServiceIfNotExists(this)
                .RegisterServiceIfNotExists<IPlayerBackpack>(_playerMediator)
                .RegisterServiceIfNotExists<IPlayerHotbar>(_playerMediator)
                .RegisterServiceIfNotExists<IPlayerHealth>(_playerMediator)
                .RegisterServiceIfNotExists<IPlayerStamina>(_playerMediator);

            _timerConfigurator = new PlayerTimerConfigurator();
            ServiceLocator.For(this).Register(_timerConfigurator as IPlayerTimerConfigurator);

            _playerController = new PlayerController();
            ServiceLocator.For(this).Register(_playerController);

            _smConfigurator = new StateMachineConfigurator();
            ServiceLocator.For(this).Register(_smConfigurator as IPlayerSmConfigurator);
        }
    }
}