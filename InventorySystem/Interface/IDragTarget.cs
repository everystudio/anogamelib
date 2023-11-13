using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IDragTarget<T> where T : class
    {
        int MaxAcceptable(T item);
        void Set(T item, int amount);
        void AddAmount(int amount);
    }
}
