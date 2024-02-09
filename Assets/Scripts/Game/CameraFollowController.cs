using UnityEngine;

namespace Game
{
    public sealed class CameraFollowController : MonoBehaviour
    {
        [SerializeField] private LayerMask _cameraObstaclesLayers;
        
        [SerializeField] private float _distance;
        [SerializeField] private float _smoothing;
        [SerializeField] private Vector3 _cameraDefaultOffset;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;
        
        private void FixedUpdate()
        {
            var completePositionBehindTarget = _targetTransform.position + _targetTransform.forward * -_distance;
            
            var newPositionBehindTarget = Vector3.Lerp(transform.position, completePositionBehindTarget, 
                _smoothing * Time.fixedDeltaTime);

            var predictedCameraPosition = newPositionBehindTarget + _cameraDefaultOffset;
            var ray = new Ray(predictedCameraPosition, _targetTransform.position - predictedCameraPosition);
            
            if (Physics.Raycast(ray, out var raycastInfo, _distance, _cameraObstaclesLayers))
                _cameraTransform.position = raycastInfo.point + ray.direction.normalized * 2;
            else
                _cameraTransform.localPosition = _cameraDefaultOffset;
            
            transform.position = newPositionBehindTarget;

            transform.LookAt(_targetTransform, Vector3.up);
        }

    }
}