using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{
    public abstract class SystemCore : MonoBehaviour
    {
        [System.Serializable]
        public class Settings
        {
            public bool hasUpdate;
            public bool hasFixedUpdate;

            public float updateInterval;
            public float fixedUpdateInterval = 0;
        }
        public Settings systemSettings;

        private float currentUpdateTime;
        private float currentFixedUpdateTime;

        private bool isInitialized;
        private bool pausedUpdate;
        protected virtual void pause(bool state) { }
        public void Pause(bool state)
        {
            pausedUpdate = state;
            pause(state);
        }

        public void Initialize(SystemTicker ticker)
        {
            if (!isInitialized)
            {
                OnLoadSystem();
                isInitialized = true;
            }
        }

        public void Tick(float deltaTime)
        {
            if (pausedUpdate)
            {
                return;
            }

            currentUpdateTime += deltaTime;
            if (systemSettings.updateInterval == 0 || systemSettings.updateInterval <= currentUpdateTime)
            {
                // ここの処理は誤差を考慮する必要が出たら改善する
                float time = systemSettings.updateInterval == 0 ? deltaTime : systemSettings.updateInterval;
                OnTick(time);
                currentUpdateTime = 0;
            }
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (pausedUpdate)
                return;

            if (systemSettings.fixedUpdateInterval == 0 || systemSettings.fixedUpdateInterval <= currentFixedUpdateTime)
            {
                float time = systemSettings.fixedUpdateInterval == 0 ? fixedDeltaTime : systemSettings.fixedUpdateInterval;
                OnFixedTick(time);
                currentFixedUpdateTime = 0;
            }
            else
            {
                currentFixedUpdateTime += fixedDeltaTime;
            }
        }
        public abstract void OnLoadSystem();
        public virtual void OnTick(float deltaTime) { }
        public virtual void OnFixedTick(float deltaTime) { }
    }
}
