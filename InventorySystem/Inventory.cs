using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace anogame.inventory
{
    public class Inventory : InventoryBase<InventoryItem>, ISaveable
    {

        protected virtual string GetSaveKey()
        {
            return "playerInventory";
        }

        public string GetKey()
        {
            return GetSaveKey();
        }

        public bool IsSaveable()
        {
            return true;
        }

        public void OnLoad(string json)
        {
            //Debug.Log("Inventory.OnLoad");
            var saveData = JsonUtility.FromJson<SaveDataInventory>(json);

            //Debug.Log("saveData.Length: " + saveData.inventorySlotDatas.Length);
            for (int i = 0; i < saveData.inventorySlotDatas.Length; i++)
            {
                //Debug.Log(i);
                //Debug.Log($"saveData[i].itemID={saveData.inventorySlotDatas[i].itemID} amount={saveData.inventorySlotDatas[i].amount}");
                inventorySlotDatas[i].inventoryItem = InventoryItem.GetFromID(saveData.inventorySlotDatas[i].itemID);
                inventorySlotDatas[i].amount = saveData.inventorySlotDatas[i].amount;
            }

            if (inventoryUpdated != null)
            {
                inventoryUpdated.Invoke();
            }
        }

        public string OnSave()
        {
            //Debug.Log("Inventory.OnSave");
            SaveDataInventory saveData = new SaveDataInventory();
            saveData.capacity = capacity;
            saveData.inventorySlotDatas = new SaveDataInventorySlot[inventorySlotDatas.Length];

            //Debug.Log("inventorySlotDatas.Length: " + inventorySlotDatas.Length);
            for (int i = 0; i < inventorySlotDatas.Length; i++)
            {
                if (inventorySlotDatas[i].inventoryItem != null)
                {
                    saveData.inventorySlotDatas[i].itemID = inventorySlotDatas[i].inventoryItem.GetItemID();
                    saveData.inventorySlotDatas[i].amount = inventorySlotDatas[i].amount;
                }
                else
                {
                    saveData.inventorySlotDatas[i].itemID = "";
                    saveData.inventorySlotDatas[i].amount = 0;

                }
            }

            /*
            foreach (var item in saveData.inventorySlotDatas)
            {
                Debug.Log($"itemID={item.itemID} amount={item.amount}");
            }
            */

            //Debug.Log(JsonUtility.ToJson(saveData.inventorySlotDatas[0]));
            //Debug.Log(JsonUtility.ToJson(saveData));

            return JsonUtility.ToJson(saveData);
        }


        /*
        public int capacity = 20;

        InventorySlotData[] inventorySlotDatas;
        public event Action inventoryUpdated;

        public struct InventorySlotData
        {
            public int amount;
            public InventoryItem inventoryItem;
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

        public bool AddToFirstEmptySlot(InventoryItem item, int amount)
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
                inventoryUpdated();
            }
            return true;
        }
        public bool HasItem(InventoryItem item)
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
        public InventoryItem GetItemInSlot(int slotIndex)
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
                inventoryUpdated();
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
                inventoryUpdated();
            }
        }



        public bool AddItemToSlot(int slotIndex, InventoryItem item, int amount)
        {
            if (inventorySlotDatas[slotIndex].inventoryItem != null)
            {
                return AddToFirstEmptySlot(item, amount);
            }

            inventorySlotDatas[slotIndex].inventoryItem = item;
            inventorySlotDatas[slotIndex].amount += amount;
            if (inventoryUpdated != null)
            {
                inventoryUpdated();
            }
            return true;
        }

        public bool AddItemToSlot(InventoryItem item, int amount)
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
                        inventoryUpdated();
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
*/

    }

}
