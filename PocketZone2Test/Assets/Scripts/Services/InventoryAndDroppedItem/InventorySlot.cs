using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.InventoryAndDroppedItem
{
    public class InventorySlot : MonoBehaviour
    {
        public event Action<InventoryItem> ItemDeleted;
        
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _stackSizeText;
        
        private InventoryItem _item;
        
        public void AddItem(InventoryItem newItem)
        {
            _item = newItem;
            _iconImage.sprite = _item.icon;
            _iconImage.enabled = true;

            if (_item.stackSize > 1)
            {
                _stackSizeText.text = _item.stackSize.ToString();
                _stackSizeText.enabled = true;
            }
            else
            {
                _stackSizeText.enabled = false;
            }
        }

        public void DeleteItem()
        {
            ItemDeleted?.Invoke(_item);
            
            _item = null;
            _iconImage.sprite = null;
            _iconImage.enabled = false;
            _stackSizeText.enabled = false;
        }
    }
}
