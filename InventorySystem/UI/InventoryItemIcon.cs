using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace anogame.inventory
{
    [RequireComponent(typeof(Image))]
    public class InventoryItemIcon : MonoBehaviour
    {
        [SerializeField] private GameObject amountRoot;
        [SerializeField] private TextMeshProUGUI amountText;

        public void SetItem(InventoryItem inventoryItem, int amount)
        {
            var iconImage = GetComponent<Image>();
            if (inventoryItem == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = inventoryItem.GetIcon();
            }

            if (amountRoot != null)
            {
                if (amount > 1)
                {
                    amountRoot.SetActive(true);
                    amountText.text = "x" + amount.ToString();
                }
                else
                {
                    amountRoot.SetActive(false);
                }
            }


        }
        public Sprite GetItemSprite()
        {
            var iconImage = GetComponent<Image>();
            if (!iconImage.enabled)
            {
                return null;
            }
            return iconImage.sprite;
        }
    }
}
