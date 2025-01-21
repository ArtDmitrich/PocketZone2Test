using System.Collections.Generic;
using Services.GameEvents;
using Services.Logger;
using Services.UI;
using UnityEngine;
using Zenject;

namespace Services.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        
        private IViewMediatorUI _viewMediator;
        private InventoryView _inventoryView;
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(IGameEvent gameEvent, InventoryView inventoryView, IViewMediatorUI viewMediator)
        {
            _gameEvent = gameEvent;
            _inventoryView = inventoryView;
            _viewMediator = viewMediator;
        }

        public void AddItemToInventory(InventoryItem inventoryItem)
        {
            if (TryFindItemInInventory(inventoryItem.ItemName, out var foundInventoryItem))
            {
                foundInventoryItem.StackSize += inventoryItem.StackSize;
            }
            else
            {
                if (_inventoryItems.Count > _inventoryView.MAX_COUNT_SLOTS)
                {
                    LoggerService.LogError($"Inventory Full. {inventoryItem.ItemName} don`t added.");
                    return;
                }
                
                _inventoryItems.Add(inventoryItem);
            }
        }

        private void RemoveItemFromInventory(InventoryItem inventoryItem)
        {
            if (_inventoryItems.Contains(inventoryItem))
            {
                _inventoryItems.Remove(inventoryItem);
            }
            
            _viewMediator.CloseView();
            OpenInventory();
        }

        private void OpenInventory()
        {
            _inventoryView.UpdateInventoryView(_inventoryItems);
            _viewMediator.OpenView(ViewType.Inventory);
        }

        private void CloseInventory()
        {
            _viewMediator.OpenView(ViewType.GameplayUI);
        }

        private bool TryFindItemInInventory(string itemName, out InventoryItem targetItem)
        {
            foreach (var item in _inventoryItems)
            {
                if (item.ItemName == itemName)
                {
                    targetItem = item;
                    return true;
                }
            }

            targetItem = null;
            return false;
        }

        private void OnEnable()
        {
            _gameEvent.AddSub(GameEventType.OpenInventory, OpenInventory);
            _gameEvent.AddSub(GameEventType.CloseInventory, CloseInventory);
            _inventoryView.OnInventoryItemDeleted += RemoveItemFromInventory;
        }

        private void OnDisable()
        {
            _gameEvent.RemoveSub(GameEventType.OpenInventory, OpenInventory);
            _gameEvent.RemoveSub(GameEventType.CloseInventory, CloseInventory);
            _inventoryView.OnInventoryItemDeleted -= RemoveItemFromInventory;
        }
    }
}
