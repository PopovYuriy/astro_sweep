using UnityEngine;

namespace Game.Input
{
    public sealed class CharacterInputController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private GameInput _gameInput;

        private void Awake()
        {
            _gameInput = new GameInput();
            _gameInput.Enable();
        }

        private void FixedUpdate()
        {
            var moveDirection = _gameInput.Gameplay.Movement.ReadValue<Vector2>();
            _character.SetMoveDirection(moveDirection.y);
            _character.SetRotationDirection(moveDirection.x);
        }
    }
}