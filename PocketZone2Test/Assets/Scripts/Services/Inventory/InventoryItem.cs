using System;
using Services.Helper;
using Services.SaveSystem;
using UnityEngine;

namespace Services.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string ItemName; 
        public Sprite Icon;      
        public string IconPath;
        public int StackSize;

        public InventoryItem(string itemName, string iconPath, int stackSize)
        {
            ItemName = itemName;
            IconPath = iconPath;
            StackSize = stackSize;
            Icon = HelperMethods.LoadIcon(iconPath);
        }
    }
}
