using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] InventorySlot InventoryItemPrefab = null;
        [SerializeField] private Transform inventorySlotRoot = null;

        // CACHE
        [SerializeField] private InventoryBase<InventoryItem> targetInventory;

        /*
        実用性は無いのでそろそろ消す
        private void Awake()
        {
            if (targetInventory == null)
            {
                targetInventory = Inventory.GetPlayerInventory();
                targetInventory.inventoryUpdated.AddListener(Redraw);
            }
        }
        */

        private void Start()
        {
            if (targetInventory != null)
            {
                SetTargetInventory(targetInventory);
            }
        }

        public void SetTargetInventory(InventoryBase<InventoryItem> targetInventory)
        {
            if (this.targetInventory != null)
            {
                this.targetInventory.inventoryUpdated.RemoveListener(Redraw);
            }

            this.targetInventory = targetInventory;
            targetInventory.inventoryUpdated.AddListener(Redraw);
            Redraw();
        }

        // PRIVATE

        private void Redraw()
        {
            foreach (Transform child in inventorySlotRoot)
            {
                Destroy(child.gameObject);
            }
            //Debug.Log(playerInventory);
            for (int i = 0; i < targetInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, inventorySlotRoot);
                itemUI.Setup(targetInventory, i);
                itemUI.name = $"InventorySlot[{i}]";
            }
        }
    }

}