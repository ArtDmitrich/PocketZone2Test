using System;

namespace Services.GameEvents
{
    public interface IGameEvent
    {
        void AddSub(GameEventType gameEvent, Action action);
        void RemoveSub(GameEventType gameEvent, Action action);
        void InvokeEvent(GameEventType gameEvent);
    }
}
