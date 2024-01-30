using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class PickableItemBase : MonoBehaviour
    {
        private InventoryItem inventoryItem
        {
            get
            {
                if (coreInventoryItem == null)
                {
                    coreInventoryItem = InventoryItem.GetFromID(itemID);
                }
                return coreInventoryItem;
            }
        }
        private InventoryItem coreInventoryItem;
        public InventoryItem Item
        {
            get
            {
                return inventoryItem;
            }
        }

        private string itemID = "";
        private int amount = 1;



        // これテスト用
        private Inventory inventory;

        private SpriteRenderer modelSpriteRenderer;

        [SerializeField] private ScriptableReference player;

        // ここテスト用
        private void Awake()
        {
            if (player == null)
            {
                Debug.LogError("playerがnullです");
            }
            else if (player.Reference != null)
            {
                inventory = player.Reference.GetComponent<Inventory>();
            }

            player.AddListener((GameObject player) =>
            {
                inventory = player.GetComponent<Inventory>();
            });
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
            coreInventoryItem = item;
            this.itemID = item.GetItemID();
            this.amount = amount;
            if (modelSpriteRenderer != null)
            {
                modelSpriteRenderer.sprite = inventoryItem.GetIcon();
            }
        }
        public void PickupItem()
        {
            if (inventory == null)
            {
                Debug.LogError("ここ修正ポイント");
                return;
            }
            PickupItem(inventory);
        }

        public void PickupItem(Inventory inventory)
        {
            bool foundSlot = inventory.AddItemToSlot(inventoryItem, amount);
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
            return CanBePickedUp(inventory);
        }
        public bool CanBePickedUp(Inventory inventory)
        {
            return inventory.HasSpaceFor(inventoryItem);
        }
    }
}