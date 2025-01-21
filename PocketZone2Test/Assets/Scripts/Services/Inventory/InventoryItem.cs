using System;
using UnityEngine;

namespace Services.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string ItemName; 
        public Sprite Icon;      
        public int StackSize;  
    }
}
