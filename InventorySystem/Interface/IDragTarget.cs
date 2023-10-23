using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IDragTarget<T> where T : class
    {
        int MaxAcceptable(T item);
        void Add(int amount);
        void Set(T item, int amount);
    }
}
