using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Tools.CSharp
{
    public static class TaskExtension
    {
        public static void Run<T>(this Task<T> task, Action<T> onComplete = null, bool useCurrentTaskScheduler = true,
            CancellationTokenSource cancellationTokenSource = null)
        {
            var cancellationToken = cancellationTokenSource?.Token ?? ApplicationQuitCancellationToken.Token;
            
            if (useCurrentTaskScheduler)
                task.ContinueWith(TaskCompleted, null, cancellationToken, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            else
                task.ContinueWith(TaskCompleted, null, cancellationToken);

            void TaskCompleted(Task<T> result, object token)
            {
                if (result.IsFaulted && result.Exception != null)
                {
                    Debug.LogError(result.Exception);
                    return;
                }

                try
                {
                    onComplete?.Invoke(result.Result);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }

        public static void Run(this Task task, Action onComplete = null, bool useCurrentTaskScheduler = true, CancellationTokenSource cancellationTokenSource = null)
        {
            var cancellationToken = cancellationTokenSource?.Token ?? ApplicationQuitCancellationToken.Token;
            
            if (useCurrentTaskScheduler)
                task.ContinueWith(TaskCompleted, null, cancellationToken, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            else
                task.ContinueWith(TaskCompleted, null, cancellationToken);

            void TaskCompleted(Task result, object token)
            {
                if (result.IsFaulted && result.Exception != null)
                {
                    Debug.LogError(result.Exception);
                    return;
                }

                try
                {
                    onComplete?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
        }
    }
}