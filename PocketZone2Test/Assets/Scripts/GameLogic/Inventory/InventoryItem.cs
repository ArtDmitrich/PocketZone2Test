using System;
using Services.Helper;
using Services.SaveSystem;
using UnityEngine;

namespace Services.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public InventoryItemType ItemType;
        public string ItemName; 
        public Sprite Icon;      
        public string IconPath;
        public int StackSize;

        public InventoryItem(InventoryItemType  itemType, string itemName, string iconPath, int stackSize)
        {
            ItemType = itemType;
            ItemName = itemName;
            IconPath = iconPath;
            StackSize = stackSize;
            Icon = HelperMethods.LoadIcon(iconPath);
        }
    }
}
