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
            icon.SetItem(inventory.GetItemInSlot(index));
        }

        public void Add(int amount)
        {
        }

        public void Clear()
        {
            inventory.RemoveFromSlot(index);
        }

        public int GetAmount()
        {
            return 1;
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
            inventory.RemoveFromSlot(index);
        }

        public void Set(InventoryItem item, int amount)
        {
            inventory.AddItemToSlot(index, item);
        }
    }
}