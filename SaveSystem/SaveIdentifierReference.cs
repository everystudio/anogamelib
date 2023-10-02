using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace anogame
{
    [System.Serializable]
    public class SaveIdentifierReference
    {
        public bool UseConstant = true;

        [FormerlySerializedAs("saveIdentification")]
        public string ConstantValue = "";
        public SaveIdentifierVariable Variable;

        public SaveIdentifierReference()
        { }

        public SaveIdentifierReference(string value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public string Value
        {
            get { return UseConstant ? ConstantValue : Variable != null ? Variable.Identifier : ""; }
        }

        public static implicit operator string(SaveIdentifierReference reference)
        {
            return reference.Value;
        }
    }
}