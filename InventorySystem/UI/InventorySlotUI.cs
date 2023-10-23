using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{

    public class InventorySlotUI : MonoBehaviour, IDragContainer<Sprite>
    {
        [SerializeField] InventoryItemIcon icon = null;

        public void Add(int amount)
        {
        }

        public void Clear()
        {
            icon.SetItem(null);
        }

        public int GetAmount()
        {
            return 1;
        }

        public Sprite GetItem()
        {
            return icon.GetItemSprite();
        }

        public int MaxAcceptable(Sprite item)
        {
            if (GetItem() == null)
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void Remove(int amount)
        {
            icon.SetItem(null);
        }

        public void Set(Sprite item, int amount)
        {
            icon.SetItem(item);
        }
    }
}