using System.Collections.Generic;
using Services.Helper;
using Services.Inventory;
using UnityEngine;
using Zenject;

namespace GameLogic.DroppedItem
{
    public class DroppedItemController : MonoBehaviour
    {
        [SerializeField] private List<string> _droppedItemNames = new List<string>();
        
        private InventoryController _inventoryController;

        [Inject]
        private void Construct(InventoryController inventoryController)
        {
            _inventoryController = inventoryController;
        }

        public void SeRandomDroppedItem(Vector2 position)
        {
            if (_droppedItemNames.Count == 0)
            {
                return;
            }
                
            var randomIndex = Random.Range(0, _droppedItemNames.Count);
            var randomDroppedItem = _droppedItemNames[randomIndex];
            
            SetDroppedItem(position, randomDroppedItem);
        }

        public void SetDroppedItem(Vector2 position, string droppedItemName)
        {
            var item = DroppedItemPoolManager.Instance.GetDroppedItem(droppedItemName);

            if (item != null)
            {
                item.transform.position = position;
                item.ItemPickUped += AddItemToInventory;
            }
        }

        private void AddItemToInventory(DroppedItem droppedItem)
        {
            var item = new InventoryItem(droppedItem.ItemType, droppedItem.name, droppedItem.IconPath, droppedItem.StackSize);
            
            _inventoryController.AddItemToInventory(item);
            droppedItem.ItemPickUped -= AddItemToInventory;
        }
    }
}
