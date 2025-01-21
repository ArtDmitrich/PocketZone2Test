using System;
using System.Collections.Generic;

namespace Services.GameEvents
{
    public class GameEvents: IGameEvent
    {
        private Dictionary<GameEventType, Action> _events = new Dictionary<GameEventType, Action>();

        public void AddSub(GameEventType gameEvent, Action action)
        {
            if (_events.ContainsKey(gameEvent))
                _events[gameEvent] += action;
            else
                _events[gameEvent] = action;
        }

        public void InvokeEvent(GameEventType gameEvent)
        {
            if (_events.TryGetValue(gameEvent, out _))
                _events[gameEvent]?.Invoke();
        }

        public void RemoveSub(GameEventType gameEvent, Action action)
        {
            if (_events.TryGetValue(gameEvent, out _))
                _events[gameEvent] -= action;
        }
    }
    
    public enum GameEventType
    {
        GameStart,
        GameOver,
        PlayerShoot,
        PlayerRespawn,
        ChangeWeapon,
        OpenInventory,
        CloseInventory
    }
}
