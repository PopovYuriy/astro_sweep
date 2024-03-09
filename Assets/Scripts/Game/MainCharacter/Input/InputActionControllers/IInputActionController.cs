using System;

namespace Game.MainCharacter.Input.InputActionControllers
{
    public interface IInputActionController<T> : IDisposable
    {
        event Action<T> OnPerformed;
    }
}