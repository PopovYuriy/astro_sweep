using UnityEngine;

namespace Game.MainCharacter.Abilities
{
    public sealed class ThrowAbilityRunner : AbilityRunnerAbstract
    {
        [SerializeField] private Rigidbody _objectPrefab;
        [SerializeField] [Range(1, 100)] private float _throwStrength = 10f;
        
        [Header("Trajectory line settings")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] [Range(10, 100)] private int _trajectoryLinePoints = 25;
        [SerializeField] [Range(0.01f, 0.25f)] private float _timeBetweenPoints = 0.1f;
        
        private LayerMask _objectCollisionMask;

        public override void Run()
        {
            
        }

        public override void Stop()
        {
            
        }
    }
}