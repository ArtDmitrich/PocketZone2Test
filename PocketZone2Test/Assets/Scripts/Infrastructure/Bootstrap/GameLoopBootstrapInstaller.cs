using GameLogic.Characters;
using GameLogic.EnemiesController;
using Services.GameEvents;
using Services.Input;
using Services.ObjectPool.ZenjectVariant;
using Services.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameLoopBootstrapInstaller : MonoInstaller
    {
        [FormerlySerializedAs("viewMediatorUI")] [FormerlySerializedAs("_mediatorUI")] [SerializeField] private ViewMediatorUI viewViewMediatorUI;
        [SerializeField] private EnemiesController _enemiesController;
    
        public override void InstallBindings()
        {
            BindMediatorUI();
            BindInputService();
            BindEnemiesController();
            BindGameEvents();
        }
    
        private void BindMediatorUI()
        {
            Container.Bind<IViewMediatorUI>().To<ViewMediatorUI>().FromInstance(viewViewMediatorUI).AsSingle();
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
    }
}