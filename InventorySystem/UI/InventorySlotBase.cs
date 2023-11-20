using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class InventorySlotBase<T> : MonoBehaviour, IItemHolder<T>, IDragContainer<T> where T : InventoryItem
    {
        [SerializeField] protected InventoryItemIcon icon = null;

        protected InventoryBase<T> inventory;
        protected int index;

        /*
        private void Awake()
        {
            if (inventory != null)
            {
                inventory.inventoryUpdated.AddListener(UpdateIcon);
                UpdateIcon();
            }
        }
        */

        public void Setup(InventoryBase<T> inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;

            var slot = inventory.GetSlot(index);
            icon.SetItem(slot.inventoryItem, slot.amount);

            inventory.inventoryUpdated.AddListener(UpdateIcon);
            UpdateIcon();

        }

        public virtual void Set(T item, int amount)
        {
            inventory.AddItemToSlot(index, item, amount);
        }

        public virtual void AddAmount(int amount)
        {
            inventory.AddAmountToSlot(index, amount);
        }


        public virtual void Clear()
        {
            var slot = inventory.GetSlot(index);
            inventory.RemoveFromSlot(index, slot.amount);
        }

        public virtual int GetAmount()
        {
            return inventory.GetAmountInSlot(index);
        }

        public virtual T GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public virtual int MaxAcceptable(T item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public virtual void Remove(int amount)
        {
            // 個数は一旦考えない
            inventory.RemoveFromSlot(index, amount);
        }

        protected virtual void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetAmount());
        }
    }
}