using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class ActionInventoryUI : InventoryUIBase<InventoryItem>
    {
        public int selectingIndex = -1;
        [SerializeField] private int displaySize = 8;

        public ActionSlotUI[] slots;

        protected override void Redraw()
        {
            foreach (Transform child in inventorySlotRoot)
            {
                Destroy(child.gameObject);
            }

            int loop_count = Mathf.Min(displaySize, targetInventory.GetSize());

            slots = new ActionSlotUI[loop_count];

            for (int i = 0; i < loop_count; i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, inventorySlotRoot);
                itemUI.Setup(targetInventory, i);
                itemUI.name = $"{gameObject.name}.InventorySlot[{i}]";

                // できるやつをセットしてね
                slots[i] = itemUI as ActionSlotUI;
            }
            ShowSelectingFrame(selectingIndex);
        }

        private void ShowSelectingFrame(int selecting)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].Select(i == selecting);
            }
            targetInventory.Select(selecting, targetInventory.gameObject);
        }

        private void Update()
        {
            // マウスホイールで選択中のアイテムを変更する
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            if (wheel != 0)
            {
                selectingIndex -= (int)Mathf.Sign(wheel);
                if (selectingIndex < -1)
                {
                    selectingIndex = displaySize - 1;
                }
                else if (selectingIndex >= displaySize)
                {
                    selectingIndex = -1;
                }
                ShowSelectingFrame(selectingIndex);
                //Debug.Log(selectingIndex);
            }

            if (0 <= selectingIndex)
            {
                // スペースキーで選択中のアイテムを使用する
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    targetInventory.Use(selectingIndex, targetInventory.gameObject);
                }

            }


        }
    }
}