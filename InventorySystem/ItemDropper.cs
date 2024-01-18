using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anogame.inventory
{
    public class ItemDropper : MonoBehaviour
    {
        //private List<PickableItem> droppedItems = new List<PickableItem>();

        [System.Serializable]
        public class DroppableItem
        {
            public InventoryItem item;
            public Vector2Int amountRange;
            public Vector2Int dropCountRange;
        }

        [SerializeField] private DroppableItem[] dropItemArray;
        [SerializeField] private float dropRadius = 0.5f;
        private Vector2 offset = new Vector2(0f, 0f);

        protected virtual Vector3 GetDropLocation()
        {
            return transform.position + (Vector3)offset;
        }
        protected virtual Vector2 GetDropLocation2D()
        {
            return transform.position + (Vector3)offset;
        }

        public void SetOffset(Vector2 offset)
        {
            this.offset = offset;
        }

        public void DropItem()
        {
            for (int i = 0; i < dropItemArray.Length; i++)
            {
                int dropCount = Random.Range(dropItemArray[i].dropCountRange.x, dropItemArray[i].dropCountRange.y);

                for (int dropCountIndex = 0; dropCountIndex < dropCount; dropCountIndex++)
                {
                    Vector2 dropPoint = GetDropLocation2D() + Random.insideUnitCircle * dropRadius;

                    int dropAmount = Random.Range(dropItemArray[i].amountRange.x, dropItemArray[i].amountRange.y);
                    SpawnPickup(dropItemArray[i].item, dropPoint, dropAmount);
                }
            }
        }

        public void DropItem(InventoryItem item, int amount)
        {
            SpawnPickup(item, GetDropLocation(), amount);
        }

        public PickableItemBase SpawnPickup(InventoryItem item, Vector3 spawnLocation, int amount)
        {
            var pickup = item.SpawnPickableItem(spawnLocation, amount);
            //droppedItems.Add(pickup);
            return pickup;
        }

        /*
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
        */
    }
}
