using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;

        // テスト用
        ActionStore store;


        private void Awake()
        {
            store = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionStore>();
            store.storeUpdated += UpdateIcon;
            UpdateIcon();
        }

        /*
        public void Set(InventoryItem item, int amount)
        {
            store.AddAction(item, index, amount);
        }
        */

        public void Add(InventoryItem item, int amount)
        {
            store.AddAction(item, index, amount);
        }

        public void Clear()
        {
            Remove(GetAmount());
        }

        public int GetAmount()
        {
            return store.GetAmount(index);
        }

        public InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int MaxAcceptable(InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void Remove(int amount)
        {
            store.RemoveItems(index, amount);
        }

        // PRIVATE

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetAmount());
        }
    }
}
