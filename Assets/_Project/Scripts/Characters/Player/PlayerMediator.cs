﻿using System.Collections.Generic;
using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Backpack;
using TheLastLand._Project.Scripts.GameSystems.Backpack.Common;
using TheLastLand._Project.Scripts.GameSystems.Health;
using TheLastLand._Project.Scripts.GameSystems.Health.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.GameSystems.Stamina;
using TheLastLand._Project.Scripts.GameSystems.Stamina.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerMediator", menuName = "TheLastLand/PlayerMediator")]
    public class PlayerMediator : ScriptableObject, IPlayerHealth, IPlayerStamina, IPlayerBackpack
    {
        private IStaminaController _staminaController;
        private IHealthController _healthController;
        private IBackpackController _backpackController;

        public void Initialize(PlayerData data)
        {
            _staminaController = new StaminaController(data.Stamina);
            _healthController = new HealthController(data.Health);
            _backpackController = new BackpackController(data.Backpack);

            BackpackSize = data.Backpack.Size;
            HotbarSize = data.Backpack.HotbarSize;
        }

        #region IPlayerStamina

        public float Stamina => _staminaController.Stamina;

        public bool HasSufficientStamina(float staminaThreshold) =>
            Stamina >= staminaThreshold;

        public void UseStamina(float value) => _staminaController.UseStamina(value);

        public void RegenerateStamina() => _staminaController.RegenerateStamina();

        public void AddStamina(float value) => _staminaController.AddStamina(value);

        public void SetStamina(float value) => _staminaController.SetStamina(value);

        #endregion

        #region IPlayerHealth

        public void TakeDamage(float value) => _healthController.TakeDamage(value);

        #endregion

        #region IPlayerBackpack

        public List<IItem> Backpack => _backpackController.Backpack;

        public int BackpackSize { get; private set; }

        public int HotbarSize { get; private set; }

        public void Swap(int fromIndex, int toIndex) =>
            _backpackController.Swap(fromIndex, toIndex);

        public void Drop(ItemData itemData, int stackSize) =>
            _backpackController.Drop(itemData, stackSize);

        public void Add(ItemData itemData, int stackSize) =>
            _backpackController.Add(itemData, stackSize);

        public void Remove(ItemData itemData, int stackSize) =>
            _backpackController.Remove(itemData, stackSize);

        #endregion
    }
}