using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] InventorySlot InventoryItemPrefab = null;
        [SerializeField] private Transform inventorySlotRoot = null;

        // CACHE
        Inventory playerInventory;

        private void Awake()
        {
            playerInventory = Inventory.GetPlayerInventory();
            playerInventory.inventoryUpdated.AddListener(Redraw);
        }

        private void Start()
        {
            Redraw();
        }

        // PRIVATE

        private void Redraw()
        {
            foreach (Transform child in inventorySlotRoot)
            {
                Destroy(child.gameObject);
            }
            //Debug.Log(playerInventory);
            for (int i = 0; i < playerInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, inventorySlotRoot);
                itemUI.Setup(playerInventory, i);
                itemUI.name = $"InventorySlot[{i}]";
            }
        }
    }

}