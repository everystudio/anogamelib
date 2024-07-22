using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    [CreateAssetMenu(fileName = "New Craft Recipe", menuName = "ScriptableObject/Recipe")]
    public class CraftRecipe : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private string recipeID = null;

        [SerializeField] private InventoryItem resultItem = null;
        [SerializeField] private int resultAmount = 1;

        // 素材になるInventroyItemと個数のペアの構造体
        [System.Serializable]
        public class MaterialPair
        {
            public InventoryItem item;
            public int amount;
        }

        public MaterialPair[] materials;

        // クラフトが可能かどうかを判定
        public bool CanCraft(Inventory inventory, int multiple = 1)
        {
            foreach (var material in materials)
            {
                if (inventory.GetAmount(material.item) < material.amount * multiple)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Crafting(Inventory inventory, int multiple = 1)
        {
            if (!CanCraft(inventory))
            {
                return false;
            }

            try
            {
                // 素材を消費
                foreach (var material in materials)
                {
                    if (!inventory.RemoveItem(material.item, material.amount))
                    {
                        throw new System.Exception("Failed to remove item");
                    }
                    // 結果を追加
                    if (inventory.AddItemToSlot(resultItem, resultAmount))
                    {
                        throw new System.Exception("Failed to add item");
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                return false;
            }
            return true;
        }


        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
            if (string.IsNullOrWhiteSpace(recipeID))
            {
                recipeID = System.Guid.NewGuid().ToString();
            }

        }
    }
}