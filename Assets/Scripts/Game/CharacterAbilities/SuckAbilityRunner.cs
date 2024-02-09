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
            rigidbody.AddForce(_characterTransform.forward * _spitingStrength, ForceMode.Acceleration);
        }
    }
}