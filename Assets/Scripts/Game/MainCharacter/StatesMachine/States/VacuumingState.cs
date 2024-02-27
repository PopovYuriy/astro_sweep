using System.Threading.Tasks;
using UnityEngine;

namespace Game.MainCharacter.StatesMachine.States
{
    public class VacuumingState : MainCharacterStateAbstract
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public override void ResetState()
        {
            _particleSystem.gameObject.SetActive(false);
        }

        public override async Task Enter()
        {
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }

        public override async Task Exit()
        {
            _particleSystem.Stop();
            _particleSystem.gameObject.SetActive(false);
        }
    }
}