using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tools.CSharp;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.Common
{
    public class DelayGameAction : GameActionAbstract
    {
        [SerializeField] private float _delayInSeconds;
        
        public override void Execute(Action onComplete)
        {
            ExecuteAsync().Run(onComplete);
        }

        private async Task ExecuteAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_delayInSeconds));
        }
    }
}