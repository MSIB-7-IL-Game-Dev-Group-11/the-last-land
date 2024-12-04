using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.UI
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private UIInventoryItem item;

        public void Awake()
        {
            canvas = transform.root.GetComponent<Canvas>();
            item = GetComponentInChildren<UIInventoryItem>();
        }

        private void Update()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                UnityEngine.Input.mousePosition,
                canvas.worldCamera,
                out var position
            );
            transform.position = canvas.transform.TransformPoint(position);
        }

        public void SetData(Sprite sprite, int quantity)
        {
            item.SetData(sprite, quantity);
        }

        public void Toggle(bool val)
        {
            Debug.Log($"Item toggled {val}");
            gameObject.SetActive(val);
        }
    }
}