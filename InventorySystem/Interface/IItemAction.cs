using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IItemAction
    {
        bool Use(GameObject owner);
        bool IsConsumable();
    }
}