using Services.Inventory;
using UnityEngine;
using Zenject;

namespace GameLogic.ItemDispatcher
{
    public class ItemDispatcher : MonoBehaviour
    {
        private InventoryController _inventoryController;
        private AmmunitionController _ammunitionController;

        [Inject]
        private void Construct(InventoryController inventoryController, AmmunitionController ammunitionController)
        {
            _inventoryController = inventoryController;
            _ammunitionController = ammunitionController;
        }

        private void AddItemToController(InventoryItem inventoryItem)
        {
            switch (inventoryItem.ItemType)
            {
                case InventoryItemType.Ammunition:
                    _ammunitionController.AddAmmunition(inventoryItem);
                    break;
                case InventoryItemType.Bag:
                default:
                    break;
            }
        }
        
        private void RemoveItemFromController(InventoryItem inventoryItem)
        {
            switch (inventoryItem.ItemType)
            {
                case InventoryItemType.Ammunition:
                    _ammunitionController.RemoveAmmunition(inventoryItem);
                    break;
                case InventoryItemType.Bag:
                default:
                    break;
            }
        }
        
        private void UpdateItemStackSizeInController(InventoryItem inventoryItem)
        {
            switch (inventoryItem.ItemType)
            {
                case InventoryItemType.Ammunition:
                    _ammunitionController.UpdateAmmunitionCount(inventoryItem);
                    break;
                case InventoryItemType.Bag:
                default:
                    break;
            }
        }

        private void OnEnable()
        {
            _inventoryController.OnInventoryItemAdded += AddItemToController;
            _inventoryController.OnInventoryItemRemoved += RemoveItemFromController;
            _inventoryController.OnInventoryItemStackSizeChanged += UpdateItemStackSizeInController;
        }

        private void OnDisable()
        {
            _inventoryController.OnInventoryItemAdded -= AddItemToController;
            _inventoryController.OnInventoryItemRemoved -= RemoveItemFromController;
            _inventoryController.OnInventoryItemStackSizeChanged -= UpdateItemStackSizeInController;
        }
    }
}
