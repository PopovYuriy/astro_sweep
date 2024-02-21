using System.Threading;
using UnityEngine;

namespace Tools.CSharp
{
    public static class ApplicationQuitCancellationToken
    {
        public static CancellationToken Token { get; }

        static ApplicationQuitCancellationToken()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Token = cancellationTokenSource.Token;
            Application.quitting += OnApplicationQuit;

            void OnApplicationQuit()
            {
                Application.quitting -= OnApplicationQuit;
                cancellationTokenSource.Cancel();
            }
        }
    }
}