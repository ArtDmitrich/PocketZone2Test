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
        
        private InventoryItem _ammunition;
        
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(IGameEvent gameEvent)
        {
            _gameEvent = gameEvent;
        }

        public void AddAmmunition(InventoryItem ammunition)
        {
            _ammunition = ammunition;
            AmmunitionCountChanged?.Invoke(_ammunition.StackSize);
        }

        public void RemoveAmmunition(InventoryItem ammunition)
        {
            if (_ammunition == ammunition)
            {
                _ammunition = null;
                AmmunitionCountChanged?.Invoke(0);
            }
        }
        
        public void UpdateAmmunitionCount(InventoryItem ammunition)
        {
            if (_ammunition == ammunition)
            {
                AmmunitionCountChanged?.Invoke(_ammunition.StackSize);
            }
        }

        public bool CheckAmmoCount()
        {
            if (_ammunition == null || _ammunition.StackSize <= 0)
            {
                return false;
            }
            
            return true;
        }

        public void ChangeAmmoCount(int changeValue)
        {
            _ammunition.StackSize += changeValue;
            AmmunitionCountChanged?.Invoke(_ammunition.StackSize);
        }

        private void UpdateAmmoCountInStartGame()
        {
            AmmunitionCountChanged?.Invoke(_ammunition.StackSize);
            Debug.Log("Ammunition Count: " + _ammunition.StackSize);
        }

        private void OnEnable()
        {
            _gameEvent.AddSub(GameEventType.GameStart, UpdateAmmoCountInStartGame);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveSub(GameEventType.GameStart, UpdateAmmoCountInStartGame);
        }
    }
}
