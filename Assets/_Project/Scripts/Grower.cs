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
        private GameObject _plantPrefab;
        private CountdownTimer _harvestTimer;
        private bool _isHarvesting;

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

        public void Harvest()
        {
            if (!CanHarvest()) return;

            _isHarvesting = true;

            DropHarvestedItems();
            DropSeeds();
            Destroy(_plantPrefab.gameObject);
            _item = null;

            _isHarvesting = false;
        }

        private bool IsValidTrigger()
        {
            if (_item != null || _playerHotbar.SelectedItem == null) return false;

            _seedItemData = _playerHotbar.SelectedItem.ItemData as SeedItemData;
            return _seedItemData != null;
        }

        private void InstantiatePlant()
        {
            _plantPrefab = Instantiate(
                _seedItemData.PlantPrefab,
                transform.position,
                Quaternion.identity
            );
            _plantPrefab.transform.SetParent(transform);

            _harvestTimer.Reset(_seedItemData.GrowTime);
            _harvestTimer.Start();
        }

        private void UpdatePlantScale()
        {
            if (!_harvestTimer.IsRunning) return;

            _plantPrefab.transform.localScale = Vector3.Lerp(
                Vector3.one,
                Vector3.zero,
                _harvestTimer.Progress
            );
        }

        private void DropHarvestedItems()
        {
            var dropPosition = _plantPrefab.transform.position;
            Instantiate(_seedItemData.HarvestedItemPrefab, dropPosition, Quaternion.identity);
        }

        private void DropSeeds()
        {
            var dropOffset = transform.forward * 1.5f;
            var dropPosition = _plantPrefab.transform.position + dropOffset;
            int seedCount = Random.Range(
                _seedItemData.MinimumDropSeedAmount,
                _seedItemData.MaximumDropSeedAmount + 1
            );

            for (int i = 0; i < seedCount; i++)
            {
                var seed = Instantiate(_seedItemData.SeedPrefab, dropPosition, Quaternion.identity);
                var seedRigidbody = seed.GetComponent<Rigidbody2D>();
                if (seedRigidbody != null)
                {
                    seedRigidbody.AddForce(Vector3.up * 5f, ForceMode2D.Impulse);
                }
            }
        }

        private bool CanHarvest()
        {
            return !_harvestTimer.IsRunning && _plantPrefab != null && !_isHarvesting;
        }
    }
}