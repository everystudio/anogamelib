using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace anogame.inventory
{
    public class InventoryBase<T> : MonoBehaviour where T : InventoryItem
    {
        public int capacity = 20;

        protected InventorySlotData[] inventorySlotDatas;
        public UnityEvent inventoryUpdated;

        public struct InventorySlotData
        {
            public int amount;
            public T inventoryItem;
        }

        private void Awake()
        {
            inventorySlotDatas = new InventorySlotData[capacity];
        }

        public InventorySlotData GetSlot(int index)
        {
            // 範囲外チェック
            if (index < 0 || index >= inventorySlotDatas.Length)
            {
                Debug.Log("Index is out of range");
                //return null;
            }
            return inventorySlotDatas[index];
        }

        // テスト用
        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        public bool HasSpaceFor(T item)
        {
            return FindSlot(item) >= 0;
        }

        private int FindSlot(T item)
        {
            return FindEmptySlot();
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < inventorySlotDatas.Length; i++)
            {
                if (inventorySlotDatas[i].inventoryItem == null)
                {
                    return i;
                }
            }
            Debug.Log("Inventory is full");
            return -1;
        }

        public int GetSize()
        {
            return inventorySlotDatas.Length;
        }

        public bool AddToFirstEmptySlot(T item, int amount)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            inventorySlotDatas[i].inventoryItem = item;
            inventorySlotDatas[i].amount = amount;

            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }
            return true;
        }
        public bool HasItem(T item)
        {
            foreach (var slot in inventorySlotDatas)
            {
                if (object.ReferenceEquals(slot.inventoryItem, item))
                {
                    return true;
                }
            }
            return false;
        }
        public T GetItemInSlot(int slotIndex)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlotDatas.Length)
            {
                return null;
            }
            return inventorySlotDatas[slotIndex].inventoryItem;
        }

        public int GetAmountInSlot(int slotIndex)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlotDatas.Length)
            {
                return -1;
            }
            return inventorySlotDatas[slotIndex].amount;
        }

        public void RemoveFromSlot(int slotIndex, int amount)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlotDatas.Length)
            {
                return;
            }
            inventorySlotDatas[slotIndex].amount -= amount;
            if (inventorySlotDatas[slotIndex].amount <= 0)
            {
                inventorySlotDatas[slotIndex].inventoryItem = null;
                inventorySlotDatas[slotIndex].amount = 0;
            }

            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }
        }

        public void AddAmountToSlot(int slotIndex, int amount)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlotDatas.Length)
            {
                return;
            }
            inventorySlotDatas[slotIndex].amount += amount;
            if (inventorySlotDatas[slotIndex].amount <= 0)
            {
                inventorySlotDatas[slotIndex].inventoryItem = null;
                inventorySlotDatas[slotIndex].amount = 0;
            }

            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }
        }

        public bool AddItemToSlot(int slotIndex, T item, int amount)
        {
            if (inventorySlotDatas[slotIndex].inventoryItem != null)
            {
                return AddToFirstEmptySlot(item, amount);
            }

            inventorySlotDatas[slotIndex].inventoryItem = item;
            inventorySlotDatas[slotIndex].amount += amount;
            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }
            return true;
        }

        public bool AddItemToSlot(T item, int amount)
        {
            // 同じInventoryItemのスロットを集める

            for (int i = 0; i < inventorySlotDatas.Length; i++)
            {
                if (inventorySlotDatas[i].inventoryItem == item)
                {
                    int capacity = 99 - inventorySlotDatas[i].amount;
                    if (amount <= capacity)
                    {
                        inventorySlotDatas[i].amount += amount;
                        inventoryUpdated.Invoke();
                        return true;
                    }
                    else
                    {
                        inventorySlotDatas[i].amount = 99;
                        amount -= capacity;
                    }
                }
            }
            if (0 < amount)
            {
                return AddToFirstEmptySlot(item, amount);
            }
            return false;
        }

        public bool Use(int index, GameObject user)
        {
            //Debug.LogError("使う時確認必要");
            var actionItem = inventorySlotDatas[index].inventoryItem as ActionItem;
            if (actionItem == null)
            {
                return false;
            }

            actionItem.Use(user);
            if (actionItem.isConsumable())
            {
                RemoveFromSlot(index, 1);
            }
            return true;
        }

    }

}