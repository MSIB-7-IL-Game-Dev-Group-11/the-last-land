using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TheLastLand
{
    public class ItemActionPanels : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            Button uiButton = button.GetComponent<Button>();
            if (uiButton != null)
            {
                uiButton.onClick.AddListener(() => onClickAction());
            }

            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = name;
            }
        }

        public void Toggle(bool val)
        {
            if (val)
            {
                RemoveOldButtons();
            }
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
