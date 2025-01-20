using GameLogic.Weapons;
using Services.Input;
using UnityEngine;
using Zenject;

namespace GameLogic.Characters
{
    public class PlayerCharacter : MovableCharacter
    {
        private Weapon _currentWeapon;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.PlayerMoveStarted += StartMovement;
            _inputService.PlayerMoveStoped += StopMovement;
            
            Initialize();
        }

        public void Attack()
        {
            _currentWeapon.Attack();
        }

        public void SetWeapon(Weapon weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.gameObject.SetActive(false);
            }
            
            _currentWeapon = weapon;
            _currentWeapon.gameObject.SetActive(true);
            
            Model.SetWeapon(_currentWeapon.transform);
        }

        public void RespawnPlayer()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            if (_inputService == null)
            {
                return;
            }
            
            _inputService.PlayerMoveStarted -= StartMovement;
            _inputService.PlayerMoveStoped -= StopMovement;
        }
    }
}
