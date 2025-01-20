using System;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService
    {
        event Action<Vector2> PlayerMoveStarted;
        event Action PlayerMoveStoped;

        void SetEnableToCharacterInput(bool enable);
    }
}
