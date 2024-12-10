using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts
{
    public class Item : MonoBehaviour, ICollectible, IStackable
    {
        [SerializeField] protected ItemData itemData;
        public static event UnityAction<ItemData, int> OnCollected = delegate { };
        private bool _collisionHandled;

        public void Collect()
        {
            Destroy(gameObject);
            OnCollected?.Invoke(itemData, StackSize);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            HandleItemStacking(other);
        }

        private void HandleItemStacking(Collision2D other)
        {
            if (_collisionHandled) return;
            var otherItem = other.gameObject.GetComponent<Item>();

            if (otherItem == null || otherItem.itemData != itemData) return;
            var otherStackableItem = other.gameObject.GetComponent<IStackable>();

            if (otherStackableItem == null) return;

            // Transfer the stack size from the other item to this item
            AddToStack(otherStackableItem.StackSize);

            // Mark the collision as handled
            _collisionHandled = true;
            otherItem._collisionHandled = true;

            // Destroy the other item
            Destroy(other.gameObject);
            _collisionHandled = false;
        }

        private void UpdateStackText()
        {
            var stackText = GetComponentInChildren<TMP_Text>();
            if (stackText != null)
            {
                stackText.text = StackSize > 1 ? StackSize.ToString() : string.Empty;
            }
        }

        public void AddToStack(int stackSize)
        {
            StackSize += stackSize;
            UpdateStackText();
        }

        public void RemoveFromStack(int stackSize)
        {
            StackSize -= stackSize;
            UpdateStackText();
        }

        public int StackSize { get; private set; } = 1;
    }
}