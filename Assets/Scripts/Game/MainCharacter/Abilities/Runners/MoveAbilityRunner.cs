using Core.GameSystems.AbilitySystem.Model;
using DG.Tweening;
using UnityEngine;

namespace Game.MainCharacter.Abilities.Runners
{
    public sealed class MoveAbilityRunner : AbilityRunnerAbstract<MovingAbilityModel>
    {
        [SerializeField] private float _moveAccelerationDuration;
        [SerializeField] private float _rotationAccelerationDuration;

        [SerializeField] private CharacterController _characterController;
        
        private bool _activated;
        
        private float _velocity;
        
        private float _currentSpeed;
        private float _currentRotationSpeed;
        
        private Tween _speedTween;
        private Tween _rotationSpeedTween;

        private void OnDestroy()
        {
            KillSpeedTween();
            KillRotationSpeedTween();
        }

        private void FixedUpdate()
        {
            if (!_activated)
                return;
            
            if (Model.MoveDirection.y != 0f)
            {
                if (_currentSpeed < Model.MoveSpeed)
                {
                    KillSpeedTween();
                    _speedTween = DOTween.To(() => _currentSpeed, 
                        speed => _currentSpeed = speed, 
                        Model.MoveSpeed, 
                        _moveAccelerationDuration * (1 - _currentSpeed / Model.MoveSpeed));
                }
                
                Move(Model.MoveDirection.y);
            }
            else
            {
                _currentSpeed = 0f;
            }
            
            if (Model.MoveDirection.x != 0f)
            {
                if (_currentRotationSpeed < Model.RotationSpeed)
                {
                    KillRotationSpeedTween();
                    _rotationSpeedTween = DOTween.To(() => _currentRotationSpeed, 
                        speed => _currentRotationSpeed = speed,
                        Model.RotationSpeed, 
                        _rotationAccelerationDuration * (1 - _currentRotationSpeed / Model.RotationSpeed));
                }
                
                Rotate(Model.MoveDirection.x);
            }
            else
            {
                _currentRotationSpeed = 0f;
            }

            DoGravity();
        }
        
        protected override void RunInternal()
        {
            _activated = true;
            DispatchActed();
        }

        protected override void StopInternal()
        {
            _activated = false;
        }

        private void Move(float direction)
        {
            _characterController.Move(transform.forward * direction * _currentSpeed * Time.fixedDeltaTime);
        }

        private void Rotate(float value)
        {
            _characterController.transform.transform.Rotate(new Vector3(0f, value * _currentRotationSpeed * Time.fixedDeltaTime, 0f), Space.Self);
        }

        private void DoGravity()
        {
            _velocity += Physics.gravity.y * Time.fixedDeltaTime;
            
            _characterController.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
        }

        private void KillSpeedTween()
        {
            if (_speedTween == null)
                return;
            
            _speedTween.Kill();
            _speedTween = null;
        }
        
        private void KillRotationSpeedTween()
        {
            if (_rotationSpeedTween == null)
                return;
            
            _rotationSpeedTween.Kill();
            _rotationSpeedTween = null;
        }
    }
}