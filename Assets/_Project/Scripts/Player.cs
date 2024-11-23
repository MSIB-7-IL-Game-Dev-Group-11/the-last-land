using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Common;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.Health;
using TheLastLand._Project.Scripts.Stamina;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(StaminaController))]
    [RequireComponent(typeof(HealthController))]
    public class Player : MonoBehaviour, IStamina, IDamageable
    {
        [field: Header("References"), SerializeField]
        public PlayerData Data { get; private set; }

        public event UnityAction<float> OnStaminaUsed = delegate { };
        public event UnityAction<float> OnPlayerDamaged = delegate { };

        // Controller
        private PlayerController _playerController;
        private StaminaController _staminaController;
        private HealthController _healthController;

        // Timer
        private List<Timer> _timers;
        private CountdownTimer _staminaRegenTimer;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _staminaController = GetComponent<StaminaController>();
            _healthController = GetComponent<HealthController>();

            _playerController.Initialize(Data);
            _staminaController.Initialize(Data.Stamina);
            _healthController.Initialize(Data.Health);
        }

        private void Start()
        {
            SetupTimers();
        }

        private void Update()
        {
            HandleTimer();

            if (!_staminaRegenTimer.IsRunning)
            {
                _staminaRegenTimer.Start();
            }
        }

        private void SetupTimers()
        {
            _staminaRegenTimer = new CountdownTimer(Data.Stamina.RegenerationTime);
            _staminaRegenTimer.OnStart += () => { _staminaController.RegenerateStamina(); };

            _timers = new List<Timer>(1) { _staminaRegenTimer };
        }

        private void HandleTimer()
        {
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        #region Movement

        public void Move(float speedModifier) => _playerController.Move(speedModifier);
        public void Jump() => _playerController.Jump();
        public void FlipCharacterSprite() => _playerController.FlipCharacterSprite();

        #endregion

        #region Checker

        public void IsStaminaBelowThreshold(float threshold, Action onTrue, Action onFalse)
        {
            if (_staminaController.Stamina < threshold)
            {
                onTrue.Invoke();
            }
            else
            {
                onFalse.Invoke();
            }
        }

        #endregion

        public void UseStamina(float value)
        {
            _staminaController.UseStamina(value);
            OnStaminaUsed.Invoke(_staminaController.Stamina);
        }

        public void TakeDamage(float value)
        {
            _healthController.TakeDamage(value);
            OnPlayerDamaged.Invoke(_healthController.Health);
        }
    }
}