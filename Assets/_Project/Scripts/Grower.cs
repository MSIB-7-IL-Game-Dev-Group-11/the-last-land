using TheLastLand._Project.Scripts.Characters.Player.Common;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TheLastLand._Project.Scripts.SeviceLocator;
using TheLastLand._Project.Scripts.Utils;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class Grower : MonoBehaviour, IHarvestable, IPlantable
    {
        private SeedItemData _seedItemData;

        private IItem _item;
        private IPlayerHotbar _playerHotbar;

        private Transform _plantTransform;
        private CountdownTimer _harvestTimer;

        private void Awake()
        {
            _harvestTimer = new CountdownTimer(0f);
        }

        private void Start()
        {
            ServiceLocator.Global.TryGetWithStatus(out _playerHotbar);
        }

        private void Update()
        {
            _harvestTimer.Tick(Time.deltaTime);
            UpdatePlantScale();
        }

        public void Plant()
        {
            if (!IsValidTrigger()) return;

            _item = _playerHotbar.SelectedItem;
            _item.RemoveFromStack(1);

            InstantiatePlant();
            Debug.Log($"Planting {_item.ItemData.DisplayName}");
        }

        private bool IsValidTrigger()
        {
            if (_item != null) return false;
            if (_playerHotbar.SelectedItem == null) return false;

            _seedItemData = _playerHotbar.SelectedItem.ItemData as SeedItemData;
            return _seedItemData != null;
        }

        private void InstantiatePlant()
        {
            var plant = Instantiate(
                _seedItemData.PlantPrefab,
                transform.position,
                Quaternion.identity
            );
            plant.transform.SetParent(transform);
            _plantTransform = plant.transform;

            _harvestTimer.Reset(_seedItemData.GrowTime);
            _harvestTimer.Start();
        }

        private void UpdatePlantScale()
        {
            if (!_plantTransform) return;

            _plantTransform.localScale = Vector3.Lerp(
                Vector3.one,
                Vector3.zero,
                _harvestTimer.Progress
            );
        }
    }
}