using System.Threading.Tasks;

namespace Core.Common.StateMachine.Api
{
    public interface ISimpleStateMachineAsync<T>
    {
        void RegisterState(T stateKey, IStateAsync state);
        Task SetState(T stateKey);
    }
}