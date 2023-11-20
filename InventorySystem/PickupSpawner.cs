using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] InventoryItem item = null;
        [SerializeField] int amount = 1;

        private PickableItem pickup = null;
        private void Awake()
        {
            // Spawn in Awake so can be destroyed by save system after.
            SpawnPickup();
        }
        public PickableItem GetPickup()
        {
            return GetComponentInChildren<PickableItem>();
        }
        public bool isCollected()
        {
            return GetPickup() == null;
        }
        private void SpawnPickup()
        {
            pickup = item.SpawnPickableItem(transform.position, amount);
            pickup.transform.SetParent(transform);
        }
        private void DestroyPickup()
        {
            if (GetPickup())
            {
                Destroy(GetPickup().gameObject);
            }
        }

        public void PickupItem()
        {
            if (GetPickup())
            {
                GetPickup().PickupItem();
            }
        }


    }
}
