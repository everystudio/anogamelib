using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame.inventory
{
    public class ActionInventory : InventoryBase<InventoryItem>
    {
        /*
        Dictionary<int, DockedItemSlot> inventorySlotDatas = new Dictionary<int, DockedItemSlot>();
        private class DockedItemSlot
        {
            public ActionItem item;
            public int amount;
        }
        */
        //public event Action inventoryUpdated;

        public InventoryItem GetAction(int index)
        {
            return inventorySlotDatas[index].inventoryItem;
        }
        public int GetAmount(int index)
        {
            return inventorySlotDatas[index].amount;
        }
        /*
        public void AddAction(InventoryItem item, int index, int amount)
        {
            if (inventorySlotDatas.ContainsKey(index))
            {
                if (object.ReferenceEquals(item, inventorySlotDatas[index].item))
                {
                    inventorySlotDatas[index].amount += amount;
                }
            }
            else
            {
                var slot = new DockedItemSlot();
                slot.item = item as ActionItem;
                slot.amount = amount;
                inventorySlotDatas[index] = slot;
            }
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
        public bool Use(int index, GameObject user)
        {
            Debug.LogError("使う時確認必要");
            var actionItem = inventorySlotDatas[index].inventoryItem as ActionItem;

            actionItem.Use(user);
            if (actionItem.isConsumable())
            {
                RemoveItems(index, 1);
            }
            return true;
        }
        public void RemoveItems(int index, int amount)
        {
            inventorySlotDatas[index].amount -= amount;
            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }

        }
        */

        public int MaxAcceptable(InventoryItem item, int index)
        {
            var actionItem = item as IItemAction;
            if (actionItem == null)
            {
                return 0;
            }

            if (!object.ReferenceEquals(item, inventorySlotDatas[index].inventoryItem))
            {
                return 0;
            }
            if (actionItem.IsConsumable())
            {
                return int.MaxValue;
            }
            /*
            if (inventorySlotDatas.ContainsKey(index))
            {
                return 0;
            }
            */

            return 1;
        }


    }
}
