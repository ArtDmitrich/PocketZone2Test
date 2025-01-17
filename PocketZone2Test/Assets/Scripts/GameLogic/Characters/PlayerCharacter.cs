using Services.Input;
using UnityEngine;
using Weapons;

namespace Characters
{
    public class PlayerCharacter : MovableCharacter
    {
        [SerializeField] private Transform _weaponSlot;

        public Weapon _currentWeapon;
        private IInputService _inputService;

        public void Initialization(IInputService inputService)
        {
            _inputService = inputService;

            _inputService.PlayerMoveStarted += StartMovement;
            _inputService.PlayerMoveStoped += StopMovement;
        }

        public void Attack()
        {
            _currentWeapon.Attack();
        }

        public void SetWeapon(Weapon weapon)
        {
            _currentWeapon.gameObject.SetActive(false);
            
        }

    }
}
