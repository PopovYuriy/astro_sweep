using Game.Environment.InteractiveItems;
using UnityEngine;

namespace Game.MainCharacter.Abilities.Detectors
{
    [RequireComponent(typeof(Collider))]
    public sealed class AbilityRunColliderDetector : MonoBehaviour
    {
        [SerializeField] private AbilityRunController _abilitiesController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<AbilityPlaceholder>(out var abilityRunner))
                _abilitiesController.ProcessAbilityRunner(abilityRunner.AbilityType);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<AbilityPlaceholder>(out var abilityRunner))
                _abilitiesController.ProcessAbilityRunner(abilityRunner.AbilityType);
        }
    }
}