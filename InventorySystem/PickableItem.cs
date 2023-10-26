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

        // ここテスト用
        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
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
        }
        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(inventoryItem, amount);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");

            }
        }
        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(inventoryItem);
        }
    }
}