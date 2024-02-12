using System;
using UnityEngine;

namespace Game.CharacterAbilities
{
    public sealed class SuckAbilityRunner : MonoBehaviour
    {
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _suckingStrength;
        [SerializeField] private float _minDistanceToPickUp;
        [SerializeField] private float _spitingStrength;
        [SerializeField] private float _maxSpitDistance;
        

        private bool _enabled;
        private Rigidbody _suckedObject;
        private Rigidbody _objectToSuck;

        private void Start()
        {
            Stop();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.E))
            {
                _enabled = !_enabled;
                if (_enabled)
                    Run();
                else
                    Stop();
            }

            if (UnityEngine.Input.GetKey(KeyCode.Space) && _suckedObject != null)
                Spit(_suckedObject);

            if (_objectToSuck != null)
                Suck(_objectToSuck);
        }

        public void Run()
        {
            _particleSystem.Play();
            _collider.enabled = true;
        }

        public void Stop()
        {
            _particleSystem.Stop();
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            _objectToSuck = other.GetComponent<Rigidbody>();
        }
        
        private void OnTriggerExit(Collider other)
        {
            var rigidBody = other.GetComponent<Rigidbody>();
            if (rigidBody == _objectToSuck)
                _objectToSuck = null;
        }

        private void Suck(Rigidbody rigidbody)
        {
            var forceDirection = (transform.position - rigidbody.transform.position).normalized;
            var distance = Vector3.Distance(transform.position, rigidbody.transform.position);

            if (distance < _minDistanceToPickUp)
            {
                _objectToSuck.gameObject.SetActive(false);
                _suckedObject = _objectToSuck;
                _objectToSuck = null;
                Stop();
            }
            else
            {
                var force = forceDirection * (_suckingStrength / distance);
                _objectToSuck.AddForce(force, ForceMode.Acceleration);   
            }
        }

        private void Spit(Rigidbody rigidbody)
        {
            rigidbody.transform.position = transform.position + _characterTransform.forward;
            rigidbody.gameObject.SetActive(true);
            var targetPosition = CalculateTargetPosition();
            var force = CalculateForce(targetPosition);
            rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private Vector3 CalculateTargetPosition()
        {
            var screenCenter = new Vector3(Screen.width >> 1, Screen.height >> 1, 0);
            var ray = Camera.main.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(ray, out var hitInfo, _maxSpitDistance))
            {
                return hitInfo.point;
            }

            return transform.position + transform.forward * _maxSpitDistance;
        }
        
        private Vector3 CalculateForce(Vector3 targetPosition)
        {
            // Розрахунок необхідних параметрів для досягнення цільової позиції

            Vector3 initialPosition = transform.position;
            Vector3 displacement = targetPosition - initialPosition;
            var gravity = Math.Abs(Physics.gravity.y);
            // Вирахування часу, який потрібен об'єкту, щоб долетіти до цільової позиції
            // Використовуємо формулу для рівномірного прискореного руху: d = v0*t + (1/2)*a*t^2
            // Де d - відстань, яку потрібно подолати, a - прискорення (гравітація), t - час, v0 - початкова швидкість
            float time = Mathf.Sqrt(2 * (displacement.magnitude / gravity));
            float verticalVelocity = gravity * time;

            // Розрахунок початкової швидкості по горизонталі
            Vector3 horizontalVelocity = displacement / time;

            // Застосування сили до Rigidbody
            Vector3 force = horizontalVelocity;
            force.y = verticalVelocity;
            return force;
        }
    }
}