using Game.Environment.InteractiveItems.GameActions;
using UnityEngine;

namespace Game.Environment.InteractiveItems.ActionTriggers
{
    public abstract class ActionTriggerAbstract : MonoBehaviour
    {
        [field: SerializeField] protected GameActionAbstract Action { get; private set; }
    }
}