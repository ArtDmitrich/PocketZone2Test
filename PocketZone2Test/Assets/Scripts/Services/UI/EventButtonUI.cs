using System;
using Services.GameEvents;
using UnityEngine;
using UnityEngine.UI;

public class EventButtonUI : MonoBehaviour
{
    public event Action<GameEventType> OnClick;
    
    [SerializeField] private GameEventType _gameEventType;
    
    private Button Button { get { return _button ??= GetComponent<Button>(); } }
    private Button _button;

    private void Click()
    {
        OnClick?.Invoke(_gameEventType);
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(Click);
    }
}
