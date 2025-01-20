using System.Collections.Generic;
using Characters;
using GameLogic.Characters;
using GameLogic.EnemiesController;
using GameLogic.Weapons;
using Infrastructure;
using Services.Initialize;
using Services.Input;
using Services.Logger;
using Services.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerCharacter PlayerCharacterPrefab;
    [SerializeField] Weapon AK74Prefab;
    [SerializeField] Weapon MakarovPrefab;
    
    private IMediatorUI _mediator;
    private IInputService _inputService;
    private DiContainer _container;
    
    private PlayerCharacter _playerCharacter;
    private Weapon _aK74;
    private Weapon _makarov;
    
    private LinkedList<Weapon> _weapons = new LinkedList<Weapon>();
    private LinkedListNode<Weapon> _currentWeapon;
    
    private EnemiesController _enemiesController;
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    [Inject]
    private void Construct(IMediatorUI mediator, IInputService inputService, DiContainer container, EnemiesController enemiesController)
    {
        _mediator = mediator;
        _inputService = inputService;

        if (_inputService is IInitialazable input)
        {
            input.Init();
        }
        
        _container = container;
        _playerCharacter = _container.InstantiatePrefabForComponent<PlayerCharacter>(PlayerCharacterPrefab);
        _playerCharacter.transform.position = Vector3.zero;
        
        _aK74 = _container.InstantiatePrefabForComponent<Weapon>(AK74Prefab);
        _makarov = _container.InstantiatePrefabForComponent<Weapon>(MakarovPrefab);
        
        _aK74.gameObject.SetActive(false);
        _makarov.gameObject.SetActive(false);
        
        _weapons.AddLast(_aK74);
        _weapons.AddLast(_makarov);
        
        ChangeWeapon();
        
        _enemiesController = enemiesController;
        foreach (var spawnPoint in _spawnPoints)
        {
            _enemiesController.SpawnEnemies(spawnPoint);
        }
    }
    
    public void OpenTest()
    {
        _mediator.OpenView(ViewType.Test);
    }

    public void CloseTest()
    {
        _mediator.CloseView();
    }

    public void PlayerAttack()
    {
        _playerCharacter.Attack();
    }

    public void PlayerDeath()
    {
        _playerCharacter.TakeDamage(10f);
    }
    
    public void PlayerHurt()
    {
        _playerCharacter.TakeDamage(1f);
    } 
    
    public void RespawnPlayer()
    {
        _playerCharacter.RespawnPlayer();
    }

    public void ChangeWeapon()
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

    /*private void MoveStart(Vector2 direction)
    {
        LoggerService.Log($"Moving to {direction}");
    }
    
    private void MoveStop()
    {
        LoggerService.Log($"Stop moving");
    }

    private void OnEnable()
    {
        _inputService.PlayerMoveStarted += MoveStart;
        _inputService.PlayerMoveStoped += MoveStop;
    }
    
    private void OnDisable()
    {
        _inputService.PlayerMoveStarted -= MoveStart;
        _inputService.PlayerMoveStoped -= MoveStop;
    }*/
}
