using GameLogic.Characters;
using GameLogic.EnemiesController;
using Services.Input;
using Services.ObjectPool.ZenjectVariant;
using Services.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameLoopBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private MediatorUI _mediatorUI;
        [SerializeField] private EnemiesController _enemiesController;

        [SerializeField] private GameObject _enemyPrefab;
    
        public override void InstallBindings()
        {
            BindMediatorUI();
            BindInputService();
            BindEnemiesFactory();
            BindEnemiesPool();
            BindEnemiesController();
        }
    
        private void BindMediatorUI()
        {
            Container.Bind<IMediatorUI>().To<MediatorUI>().FromInstance(_mediatorUI).AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }

        private void BindEnemiesFactory()
        {
            Container.BindFactory<MeleeEnemyCharacter, MeleeEnemyCharacter.Factory>()
                .FromComponentInNewPrefab(_enemyPrefab);
        }

        private void BindEnemiesPool()
        {
            Container.BindMemoryPool<MeleeEnemyCharacter, MeleeEnemyCharacter.Pool>()
                .FromFactory<MeleeEnemyCharacter.Factory>();
        }
        
        private void BindEnemiesController()
        {
            Container.Bind<EnemiesController>().FromInstance(_enemiesController).AsSingle();
        }
    }
}