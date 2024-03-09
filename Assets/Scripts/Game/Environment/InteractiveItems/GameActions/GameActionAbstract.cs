using System;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions
{
    public abstract class GameActionAbstract : MonoBehaviour
    {
        public abstract void Execute(Action onComplete = null);
    }
}