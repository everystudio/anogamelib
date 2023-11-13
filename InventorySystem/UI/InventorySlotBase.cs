using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class InventorySlotBase : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] protected InventoryItemIcon icon = null;

        private InventoryBase inventory;
        private int index;

        private void Awake()
        {
            inventory.inventoryUpdated += UpdateIcon;
            UpdateIcon();
        }

        public void Setup(InventoryBase inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;

            var slot = inventory.GetSlot(index);
            icon.SetItem(slot.inventoryItem, slot.amount);
        }

        public void Set(InventoryItem item, int amount)
        {
            inventory.AddItemToSlot(index, item, amount);
        }

        public void AddAmount(int amount)
        {
            inventory.AddAmountToSlot(index, amount);
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

        private void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetAmount());
        }
    }
}