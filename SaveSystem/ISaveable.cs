using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace anogame
{
    public interface ISaveable
    {
        string OnSave();
        void OnLoad(string data);
        bool OnSaveCondition();
    }
}