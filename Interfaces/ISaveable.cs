using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public interface ISaveable
    {
        string GetKey();
        string OnSave();
        void OnLoad(string json);
        bool IsSaveable();
    }
}