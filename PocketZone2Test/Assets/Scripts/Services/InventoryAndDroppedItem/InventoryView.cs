using System;
using System.Collections.Generic;
using Services.UI;
using UnityEngine;

namespace Services.InventoryAndDroppedItem
{
    public class InventoryView : View
    {
        public event Action<InventoryItem> OnInventoryItemDeleted;

        public int MAX_COUNT_SLOTS => _maxCountSlots;
        
        [SerializeField] private List<InventorySlot> _slots = new List<InventorySlot>(_maxCountSlots);

        private static int _maxCountSlots = 30;
        
        public void UpdateInventoryView(List<InventoryItem> items)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (i < items.Count)
                {
                    _slots[i].AddItem(items[i]);
                    _slots[i].gameObject.SetActive(true);
                }
                else
                {
                    _slots[i].gameObject.SetActive(false);
                }
            }
        }

        private void DeleteItem(InventoryItem item)
        {
            OnInventoryItemDeleted?.Invoke(item);
        }

        private void OnEnable()
        {
            foreach (var slot in _slots)
            {
                slot.ItemDeleted += DeleteItem;
            }
        }

        private void OnDisable()
        {
            foreach (var slot in _slots)
            {
                slot.ItemDeleted -= DeleteItem;
            }
        }
    }
}
