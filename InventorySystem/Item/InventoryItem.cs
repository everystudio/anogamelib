using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace anogame.inventory
{
    public abstract class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private string itemID = null;

        [SerializeField] private string displayName = null;
        [SerializeField][TextArea] private string description = null;
        [SerializeField] private Sprite icon = null;
        [SerializeField] private PickableItem pickable = null;
        [SerializeField] private bool stackable = false;

        private bool isSelecting = false;
        private PickableItem selectingDisplay = null;

        static Dictionary<string, InventoryItem> itemLookupCache;

        // これはいずれ修正すると思う
        public static UnityEvent<InventoryItem> OnAnyItemSelect = new UnityEvent<InventoryItem>();
        public static UnityEvent<InventoryItem> OnAnyItemDeselect = new UnityEvent<InventoryItem>();

        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    //Debug.Log(item.displayName);
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Duplicateしてない? IDが被るのでCreateから作成してください objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public bool IsStackable()
        {
            return stackable;
        }

        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        public void Select(GameObject owner)
        {
            if (!isSelecting)
            {
                isSelecting = true;
                select(owner);
                OnAnyItemSelect.Invoke(this);
            }
        }
        public void Deselect(GameObject owner)
        {
            if (isSelecting)
            {
                isSelecting = false;
                deselect(owner);

                OnAnyItemDeselect.Invoke(this);
            }
        }

        // 頭の上に表示している処理。これがデフォルトで良いのかは疑問
        protected virtual void select(GameObject owner)
        {
            selectingDisplay = Instantiate(pickable, owner.transform.position + new Vector3(0, 1f, 0), Quaternion.identity, owner.transform);
            selectingDisplay.SetItem(this, 1);
        }

        protected virtual void deselect(GameObject owner)
        {
            if (selectingDisplay != null)
            {
                Destroy(selectingDisplay.gameObject);
            }
        }

        public PickableItem SpawnPickableItem(Vector3 position, int amount)
        {
            var pickableItem = Instantiate(pickable, position, Quaternion.identity);
            pickableItem.SetItem(this, amount);
            return pickableItem;
        }

        public void OnAfterDeserialize()
        {
            // Intentionally left blank
        }

        public void OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }
    }
}
