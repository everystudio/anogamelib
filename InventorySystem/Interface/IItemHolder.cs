using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IItemHolder<T> where T : InventoryItem
    {
        T GetItem();
    }
}