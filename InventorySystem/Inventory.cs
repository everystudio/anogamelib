using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame.inventory
{
    public class Inventory : MonoBehaviour
    {
        public int capacity = 20;

        InventoryItem[] inventorySlots;
        public event Action inventoryUpdated;

        private void Awake()
        {
            inventorySlots = new InventoryItem[capacity];

            // テスト用
            // InventoryItemを作成して、IDを指定して格納
            inventorySlots[0] = InventoryItem.GetFromID("6840b2cb-f309-4162-b1b5-fa02e887c312");
            inventorySlots[1] = InventoryItem.GetFromID("9d0a5557-4b42-4030-b491-a17ad54f4083");


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
                if (inventorySlots[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSize()
        {
            return inventorySlots.Length;
        }

        public bool AddToFirstEmptySlot(InventoryItem item)
        {
            int i = FindSlot(item);

            if (i < 0)
            {
                return false;
            }

            inventorySlots[i] = item;
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
                if (object.ReferenceEquals(slot, item))
                {
                    return true;
                }
            }
            return false;
        }
        public InventoryItem GetItemInSlot(int slot)
        {
            // 範囲外チェック
            if (slot < 0 || slot >= inventorySlots.Length)
            {
                return null;
            }
            return inventorySlots[slot];
        }

        public void RemoveFromSlot(int slot)
        {
            // 範囲外チェック
            if (slot < 0 || slot >= inventorySlots.Length)
            {
                return;
            }

            inventorySlots[slot] = null;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
        }
        public bool AddItemToSlot(int slotIndex, InventoryItem item)
        {
            if (inventorySlots[slotIndex] != null)
            {
                return AddToFirstEmptySlot(item); ;
            }

            inventorySlots[slotIndex] = item;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }


    }

}
