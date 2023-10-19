using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace anogame
{
    [System.Serializable]
    public class UnityEventInt : UnityEvent<int> { }

    [System.Serializable]
    public class UnityEventGameObject : UnityEvent<GameObject> { }
}
