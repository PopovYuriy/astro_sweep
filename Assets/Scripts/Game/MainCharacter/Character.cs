using DG.Tweening;
using UnityEngine;

namespace Game.MainCharacter
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class Character : MonoBehaviour
    {
        private const float Gravity = -9.81f;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _speedTweeningDuration;
        
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _rotationTweeningDuration;

        private CharacterController _characterController;
        private float _moveDirection;
        private float _rotationDirection;
        private float _velocity;
        
        private float _currentSpeed;
        private float _currentRotationSpeed;
        
        private Tween _speedTween;
        private Tween _rotationSpeedTween;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void OnDestroy()
        {
            KillSpeedTween();
            KillRotationSpeedTween();
        }

        private void FixedUpdate()
        {
            if (_moveDirection != 0f)
            {
                if (_currentSpeed <= float.Epsilon)
                {
                    KillSpeedTween();
                    _speedTween = DOTween.To(() => _currentSpeed, 
                        speed => _currentSpeed = speed, 
                        _speed, _speedTweeningDuration);
                }
                Move(_moveDirection);
            }
            else
            {
                _currentSpeed = 0f;
            }
            
            if (_rotationDirection != 0f)
            {
                if (_currentRotationSpeed <= float.Epsilon)
                {
                    KillRotationSpeedTween();
                    _rotationSpeedTween = DOTween.To(() => _currentRotationSpeed, 
                        speed => _currentRotationSpeed = speed,
                        _rotationSpeed, 
                        _rotationTweeningDuration);
                }
                Rotate(_rotationDirection);
            }
            else
            {
                _currentRotationSpeed = 0f;
            }

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

        private void Move(float direction)
        {
            _characterController.Move(transform.forward * direction * _currentSpeed * Time.fixedDeltaTime);
        }

        private void Rotate(float value)
        {
            transform.Rotate(new Vector3(0f, value * _currentRotationSpeed * Time.fixedDeltaTime, 0f), Space.Self);
        }

        private void DoGravity()
        {
            _velocity += Gravity * Time.fixedDeltaTime;
            
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