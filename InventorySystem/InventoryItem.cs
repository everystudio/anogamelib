using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    [CreateAssetMenu(menuName = "ScriptableObject/Inventory Item")]
    public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private string itemID = null;

        [SerializeField] private string displayName = null;
        [SerializeField][TextArea] private string description = null;
        [SerializeField] private Sprite icon = null;
        [SerializeField] private PickableItem pickable = null;
        [SerializeField] private bool stackable = false;

        static Dictionary<string, InventoryItem> itemLookupCache;

        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    Debug.Log(item.displayName);
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
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
