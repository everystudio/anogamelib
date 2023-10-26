using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class ItemDropper : MonoBehaviour
    {
        private List<PickableItem> droppedItems = new List<PickableItem>();

        protected virtual Vector3 GetDropLocation()
        {
            return transform.position;
        }
        public void DropItem(InventoryItem item)
        {
            SpawnPickup(item, GetDropLocation());
        }

        public void SpawnPickup(InventoryItem item, Vector3 spawnLocation)
        {
            var pickup = item.SpawnPickableItem(spawnLocation);
            droppedItems.Add(pickup);
        }

        private void RemoveDestroyedDrops()
        {
            var newList = new List<PickableItem>();
            foreach (var item in droppedItems)
            {
                if (item != null)
                {
                    newList.Add(item);
                }
            }
            droppedItems = newList;
        }
    }
}
