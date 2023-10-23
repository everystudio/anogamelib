using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace anogame.inventory
{
    public class InventoryItemIcon : MonoBehaviour
    {
        public void SetItem(Sprite itemSprite)
        {
            var iconImage = GetComponent<Image>();
            if (itemSprite == null)
            {
                iconImage.enabled = false;
            }
            else
            {
                iconImage.enabled = true;
                iconImage.sprite = itemSprite;
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
