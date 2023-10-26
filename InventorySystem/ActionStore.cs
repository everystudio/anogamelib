using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame.inventory
{
    public class ActionStore : MonoBehaviour
    {
        Dictionary<int, DockedItemSlot> dockedItems = new Dictionary<int, DockedItemSlot>();
        private class DockedItemSlot
        {
            public ActionItem item;
            public int amount;
        }
        public event Action storeUpdated;
        public ActionItem GetAction(int index)
        {
            if (dockedItems.ContainsKey(index))
            {
                return dockedItems[index].item;
            }
            return null;
        }
        public int GetAmount(int index)
        {
            if (dockedItems.ContainsKey(index))
            {
                return dockedItems[index].amount;
            }
            return 0;
        }
        public void AddAction(InventoryItem item, int index, int amount)
        {
            if (dockedItems.ContainsKey(index))
            {
                if (object.ReferenceEquals(item, dockedItems[index].item))
                {
                    dockedItems[index].amount += amount;
                }
            }
            else
            {
                var slot = new DockedItemSlot();
                slot.item = item as ActionItem;
                slot.amount = amount;
                dockedItems[index] = slot;
            }
            if (storeUpdated != null)
            {
                storeUpdated();
            }
        }
        public bool Use(int index, GameObject user)
        {
            if (dockedItems.ContainsKey(index))
            {
                dockedItems[index].item.Use(user);
                if (dockedItems[index].item.isConsumable())
                {
                    RemoveItems(index, 1);
                }
                return true;
            }
            return false;
        }
        public void RemoveItems(int index, int amount)
        {
            if (dockedItems.ContainsKey(index))
            {
                dockedItems[index].amount -= amount;
                if (dockedItems[index].amount <= 0)
                {
                    dockedItems.Remove(index);
                }
                if (storeUpdated != null)
                {
                    storeUpdated();
                }
            }

        }
        public int MaxAcceptable(InventoryItem item, int index)
        {
            var actionItem = item as ActionItem;
            if (!actionItem) return 0;

            if (dockedItems.ContainsKey(index) && !object.ReferenceEquals(item, dockedItems[index].item))
            {
                return 0;
            }
            if (actionItem.isConsumable())
            {
                return int.MaxValue;
            }
            if (dockedItems.ContainsKey(index))
            {
                return 0;
            }

            return 1;
        }


    }
}
