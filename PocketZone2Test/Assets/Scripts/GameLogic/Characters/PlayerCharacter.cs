using GameLogic.Weapons;
using Services.GameEvents;
using Services.Input;
using Zenject;

namespace GameLogic.Characters
{
    public class PlayerCharacter : MovableCharacter
    {
        private Weapon _currentWeapon;
        private IInputService _inputService;
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(IInputService inputService, IGameEvent gameEvent)
        {
            _inputService = inputService;
            _gameEvent = gameEvent;
            
            Initialize();
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

        private void Attack()
        {
            _currentWeapon.Attack();
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
            
            _gameEvent.AddSub(GameEventType.PlayerShoot, Attack);
            _gameEvent.AddSub(GameEventType.PlayerRespawn, RespawnPlayer);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _inputService.PlayerMoveStarted -= StartMovement;
            _inputService.PlayerMoveStoped -= StopMovement;
            
            _gameEvent.RemoveSub(GameEventType.PlayerShoot, Attack);
            _gameEvent.RemoveSub(GameEventType.PlayerRespawn, RespawnPlayer);
        }
    }
}
