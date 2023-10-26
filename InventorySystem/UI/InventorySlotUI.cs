using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{

    public class InventorySlotUI : MonoBehaviour, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;

        // STATE
        int index;
        InventoryItem item;
        Inventory inventory;

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;

            Inventory.InventorySlot slot = inventory.GetSlot(index);
            //Debug.Log(index);
            //Debug.Log(slot.inventoryItem);
            //Debug.Log(slot.amount);
            icon.SetItem(slot.inventoryItem, slot.amount);
        }

        public void Add(InventoryItem item, int amount)
        {
            inventory.AddItemToSlot(index, item, amount);
        }


        public void Clear()
        {
            var slot = inventory.GetSlot(index);
            inventory.RemoveFromSlot(index, slot.amount);
        }

        public int GetAmount()
        {
            return inventory.GetAmountInSlot(index);
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void Remove(int amount)
        {
            // 個数は一旦考えない
            inventory.RemoveFromSlot(index, amount);
        }
        /*
        public void Set(InventoryItem item, int amount)
        {
            inventory.AddItemToSlot(index, item, amount);
        }
        */
    }
}