using Game.Environment.InteractiveItems.Enums;
using UnityEngine;

namespace Game.Environment.InteractiveItems.ActionTriggers
{
    [RequireComponent(typeof(Collider))]
    public class ColliderActionTrigger : ActionTriggerAbstract
    {
        [SerializeField] private TriggerItemId _expectedItemId;

        protected void OnTriggerEnter(Collider other)
        {
            var triggerObject = other.GetComponent<TriggerObject>();
            if (triggerObject == null || triggerObject.Id != _expectedItemId)
                return;
            
            Action.Execute();
        }
    }
}