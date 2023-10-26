using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame.inventory
{
    public class Inventory : MonoBehaviour
    {
        public int capacity = 20;

        InventorySlot[] inventorySlots;
        public event Action inventoryUpdated;

        public struct InventorySlot
        {
            public int amount;
            public InventoryItem inventoryItem;
        }

        private void Awake()
        {
            inventorySlots = new InventorySlot[capacity];
            /*
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                inventorySlots[i] = new InventorySlot();
            }
            */

            // テスト用
            // InventoryItemを作成して、IDを指定して格納
            //inventorySlots[0] = InventorySlot.GetFromID("6840b2cb-f309-4162-b1b5-fa02e887c312");
            //inventorySlots[1] = InventorySlot.GetFromID("9d0a5557-4b42-4030-b491-a17ad54f4083");


        }

        public InventorySlot GetSlot(int index)
        {
            /*
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                Debug.Log(i.ToString() + ":" + inventorySlots[i]);
            }
            */

            // 範囲外チェック
            if (index < 0 || index >= inventorySlots.Length)
            {
                Debug.Log("Index is out of range");
                //return null;
            }
            return inventorySlots[index];
        }

        // テスト用
        public static Inventory GetPlayerInventory()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<Inventory>();
        }

        public bool HasSpaceFor(InventoryItem item)
        {
            return FindSlot(item) >= 0;
        }

        private int FindSlot(InventoryItem item)
        {
            return FindEmptySlot();
        }

        private int FindEmptySlot()
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].inventoryItem == null)
                {
                    return i;
                }
            }
            Debug.Log("Inventory is full");
            return -1;
        }

        public int GetSize()
        {
            return inventorySlots.Length;
        }

        public bool AddToFirstEmptySlot(InventoryItem item, int amount)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            inventorySlots[i].inventoryItem = item;
            inventorySlots[i].amount = amount;

            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }
        public bool HasItem(InventoryItem item)
        {
            foreach (var slot in inventorySlots)
            {
                if (object.ReferenceEquals(slot.inventoryItem, item))
                {
                    return true;
                }
            }
            return false;
        }
        public InventoryItem GetItemInSlot(int slotIndex)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlots.Length)
            {
                return null;
            }
            return inventorySlots[slotIndex].inventoryItem;
        }

        public int GetAmountInSlot(int slotIndex)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlots.Length)
            {
                return -1;
            }
            return inventorySlots[slotIndex].amount;
        }

        public void RemoveFromSlot(int slotIndex)
        {
            // 範囲外チェック
            if (slotIndex < 0 || slotIndex >= inventorySlots.Length)
            {
                return;
            }

            inventorySlots[slotIndex].inventoryItem = null;
            inventorySlots[slotIndex].amount = 0;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
        public bool AddItemToSlot(int slotIndex, InventoryItem item, int amount)
        {

            if (inventorySlots[slotIndex].inventoryItem != null)
            {
                return AddToFirstEmptySlot(item, amount);
            }

            inventorySlots[slotIndex].inventoryItem = item;
            inventorySlots[slotIndex].amount = amount;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }


    }

}
