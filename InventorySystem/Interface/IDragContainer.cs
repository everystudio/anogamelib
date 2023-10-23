using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public interface IDragContainer<T> : IDragTarget<T>, IDragSource<T> where T : class
    {

    }
}
