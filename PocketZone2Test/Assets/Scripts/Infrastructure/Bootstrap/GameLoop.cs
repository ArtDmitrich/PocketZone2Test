using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using GameLogic.Characters;
using GameLogic.EnemiesController;
using GameLogic.Weapons;
using Services.GameEvents;
using Services.Initialize;
using Services.Input;
using Services.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.Bootstrap
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        
        [SerializeField] private PlayerCharacter _playerCharacterPrefab;
        [SerializeField] private Transform _playerStartPoint;
        
        [SerializeField] private Weapon AK74Prefab;
        [SerializeField] private Weapon MakarovPrefab;
        
        [SerializeField] private string _enemyName;
        [SerializeField] private int _enemyCount;
        
        private DiContainer _container;
        private PlayerCharacter _playerCharacter;
        private EnemiesController _enemiesController;
        
        private Weapon _aK74;
        private Weapon _makarov;
    
        private LinkedList<Weapon> _weapons = new LinkedList<Weapon>();
        private LinkedListNode<Weapon> _currentWeapon;
        
        private IViewMediatorUI _viewMediator;
        private IInputService _inputService;
        private IGameEvent _gameEvent;
    
        [Inject]
        private void Construct(IViewMediatorUI viewMediator, IInputService inputService, EnemiesController enemiesController, IGameEvent gameEvent, DiContainer container)
        {
            _viewMediator = viewMediator;
            _inputService = inputService;

            if (_inputService is IInitialazable input)
            {
                input.Init();
            }
            
            _enemiesController = enemiesController;
            _gameEvent = gameEvent;
            
            _container = container;
            
        }

        private void Start()
        {
            _viewMediator.OpenView(ViewType.Menu);
        }

        private void GameStart()
        {
            _viewMediator.OpenView(ViewType.GameplayUI);
            
            _playerCharacter = _container.InstantiatePrefabForComponent<PlayerCharacter>(_playerCharacterPrefab);
            _playerCharacter.transform.position = _playerStartPoint.position;
            _playerCharacter.CharacterDead += PlayerLose;
            
            _inputService.SetEnableToCharacterInput(true);
            
            _camera.Follow = _playerCharacter.transform;
            _camera.LookAt = _playerCharacter.transform;
            
            _aK74 = _container.InstantiatePrefabForComponent<Weapon>(AK74Prefab);
            _makarov = _container.InstantiatePrefabForComponent<Weapon>(MakarovPrefab);
        
            _aK74.gameObject.SetActive(false);
            _makarov.gameObject.SetActive(false);
        
            _weapons.AddLast(_aK74);
            _weapons.AddLast(_makarov);
        
            ChangeWeapon();

            _enemiesController.AllEnemiesDie += PlayerWin;
            
            for (var i = 0; i < _enemyCount; i++)
            {
                var spawnPoint = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                _enemiesController.SpawnEnemies(spawnPoint, _enemyName);
            }
        }

        private void PlayerWin()
        {
            _enemiesController.AllEnemiesDie -= PlayerWin;

            GameEnd();
        }

        private void PlayerLose(Character playerCharacter)
        {
            _playerCharacter.CharacterDead -= PlayerLose;
            _inputService.SetEnableToCharacterInput(false);
            _viewMediator.CloseView();
            
            GameEnd();
        }

        private async void GameEnd()
        {
            await UniTask.DelayFrame(2000);
            
            //save progress
            
            _viewMediator.OpenView(ViewType.EndPanel);
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
                _playerCharacter.SetWeapon(_currentWeapon.Value);
            }
            else
            {
                _currentWeapon = _currentWeapon.Next;
                _playerCharacter.SetWeapon(_currentWeapon.Value);
            }
        }

        private void OnEnable()
        {
            _gameEvent.AddSub(GameEventType.GameStart, GameStart);
            _gameEvent.AddSub(GameEventType.ChangeWeapon, ChangeWeapon);
            _gameEvent.AddSub(GameEventType.GameOver, GameEnd);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveSub(GameEventType.GameStart, GameStart);
            _gameEvent.RemoveSub(GameEventType.ChangeWeapon, ChangeWeapon);
            _gameEvent.RemoveSub(GameEventType.GameOver, GameEnd);
        }
    }
}
