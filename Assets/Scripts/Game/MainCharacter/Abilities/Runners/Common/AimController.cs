using Game.CameraController.StatesMachine;
using Game.CameraController.StatesMachine.Enums;
using Game.MainCharacter.Input;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities.Runners.Common
{
    public sealed class AimController : MonoBehaviour
    {
        [SerializeField] private CharacterCameraStateMachine _cameraStateMachine;
        [SerializeField] private Transform _aimHorizontalFollowTransform;
        [SerializeField] private Transform _aimFollowTransform;
        [SerializeField] private float _aimRotationSpeed;
        [SerializeField] private float _horizontalAngleMin;
        [SerializeField] private float _horizontalAngleMax;
        [SerializeField] private float _verticalAngleMin;
        [SerializeField] private float _verticalAngleMax;
        
        private PlayerInputController _inputController;
        private Transform _originLookAtTransform;

        public bool IsAiming { get; private set; }
        public Vector3 AimDirection => _aimFollowTransform.forward;

        [Inject]
        private void Construct(PlayerInputController inputController)
        {
            _inputController = inputController;
        }

        public void Activate()
        {
            _inputController.OnAimInput += AimInputHandler;
            _inputController.OnCameraRotationInput += CameraRotationHandler;
        }

        public void Deactivate()
        {
            _inputController.OnAimInput -= AimInputHandler;
            _inputController.OnCameraRotationInput -= CameraRotationHandler;
            
            _cameraStateMachine.SetState(CharacterCameraControllerState.ThirdPerson);

            Cursor.visible = true;
        }
        
        private void AimInputHandler(bool performed)
        {
            IsAiming = performed;
            Cursor.visible = !performed;
            
            _aimHorizontalFollowTransform.forward = transform.forward;
            _aimFollowTransform.forward = transform.forward;
            
            if (performed)
                _cameraStateMachine.SetState(CharacterCameraControllerState.Aim);
            else
                _cameraStateMachine.SetState(CharacterCameraControllerState.ThirdPerson);
        }

        private void CameraRotationHandler(Vector2 delta)
        {
            if (!IsAiming)
                return;

            var previousHorizontalRotation = _aimHorizontalFollowTransform.rotation;
            _aimHorizontalFollowTransform.rotation *= Quaternion.AngleAxis(delta.x * _aimRotationSpeed, Vector3.up);
            
            var forwardHRotation = new Vector2(transform.forward.x, transform.forward.z);
            var aimHRotation = new Vector2(_aimHorizontalFollowTransform.forward.x, _aimHorizontalFollowTransform.forward.z);
            var hAngle = Vector2.SignedAngle(forwardHRotation, aimHRotation);
            if (hAngle <= _horizontalAngleMin || hAngle >= _horizontalAngleMax)
                _aimHorizontalFollowTransform.rotation = previousHorizontalRotation;
            
            var previousVerticalRotation = _aimFollowTransform.rotation;
            _aimFollowTransform.rotation *= Quaternion.AngleAxis(-delta.y * _aimRotationSpeed, Vector3.right);
            
            var forwardVRotation = new Vector2(transform.forward.y, transform.forward.z);
            var aimVRotation = new Vector2(_aimFollowTransform.forward.y, _aimFollowTransform.forward.z);
            var vAngle = Vector2.SignedAngle(forwardVRotation, aimVRotation);
            if (vAngle <= _verticalAngleMin || vAngle >= _verticalAngleMax)
                _aimFollowTransform.rotation = previousVerticalRotation;
        }
    }
}