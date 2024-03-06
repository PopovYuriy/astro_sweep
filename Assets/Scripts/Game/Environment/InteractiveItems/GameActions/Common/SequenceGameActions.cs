using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Tools.CSharp;
using UnityEngine;

namespace Game.Environment.InteractiveItems.GameActions.Common
{
    public class SequenceGameActions : GameActionAbstract
    {
        [SerializeField] private GameActionAbstract[] _actionsSequence;
        
        public override void Execute(Action onComplete = null)
        {
            ExecuteAsync(onComplete).Run();
        }

        private async Task ExecuteAsync(Action onComplete = null)
        {
            var actionIndex = 0;

            while (actionIndex < _actionsSequence.Length)
                await ExecuteActionAsync(_actionsSequence[actionIndex++]);
            
            onComplete?.Invoke();
        }

        private async Task ExecuteActionAsync(GameActionAbstract action)
        {
            var completed = false;
            
            action.Execute(onActionComplete);

            await UniTask.WaitUntil(() => completed);

            void onActionComplete()
            {
                completed = true;
            }
        }
    }
}