using Services.Inventory;

namespace Services.SaveSystem
{
    [System.Serializable]
    public struct InventoryItemData
    {
        public InventoryItemType ItemType;
        public string ItemName; 
        public string IconPath;      
        public int StackSize;
    }
}
