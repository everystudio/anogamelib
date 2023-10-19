using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using anogame;

namespace anogame
{
    [AddComponentMenu("Events/EventGameObjectListener")]
    public class EventGameObjectListener : ScriptableEventListener<GameObject>
    {
        [SerializeField]
        protected EventGameObject eventObject;

        [SerializeField] protected UnityEventGameObject eventAction;

        protected override ScriptableEvent<GameObject> ScriptableEvent
        {
            get
            {
                return eventObject;
            }
        }

        protected override UnityEvent<GameObject> Action
        {
            get
            {
                return eventAction;
            }
        }
    }
}

