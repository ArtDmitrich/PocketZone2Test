using System;
using System.Collections.Generic;
using Services.GameEvents;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Services.UI
{
    public class ButtonListener : MonoBehaviour
    {
        [SerializeField] private List<EventButtonUI> _buttonsInScene  = new List<EventButtonUI>();
        
        private IGameEvent _gameEvent;

        [Inject]
        private void Construct(IGameEvent gameEvent)
        {
            _gameEvent = gameEvent;
        }
        
        private void CallEvent(GameEventType gameEvent)
        {
            _gameEvent.InvokeEvent(gameEvent);
        }

        private void OnEnable()
        {
            for (var i = 0; i < _buttonsInScene.Count; i++)
            {
                _buttonsInScene[i].OnClick += CallEvent;
            }
        }

        private void OnDisable()
        {
            for (var i = 0; i < _buttonsInScene.Count; i++)
            {
                _buttonsInScene[i].OnClick -= CallEvent;
            }
        }
    }
}
