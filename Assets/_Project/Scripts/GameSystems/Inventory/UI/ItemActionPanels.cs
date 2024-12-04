using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.UI
{
    public class ItemActionPanels : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        public void AddButton(string buttonName, Action onClickAction)
        {
            var button = Instantiate(buttonPrefab, transform);
            var uiButton = button.GetComponent<Button>();
            if (uiButton != null) uiButton.onClick.AddListener(() => onClickAction());

            var buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null) buttonText.text = buttonName;
        }

        public void Toggle(bool val)
        {
            if (val) RemoveOldButtons();
            gameObject.SetActive(val);
        }

        private void RemoveOldButtons()
        {
            foreach (Transform child in transform) Destroy(child.gameObject);
        }
    }
}