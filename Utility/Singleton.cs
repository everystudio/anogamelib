using System;
using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private bool _isInitialized = false;
    public bool dontDestroyOnLoad = false;
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //Debug.Log(Type.GetType(typeof(T).ToString()) + "のインスタンスを検索します");
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    //Debug.LogError("インスタンスが存在しません");
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }
                if (_instance._isInitialized == false)
                {
                    _instance.initialize();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance == null)
        {
            _instance = this as T;
            initialize();
        }
        else if (_instance == this)
        {
            //Debug.Log("外部からのアクセスで初期化済み");
        }
        else
        {
            Debug.LogError(Type.GetType(typeof(T).ToString()) + "のシングルトン");
            Debug.LogError("既にインスタンスが存在するため、削除します");
            Destroy(gameObject);
        }
    }

    private void initialize()
    {
        if (_isInitialized)
        {
            return;
        }

        // 継承先のInitializeの中でdontDestroyOnLoadをtrueにしても間に合うように変更
        _instance.Initialize();

        if (dontDestroyOnLoad)
        {
            transform.SetParent(null);
            Debug.Log("dontDestroyOnLoad:" + gameObject.name);
            DontDestroyOnLoad(gameObject);
        }
        _isInitialized = true;
    }

    public virtual void Initialize() { }
}