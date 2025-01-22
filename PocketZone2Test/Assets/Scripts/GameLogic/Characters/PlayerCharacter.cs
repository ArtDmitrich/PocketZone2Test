using GameLogic.Weapons;
using Services.GameEvents;
using Services.Input;
using UnityEngine;
using Zenject;

namespace GameLogic.Characters
{
    public class PlayerCharacter : MovableCharacter
    {
        private Transform _currentWeapon;
        private IInputService _inputService;
        private IGameEvent _gameEvent;
        private WeaponsCotroller _weaponsCotroller;

        [Inject]
        private void Construct(IInputService inputService, IGameEvent gameEvent, WeaponsCotroller weaponsCotroller)
        {
            _inputService = inputService;
            _gameEvent = gameEvent;
            _weaponsCotroller = weaponsCotroller;
            
            Initialize();
        }
        
        private void SetWeapon(Transform weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.gameObject.SetActive(false);
            }
            
            _currentWeapon = weapon;
            _currentWeapon.gameObject.SetActive(true);
            
            Model.SetWeapon(_currentWeapon.transform);
        }
        
        private void RespawnPlayer()
        {
            Initialize();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _inputService.PlayerMoveStarted += StartMovement;
            _inputService.PlayerMoveStoped += StopMovement;

            _weaponsCotroller.OnWeaponSetted += SetWeapon;
            
            _gameEvent.AddSub(GameEventType.PlayerRespawn, RespawnPlayer);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _inputService.PlayerMoveStarted -= StartMovement;
            _inputService.PlayerMoveStoped -= StopMovement;

            _weaponsCotroller.OnWeaponSetted -= SetWeapon;
            
            _gameEvent.RemoveSub(GameEventType.PlayerRespawn, RespawnPlayer);
        }
    }
}
