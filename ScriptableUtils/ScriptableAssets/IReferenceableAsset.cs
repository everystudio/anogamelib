using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public interface IReferenceableAsset
    {
        string GetGuid();
        void GenerateNewGuid();
    }
}