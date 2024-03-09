using System;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.GameObjectActions
{
    public class GameObjectSetActiveAction : GameObjectActionBase
    {
        [SerializeField] private bool _isActiveValue;

        private void OnValidate()
        {
            if (GameObject == null)
                Debug.LogWarning($"Game object in action ({gameObject.name}) is missing");
        }
        
        public override void Execute(Action onComplete = null)
        {
            GameObject.SetActive(_isActiveValue);
            onComplete?.Invoke();
        }
    }
}