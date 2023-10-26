using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    [CreateAssetMenu(menuName = "ScriptableObject/Inventory Action Item")]
    public class ActionItem : InventoryItem
    {
        [SerializeField] bool consumable = false;
        public virtual void Use(GameObject user)
        {
            Debug.Log("Using action: " + this);
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}