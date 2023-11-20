using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class InventoryUIBase<T> : MonoBehaviour where T : InventoryItem
    {
        // CONFIG DATA
        [SerializeField] protected InventorySlotBase<T> InventoryItemPrefab = null;
        [SerializeField] protected Transform inventorySlotRoot = null;

        // CACHE
        [SerializeField] protected InventoryBase<T> targetInventory;

        private void Awake()
        {
            // インスペクターでセットを強要する
            targetInventory.inventoryUpdated.AddListener(Redraw);
        }

        private void Start()
        {
            Redraw();
        }

        // PRIVATE

        protected virtual void Redraw()
        {
            foreach (Transform child in inventorySlotRoot)
            {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < targetInventory.GetSize(); i++)
            {
                var itemUI = Instantiate(InventoryItemPrefab, inventorySlotRoot);
                itemUI.Setup(targetInventory, i);
                itemUI.name = $"{gameObject.name}.InventorySlot[{i}]";
            }
        }
    }
}
