using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public enum EncryptionType
    {
        None,
        AES
    }
    public enum InstanceSource { Resources, Custom }
    public enum LoadTrigger
    {
        OnSlotChanged,
        OnSyncLoad,
        OnDestroy,
        OnDisable,
        OnEnable,
        OnStart,
        Manual
    }
    public enum SaveFileValidation
    {
        DontCheck = 0,
        GiveError = 1,
        ConvertToType = 2,
        Replace = 3
    }
    public enum StorageType
    {
        JSON = 0,
        Binary = 1,
        SQLiteExperimental = 2
    }
}
