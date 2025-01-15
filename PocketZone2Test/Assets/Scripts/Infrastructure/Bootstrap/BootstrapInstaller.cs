using Services.Input;
using Services.Logger;
using Services.UI;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
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