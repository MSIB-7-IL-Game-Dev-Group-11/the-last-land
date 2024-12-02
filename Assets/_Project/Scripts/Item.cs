using TheLastLand._Project.Scripts.GameSystems.Item;
using TheLastLand._Project.Scripts.GameSystems.Item.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TheLastLand._Project.Scripts
{
    public class Item : MonoBehaviour, ICollectible, IStackable
    {
        [SerializeField] private ItemData itemData;
        public static event UnityAction<ItemData> OnCollected = delegate { };
        private bool _collisionHandled;

        public void Collect()
        {
            Destroy(gameObject);
            itemData.StackSize = StackSize;
            OnCollected?.Invoke(itemData);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_collisionHandled) return;
            var otherItem = other.gameObject.GetComponent<Item>();

            if (otherItem == null || otherItem.itemData != itemData) return;
            var otherStackableItem = other.gameObject.GetComponent<IStackable>();

            if (otherStackableItem == null) return;

            // Transfer the stack size from the other item to this item
            for (int i = 0; i < otherStackableItem.StackSize; i++)
            {
                AddToStack();
            }

            // Mark the collision as handled
            _collisionHandled = true;
            otherItem._collisionHandled = true;

            // Destroy the other item
            Destroy(other.gameObject);
        }

        private void UpdateStackText()
        {
            var stackText = GetComponentInChildren<TMP_Text>();
            if (stackText != null)
            {
                stackText.text = StackSize > 1 ? StackSize.ToString() : string.Empty;
            }
        }

        public void AddToStack()
        {
            StackSize++;
            UpdateStackText();
        }

        public void RemoveFromStack()
        {
            StackSize--;
            UpdateStackText();
        }

        public int StackSize { get; private set; } = 1;
    }
}