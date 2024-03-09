using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.GameObjectActions
{
    public abstract class GameObjectActionBase : GameActionAbstract
    {
        [field: SerializeField] protected GameObject GameObject { get; private set; }
    }
}