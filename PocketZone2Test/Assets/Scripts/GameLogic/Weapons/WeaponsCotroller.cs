using System;
using System.Collections.Generic;
using GameLogic.ItemDispatcher;
using Services.GameEvents;
using UnityEngine;
using Zenject;

namespace GameLogic.Weapons
{
    public class WeaponsCotroller : MonoBehaviour
    {
        public event Action<Transform> OnWeaponSetted;
        
        public Weapon AK74Prefab;
        public Weapon MakarovPrefab;
        
        private LinkedList<Weapon> _weapons = new LinkedList<Weapon>();
        private LinkedListNode<Weapon> _currentWeapon;

        private DiContainer _container;
        private AmmunitionController _ammunitionController;
        private IGameEvent _gameEvent;
        
        [Inject]
        private void Construct(DiContainer container, AmmunitionController ammunitionController, IGameEvent gameEvent)
        {
            _container = container;
            _ammunitionController = ammunitionController;
            _gameEvent = gameEvent;
        }
        
        private void Start()
        {
            var aK74 = _container.InstantiatePrefabForComponent<Weapon>(AK74Prefab);
            var makarov = _container.InstantiatePrefabForComponent<Weapon>(MakarovPrefab);
        
            aK74.gameObject.SetActive(false);
            makarov.gameObject.SetActive(false);
        
            _weapons.AddLast(aK74);
            _weapons.AddLast(makarov);
        }

        private void ChangeWeapon()
        {
            if (_weapons.Count == 0)
            {
                return;
            }

            if (_currentWeapon == null || _currentWeapon == _weapons.Last)
            {
                _currentWeapon = _weapons.First;
                OnWeaponSetted?.Invoke(_currentWeapon.Value.transform);
            }
            else
            {
                _currentWeapon = _currentWeapon.Next;
                OnWeaponSetted?.Invoke(_currentWeapon.Value.transform);
            }
        }

        private void TryAttackWithCurrentWeapon()
        {
            if (!CheckAmmoCount())
            {
                _gameEvent.InvokeEvent(GameEventType.AmmoNotEnought);
                return;
            }

            if (_currentWeapon.Value.TryAttack())
            {
                _ammunitionController.ChangeCurrentAmmoCount(-1);
            }
        }
        
        private bool CheckAmmoCount()
        {
            return _ammunitionController.CurrentAmmunitionCount > 0;
        }

        private void OnEnable()
        {
            _gameEvent.AddSub(GameEventType.PlayerTryShoot, TryAttackWithCurrentWeapon);
            _gameEvent.AddSub(GameEventType.ChangeWeapon, ChangeWeapon);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveSub(GameEventType.PlayerTryShoot, TryAttackWithCurrentWeapon);            
            _gameEvent.RemoveSub(GameEventType.ChangeWeapon, ChangeWeapon);            
        }
    }
}
