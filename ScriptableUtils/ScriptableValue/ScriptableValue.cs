using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    [System.Serializable]
    public class ScriptableValue<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private T value;

        private T runtimeValue;

        [System.NonSerialized]
        private bool initialized;

        public void SetRuntimeValue(T value)
        {
            Value = value;
        }

        public T Value
        {
            get
            {
                if (!initialized)
                {
                    runtimeValue = value;
                    initialized = true;
                }
                return runtimeValue;
            }

            set
            {
                initialized = true;
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                runtimeValue = value;
            }
        }
        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
        }

    }
}