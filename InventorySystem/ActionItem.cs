using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    [CreateAssetMenu(menuName = "ScriptableObject/Inventory Action Item")]
    public class ActionItem : InventoryItem
    {
        [SerializeField] bool consumable = false;
        [SerializeField] private string animationTriggerName = "swing";
        public virtual void Use(GameObject user)
        {
            if (user.TryGetComponent<Animator>(out var animator))
            {
                animator.SetTrigger(animationTriggerName);
            }
            else
            {
                Debug.Log("No animator found");
            }
        }

        public bool isConsumable()
        {
            return consumable;
        }
    }
}