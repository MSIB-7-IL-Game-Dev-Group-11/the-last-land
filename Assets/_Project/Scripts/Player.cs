using System;
using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [field: Header("References"), SerializeField]
        private PlayerData data;

        [SerializeField]
        private bool staminaRegeneration = true;

        private PlayerMediator _playerMediator;

        // Timer
        private List<Timer> _timers;
        private CountdownTimer _staminaRegenTimer;

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register(data)
                .Register(_playerMediator = new PlayerMediator(data));
        }

        private void Start()
        {
            SetupTimers();
        }

        private void Update()
        {
            HandleTimer();

            if (!_staminaRegenTimer.IsRunning && staminaRegeneration)
            {
                _staminaRegenTimer.Start();
            }
        }

        private void OnEnable()
        {
            Item.OnCollected += _playerMediator.InventoryAdd;
        }

        private void OnDisable()
        {
            Item.OnCollected -= _playerMediator.InventoryAdd;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<ICollectible>()?.Collect();
        }

        private void SetupTimers()
        {
            _staminaRegenTimer = new CountdownTimer(data.Stamina.RegenerationTime);
            _staminaRegenTimer.OnStart += () => { _playerMediator.RegenerateStamina(); };

            _timers = new List<Timer>(1) { _staminaRegenTimer };
        }

        private void HandleTimer()
        {
            foreach (var timer in _timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }
    }
}