using System;
using Core.GameSystems.AbilitySystem.Enums;
using Game.MainCharacter.Abilities;
using Game.MainCharacter.StatesMachine;
using Game.MainCharacter.StatesMachine.Enums;
using UnityEngine;

namespace Game.MainCharacter
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class Character : MonoBehaviour
    {
        private const float Gravity = -9.81f;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private MainCharacterStateMachine _stateMachine;
        [SerializeField] private AbilityRunController _abilityRunController;

        private CharacterController _characterController;
        private float _moveDirection;
        private float _rotationDirection;
        private float _velocity;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            // _abilityRunController.OnAbilityRan += AbilityRanHandler;
            // _abilityRunController.OnAbilitiesStopped += AbilitiesStoppedHandler;
        }

        private void OnDestroy()
        {
            // _abilityRunController.OnAbilityRan -= AbilityRanHandler;
            // _abilityRunController.OnAbilitiesStopped -= AbilitiesStoppedHandler;
        }

        private void FixedUpdate()
        {
            if (_moveDirection != 0f)
                Move(_moveDirection);
            
            if (_rotationDirection != 0f)
                Rotate(_rotationDirection);
            
            DoGravity();
        }

        public void SetMoveDirection(float direction)
        {
            _moveDirection = direction;
        }
        
        public void SetRotationDirection(float direction)
        {
            _rotationDirection = direction;
        }

        private void AbilityRanHandler(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.Vacuuming:
                    _stateMachine.SetState(MainCharacterState.Vacuuming);
                    break;
                case AbilityType.Throwing:
                    _stateMachine.SetState(MainCharacterState.Throwing);
                    break;
            }
        }

        private void AbilitiesStoppedHandler()
        {
            _stateMachine.SetState(MainCharacterState.Idle);
        }

        private void Move(float direction)
        {
            _characterController.Move(transform.forward * direction * _speed * Time.fixedDeltaTime);
        }

        private void Rotate(float value)
        {
            transform.Rotate(new Vector3(0f, value * _rotationSpeed * Time.fixedDeltaTime, 0f), Space.Self);
        }

        private void DoGravity()
        {
            _velocity += Gravity * Time.fixedDeltaTime;
            
            _characterController.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
        }
    }
}