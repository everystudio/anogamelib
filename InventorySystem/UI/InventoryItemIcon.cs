using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace anogame.inventory
{
    public class InventoryItemIcon : MonoBehaviour
    {
        public void SetItem(InventoryItem inventoryItem)
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
