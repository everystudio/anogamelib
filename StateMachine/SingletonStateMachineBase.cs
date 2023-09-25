using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public class SingletonStateMachineBase<T> : StateMachineBase<T> where T : SingletonStateMachineBase<T>
    {
        // シングルトンの処理はSingletonクラスの中をコピーして同じ手触りで使えるようにしてください
        protected static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        _instance.Initialize();
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

            if (_instance != this)
            {
                _instance = this as T;
                _instance.Initialize();
            }
        }

        public virtual void Initialize() { }
    }

}