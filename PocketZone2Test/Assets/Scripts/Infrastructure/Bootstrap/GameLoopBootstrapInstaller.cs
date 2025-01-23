using GameLogic.Characters;
using GameLogic.DroppedItem;
using GameLogic.EnemiesController;
using GameLogic.ItemDispatcher;
using GameLogic.Weapons;
using Services.GameEvents;
using Services.Input;
using Services.Inventory;
using Services.ObjectPool.ZenjectVariant;
using Services.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameLoopBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private ViewMediatorUI _viewMediatorUI;
        [SerializeField] private EnemiesController _enemiesController;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryController _inventoryController;
        [SerializeField] private DroppedItemController _droppedItemController;
        [SerializeField] private AmmunitionController _ammunitionController;
        [SerializeField] private WeaponsCotroller _weaponsCotroller;
    
        public override void InstallBindings()
        {
            BindMediatorUI();
            BindInputService();
            BindEnemiesController();
            BindGameEvents();
            BindInventoryView();
            BindInventoryController();
            BindDroppedItemController();
            BindAmmunitionController();
            BindWeaponsController();
        }
    
        private void BindMediatorUI()
        {
            Container.Bind<IViewMediatorUI>().To<ViewMediatorUI>().FromInstance(_viewMediatorUI).AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
        
        private void BindEnemiesController()
        {
            Container.Bind<EnemiesController>().FromInstance(_enemiesController).AsSingle();
        }
        
        private void BindGameEvents()
        {
            Container.BindInterfacesAndSelfTo<GameEvents>().AsSingle();
        }
        
        private void BindInventoryView()
        {
            Container.Bind<InventoryView>().FromInstance(_inventoryView).AsSingle();
        }
        
        private void BindInventoryController()
        {
            Container.Bind<InventoryController>().FromInstance(_inventoryController).AsSingle();
        }
        
        private void BindDroppedItemController()
        {
            Container.Bind<DroppedItemController>().FromInstance(_droppedItemController).AsSingle();
        }
        
        private void BindAmmunitionController()
        {
            Container.Bind<AmmunitionController>().FromInstance(_ammunitionController).AsSingle();
        }
        
        private void BindWeaponsController()
        {
            Container.Bind<WeaponsCotroller>().FromInstance(_weaponsCotroller).AsSingle();
        }
    }
}