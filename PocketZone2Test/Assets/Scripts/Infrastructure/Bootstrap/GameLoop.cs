using Cinemachine;
using Cysharp.Threading.Tasks;
using GameLogic.Characters;
using GameLogic.DroppedItem;
using GameLogic.EnemiesController;
using Services.GameEvents;
using Services.Initialize;
using Services.Input;
using Services.Inventory;
using Services.Logger;
using Services.SaveSystem;
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
        
        [SerializeField] private string _enemyName;
        [SerializeField] private int _enemyCount;
        
        [SerializeField] private string _firstDroppedItemName;
        [SerializeField] private Transform _firstDroppedItemPos;
        
        private DiContainer _container;
        private PlayerCharacter _playerCharacter;
        private EnemiesController _enemiesController;
        
        private IViewMediatorUI _viewMediator;
        private IInputService _inputService;
        private IGameEvent _gameEvent;
        private InventoryController _inventoryController;
        private DroppedItemController _droppedItemController;
    
        [Inject]
        private void Construct(IViewMediatorUI viewMediator, IInputService inputService,
            EnemiesController enemiesController, IGameEvent gameEvent, DiContainer container, 
            InventoryController inventoryController, DroppedItemController droppedItemController)
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
            _inventoryController = inventoryController;
            _droppedItemController = droppedItemController;
        }

        private void Start()
        {
            _viewMediator.OpenView(ViewType.Menu);
            
            //LoadData
            LoadData();
        }

        private void GameStart()
        {
            _viewMediator.OpenView(ViewType.GameplayUI);
            
            _playerCharacter = _container.InstantiatePrefabForComponent<PlayerCharacter>(_playerCharacterPrefab);
            _playerCharacter.transform.position = _playerStartPoint.position;
            _playerCharacter.CharacterDead += PlayerLose;
            _gameEvent.InvokeEvent(GameEventType.ChangeWeapon);
            
            _inputService.SetEnableToCharacterInput(true);
            
            _camera.Follow = _playerCharacter.transform;
            _camera.LookAt = _playerCharacter.transform;
            
            _enemiesController.AllEnemiesDie += PlayerWin;
            
            for (var i = 0; i < _enemyCount; i++)
            {
                var spawnPoint = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                _enemiesController.SpawnEnemies(spawnPoint, _enemyName);
            }
            
            _droppedItemController.SetDroppedItem(_firstDroppedItemPos.position ,_firstDroppedItemName);
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
            SaveData();
            
            _viewMediator.OpenView(ViewType.EndPanel);
        }
        
        private void SaveData()
        {
            var gameData = new GameData();
            gameData.SetData(_inventoryController.InventoryItems);
            
            SaveSystem.SaveData(gameData);
            LoggerService.Log($"{gameData.InventoryItemDatas.Count} inventory items saved.");
        }

        private void LoadData()
        {
            var loadedData = SaveSystem.LoadData();
            
            if (loadedData != null)
            {
                LoggerService.Log($"{loadedData.InventoryItemDatas.Count} inventory items loaded.");
                
                var loadedItems = loadedData.GetInventoryItems();
                foreach (var item in loadedItems)
                {
                    _inventoryController.AddItemToInventory(item);
                }
            }
        }

        private void OnEnable()
        {
            _gameEvent.AddSub(GameEventType.GameStart, GameStart);
            _gameEvent.AddSub(GameEventType.GameOver, GameEnd);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveSub(GameEventType.GameStart, GameStart);
            _gameEvent.RemoveSub(GameEventType.GameOver, GameEnd);
        }
    }
}
