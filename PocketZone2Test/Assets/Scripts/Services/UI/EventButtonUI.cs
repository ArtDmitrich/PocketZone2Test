using System;
using Services.GameEvents;
using UnityEngine;
using UnityEngine.UI;

public class EventButtonUI : MonoBehaviour
{
    public event Action<GameEventType> OnClick;
    
    [SerializeField] private GameEventType _gameEventType;
    [SerializeField] private Button _button;

    private void Click()
    {
        OnClick?.Invoke(_gameEventType);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }
}
