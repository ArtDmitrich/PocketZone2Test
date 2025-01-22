using System;
using Services.Inventory;
using Services.ObjectPool;
using UnityEngine;

namespace GameLogic.DroppedItem
{
    public class DroppedItem : MonoBehaviour
    {
        public event Action<DroppedItem> ItemPickUped;
        public int StackSize => _stackSize;

        public string IconPath;
        public InventoryItemType ItemType;
        
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private int _stackSize;
        
        private PooledItem PooledItem { get { return _pooledItem ??= GetComponent<PooledItem>(); } }
        private PooledItem _pooledItem;

        private void TargetPickUpItem()
        {
            ItemPickUped?.Invoke(this);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((_targetLayer & (1 << collision.gameObject.layer)) != 0)
            {
                TargetPickUpItem();
            }

            Deactivate();
        }

        private void Deactivate()
        {
            if (gameObject.activeInHierarchy)
            {
                PooledItem.Release();
            }
        }
    }
}
