using System;
using Services.GameEvents;
using Services.Inventory;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace GameLogic.ItemDispatcher
{
    public class AmmunitionController : MonoBehaviour
    {
        public event Action<int> AmmunitionCountChanged;

        public int CurrentAmmunitionCount
        {
            get
            {
                if (_currentAmmunition == null)
                {
                    return 0;    
                }
                
                return _currentAmmunition.StackSize;
            }
        }

        private InventoryItem _currentAmmunition;
        
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(IGameEvent gameEvent)
        {
            _gameEvent = gameEvent;
        }

        public void AddAmmunition(InventoryItem ammunition)
        {
            _currentAmmunition = ammunition;
            AmmunitionCountChanged?.Invoke(_currentAmmunition.StackSize);
        }

        public void RemoveAmmunition(InventoryItem ammunition)
        {
            if (_currentAmmunition == ammunition)
            {
                _currentAmmunition = null;
                AmmunitionCountChanged?.Invoke(0);
            }
        }
        
        public void UpdateAmmunitionCount(InventoryItem ammunition)
        {
            if (_currentAmmunition == ammunition)
            {
                AmmunitionCountChanged?.Invoke(_currentAmmunition.StackSize);
            }
        }

        public void ChangeCurrentAmmoCount(int changeValue)
        {
            _currentAmmunition.StackSize += changeValue;
            AmmunitionCountChanged?.Invoke(_currentAmmunition.StackSize);
        }
    }
}
