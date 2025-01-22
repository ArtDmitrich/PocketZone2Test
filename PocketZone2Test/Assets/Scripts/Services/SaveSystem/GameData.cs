using System.Collections.Generic;
using Services.Inventory;

namespace Services.SaveSystem
{
    public class GameData
    {
        public List<InventoryItemData> InventoryItemDatas = new();

        public void SetData(List<InventoryItem> inventoryItems)
        {
            InventoryItemDatas = new List<InventoryItemData>();
            
            foreach (var item in inventoryItems)
            {
                var itemData = new InventoryItemData()
                {
                    ItemName = item.ItemName,
                    IconPath = item.IconPath,
                    StackSize = item.StackSize,
                };
                
                InventoryItemDatas.Add(itemData);
            }
        }

        public List<InventoryItem> GetInventoryItems()
        {
            var inventoryItems = new List<InventoryItem>();
            
            if (InventoryItemDatas == null || InventoryItemDatas.Count == 0)
            {
                return inventoryItems;
            }

            foreach (var itemData in InventoryItemDatas)
            {
                var item = new InventoryItem(itemData.ItemType, itemData.ItemName, itemData.IconPath, itemData.StackSize);
                inventoryItems.Add(item);
            }
            
            return inventoryItems;
        }
    }
}
