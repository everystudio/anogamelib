using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{

    public class PickableItem : MonoBehaviour
    {
        private InventoryItem inventoryItem;
        private int amount = 1;

        // これテスト用
        private Inventory inventory;

        private SpriteRenderer modelSpriteRenderer;

        // ここテスト用
        private void Awake()
        {
            /*
            if (inventory == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                Debug.Log(player);
                inventory = player.GetComponent<Inventory>();
            }
            */

            modelSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        }

        public InventoryItem GetItem()
        {
            return inventoryItem;
        }

        public int GetAmount()
        {
            return amount;
        }

        public void SetItem(InventoryItem item, int amount)
        {
            inventoryItem = item;
            this.amount = amount;
            if (modelSpriteRenderer != null)
            {
                modelSpriteRenderer.sprite = inventoryItem.GetIcon();
            }
        }
        public void PickupItem()
        {
            Debug.LogError("ここ修正ポイント");
            /*
            bool foundSlot = inventory.AddItemToSlot(inventoryItem, amount);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");

            }
            */
        }
        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(inventoryItem);
        }
    }
}