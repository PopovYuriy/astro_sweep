using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tools.CSharp;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.Common
{
    public class ParallelGameActions : GameActionAbstract
    {
        [SerializeField] private GameActionAbstract[] _actions;

        public override void Execute(Action onComplete)
        {
            ExecuteAsync().Run(onComplete);
        }

        private async Task ExecuteAsync()
        {
            var actionIndex = 0;
            var completedActionsCount = 0;

            while (actionIndex < _actions.Length)
                _actions[actionIndex++].Execute(OnActionComplete);
            
            await UniTask.WaitUntil(() => completedActionsCount >= _actions.Length);
            
            void OnActionComplete()
            {
                completedActionsCount++;
            }
        }
    }
}