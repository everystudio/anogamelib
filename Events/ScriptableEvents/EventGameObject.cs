using UnityEngine;
using System.Collections;

namespace anogame
{
    [CreateAssetMenu(menuName = "Events/GameObject Event")]
    public class EventGameObject : ScriptableEvent<GameObject>
    {
    }
}
