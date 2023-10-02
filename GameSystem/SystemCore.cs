using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected void Pauze(bool state)
    {
        pausedUpdate = state;
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
            return;

        if (systemSettings.updateInterval == 0 || systemSettings.updateInterval <= currentUpdateTime)
        {
            OnTick();
            currentUpdateTime = 0;
        }
        else
        {
            currentUpdateTime += deltaTime;
        }
    }

    public void FixedTick(float fixedDeltaTime)
    {
        if (pausedUpdate)
            return;

        if (systemSettings.fixedUpdateInterval == 0 || systemSettings.fixedUpdateInterval <= currentFixedUpdateTime)
        {
            OnFixedTick();
            currentFixedUpdateTime = 0;
        }
        else
        {
            currentFixedUpdateTime += fixedDeltaTime;
        }
    }
    public abstract void OnLoadSystem();
    public virtual void OnTick() { }
    public virtual void OnFixedTick() { }
}
