using Characters;
using Services.Initialize;
using Services.Input;
using Services.Logger;
using Services.UI;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerCharacter playerCharacter;
    
    private IMediatorUI _mediator;
    private IInputService _inputService;
    
    [Inject]
    private void Construct(IMediatorUI mediator, IInputService inputService)
    {
        _mediator = mediator;
        _inputService = inputService;

        if (_inputService is IInitialazable input)
        {
            input.Init();
        }
        
        playerCharacter.Initialization(_inputService);
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
        playerCharacter.Attack();
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
