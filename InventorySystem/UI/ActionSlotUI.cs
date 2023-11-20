using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class ActionSlotUI : InventorySlotBase<InventoryItem>
    {
        [SerializeField] private GameObject selectingFrame;

        public void Select(bool flag)
        {
            selectingFrame.SetActive(flag);
        }

        /*
        public override void Set(InventoryItem item, int amount)
        {
            inventory.AddItemToSlot(index, item, amount);
        }
        public override void AddAmount(int amount)
        {
            // 処理なし
            Debug.Log("未実装");
        }

        public override void Clear()
        {
            Remove(GetAmount());
        }
        */

    }
}
