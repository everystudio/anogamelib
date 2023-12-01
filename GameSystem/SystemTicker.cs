using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame
{

    public class SystemTicker : MonoBehaviour
    {
        private List<SystemCore> systems = new List<SystemCore>();

        private List<SystemCore> updateSystems = new List<SystemCore>();
        private List<SystemCore> fixedUpdateSystems = new List<SystemCore>();

        private void Awake()
        {
            Debug.Log("SystemTicker Awake");
            GetComponentsInChildren<SystemCore>(systems);

            foreach (SystemCore system in systems)
            {
                Debug.Log(system.GetType().Name);
                system.Initialize(this);

                if (system.systemSettings.hasFixedUpdate)
                {
                    fixedUpdateSystems.Add(system);
                }

                if (system.systemSettings.hasUpdate)
                {
                    updateSystems.Add(system);
                }
            }
        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < updateSystems.Count; i++)
            {
                updateSystems[i].Tick(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < fixedUpdateSystems.Count; i++)
            {
                fixedUpdateSystems[i].FixedTick(fixedDeltaTime);
            }
        }
    }
}