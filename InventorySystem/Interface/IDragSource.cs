using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IDragSource<T> where T : class
    {
        T GetItem();
        int GetAmount();
        void Remove(int amount);
        void Clear();
    }
}