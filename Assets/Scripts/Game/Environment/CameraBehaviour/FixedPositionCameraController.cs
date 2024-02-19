using UnityEngine;

namespace Game.Environment.CameraBehaviour
{
    public class FixedPositionCameraController : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField][Range(0, 90)] private float _startXRotation;
        [SerializeField][Range(0, 90)] private float _minXRotation;
        [SerializeField][Range(0, 90)] private float _maxXRotation = 90;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Activate()
        {
            var cameraPosition = transform.position - transform.forward * _distance;
            _camera.transform.position = cameraPosition;
            _camera.transform.forward = transform.forward;

            SetXRotation(_startXRotation);
        }

        public void RotateX(float angle)
        {
            angle = Mathf.Clamp(angle, _minXRotation, _maxXRotation);
            SetXRotation(angle);
        }

        private void SetXRotation(float angle)
        {
            var cameraRotation = _camera.transform.localRotation.eulerAngles;
            cameraRotation.x = angle;
            _camera.transform.localRotation = Quaternion.Euler(cameraRotation);
        }

        private void OnValidate()
        {
            if (_minXRotation > _maxXRotation)
                _minXRotation = _maxXRotation;

            if (_maxXRotation < _minXRotation)
                _maxXRotation = _minXRotation;

            if (_startXRotation < _minXRotation)
                _startXRotation = _minXRotation;

            if (_startXRotation > _maxXRotation)
                _startXRotation = _maxXRotation;
        }
    }
}