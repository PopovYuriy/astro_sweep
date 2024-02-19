using Core.GameSystems.InventorySystem;
using Core.GameSystems.InventorySystem.Data;
using Core.GameSystems.InventorySystem.Enums;
using Core.GameSystems.InventorySystem.Model;
using UnityEngine;
using Zenject;

namespace Game.MainCharacter.Abilities
{
    public sealed class ThrowAbilityRunner : AbilityRunnerAbstract
    {
        [SerializeField] private Transform _trajectionStartPosition;

        [SerializeField] private Transform _aimTransform;
        
        [Header("Throwing settings")]
        [SerializeField] private float _throwMaxStrength;
        [SerializeField] [Range(0, 90)] private float _minAngle;
        [SerializeField] [Range(0, 90)] private float _maxAngle;
        
        // [Header("Trajectory line settings")]
        // [SerializeField] private LineRenderer _lineRenderer;
        // [SerializeField] [Range(10, 100)] private int _trajectoryLinePoints = 25;
        // [SerializeField] [Range(0.01f, 0.25f)] private float _timeBetweenPoints = 0.1f;

        private InventoryContainerModel _inventoryContainer;
        private ItemData _currentObjectData;
        private Rigidbody _objectsRigidbodyToThrow;
        // private LayerMask _objectCollisionMask;

        private GameInput _gameInput;

        private Vector3 _currentDirection;
        private Vector3 _currentVelocity;

        private bool _activated;

        [Inject]
        private void Construct(CharacterInventorySystem inventorySystem)
        {
            _inventoryContainer = inventorySystem.GetInventoryContainer(ItemCollectionType.Throwable);
        }

        private void OnDrawGizmos()
        {
            if (!_activated)
                return;
            
            Gizmos.DrawCube(_aimTransform.position, new Vector3(0.1f, 0.1f, 0.1f));
        }

        protected override void RunInternal()
        {
            _currentObjectData = _inventoryContainer.GetItem(0);
            _objectsRigidbodyToThrow = _currentObjectData.Prefab.GetComponent<Rigidbody>();
            
            // var objectLayer = _objectsRigidbodyToThrow.gameObject.layer;
            // for (var i = 0; i < 32; i++)
            // {
            //     if (!Physics.GetIgnoreLayerCollision(objectLayer, i))
            //         _objectCollisionMask |= 1 << i;
            // }

            _aimTransform.position = transform.position + transform.forward;
            
            // _lineRenderer.enabled = true;

            _gameInput = new GameInput();
            _gameInput.Enable();
            
            _activated = true;
            
            Debug.Log("Throwing started");
        }

        protected override void StopInternal()
        {
            _currentObjectData = null;
            _objectsRigidbodyToThrow = null;
            
            // _lineRenderer.enabled = false;
            _activated = false;
            
            Debug.Log("Throwing stopped");
        }

        private void FixedUpdate()
        {
            if (!_activated)
                return;

            _currentDirection = DetermineDirection(_minAngle);
            Debug.DrawLine(_trajectionStartPosition.position, _trajectionStartPosition.position + _currentDirection * 10);
            if (UnityEngine.Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Throw");
                Throw();
            }
        }

        private Vector3 DetermineDirection(float angle)
        {
            var sign = Mathf.Sign(transform.forward.z) > 0 ? -1 : 1; 
            return (Quaternion.Euler(sign * angle, 0, 0) *  transform.forward).normalized;
        }

        private void Throw()
        {
            var throwItem = Instantiate(_currentObjectData.Prefab, _trajectionStartPosition.position, Quaternion.identity);
            var rb = throwItem.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            var strength = _throwMaxStrength / _objectsRigidbodyToThrow.mass;
            var currentVelocity = _currentDirection * strength;
            rb.AddForce(currentVelocity, ForceMode.Impulse);
            
            _inventoryContainer.TryPullItem(_currentObjectData);
            Stop();
        }
        
        // private void DrawProjection(Vector3 startVelocity)
        // {
        //     _lineRenderer.positionCount = Mathf.CeilToInt(_trajectoryLinePoints / _timeBetweenPoints) + 1;
        //     var startPosition = _trajectionStartPosition.position;
        //
        //     var i = 0;
        //     _lineRenderer.SetPosition(i, startPosition);
        //     var velocity = _throwMaxStrength / _objectsRigidbodyToThrow.mass * Time.fixedDeltaTime;
        //     for (var time = 0f; time < _trajectoryLinePoints; time++)
        //     {
        //         i++;
        //         var point = startPosition + _currentDirection * velocity * time * _timeBetweenPoints;
        //         point.y += Physics.gravity.y / 2 * (float)Math.Pow(time * _timeBetweenPoints, 2);
        //
        //         _lineRenderer.SetPosition(i, point);
        //
        //         var lastPosition = _lineRenderer.GetPosition(i - 1);
        //
        //         if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,
        //             (point - lastPosition).magnitude, _objectCollisionMask))
        //         {
        //             _lineRenderer.SetPosition(i, hit.point);
        //             _lineRenderer.positionCount = i + 1;
        //             _aimTransform.position = hit.point;
        //             return;
        //         }
        //     }
        // }
    }
}