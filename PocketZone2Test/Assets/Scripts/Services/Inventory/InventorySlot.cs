using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public event Action<InventoryItem> ItemDeleted;
        
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _stackSizeText;
        [SerializeField] private Button _delete;
        
        private InventoryItem _item;
        
        public void AddItem(InventoryItem newItem)
        {
            _item = newItem;
            _iconImage.sprite = _item.Icon;
            _iconImage.enabled = true;

            if (_item.StackSize > 1)
            {
                _stackSizeText.text = _item.StackSize.ToString();
                _stackSizeText.enabled = true;
            }
            else
            {
                _stackSizeText.enabled = false;
            }
        }

        private void DeleteItem()
        {
            ItemDeleted?.Invoke(_item);
            
            _item = null;
            _iconImage.sprite = null;
            _iconImage.enabled = false;
            _stackSizeText.enabled = false;
        }

        private void OnEnable()
        {
            _delete.onClick.AddListener(DeleteItem);
        }

        private void OnDisable()
        {
            _delete.onClick.RemoveListener(DeleteItem);
        }
    }
}
