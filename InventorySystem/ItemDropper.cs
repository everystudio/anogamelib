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
        public void DropItem(InventoryItem item, int amount)
        {
            SpawnPickup(item, GetDropLocation(), amount);
        }

        public PickableItem SpawnPickup(InventoryItem item, Vector3 spawnLocation, int amount)
        {
            var pickup = item.SpawnPickableItem(spawnLocation, amount);
            droppedItems.Add(pickup);
            return pickup;
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
