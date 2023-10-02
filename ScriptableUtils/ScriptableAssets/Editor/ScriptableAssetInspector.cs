using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace anogame
{
    [CustomEditor(typeof(ScriptableAsset), true)]
    public class ScriptableAssetInspector : Editor
    {
        //private static ScriptableAsset currentTarget;
        private string currentGUID;

        public override void OnInspectorGUI()
        {
            if (string.IsNullOrEmpty(currentGUID))
            {
                currentGUID = $"GUID: {((IReferenceableAsset)target).GetGuid()}";
            }

            EditorGUILayout.TextArea(currentGUID, EditorStyles.centeredGreyMiniLabel);

            base.DrawDefaultInspector();
        }
    }
}