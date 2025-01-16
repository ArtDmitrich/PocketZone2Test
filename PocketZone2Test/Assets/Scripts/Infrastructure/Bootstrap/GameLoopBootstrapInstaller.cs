using Services.Input;
using Services.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class GameLoopBootstrapInstaller : MonoInstaller
    {
        [SerializeField] private MediatorUI _mediatorUI;
    
        public override void InstallBindings()
        {
            BindMediatorUI();
            BindInputService();
        }
    
        private void BindMediatorUI()
        {
            Container.Bind<IMediatorUI>().To<MediatorUI>().FromInstance(_mediatorUI).AsSingle();
        }

        private void BindInputService()
        {
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}