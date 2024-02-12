namespace Core.Common.StateMachine.Api
{
    public interface ISimpleStateMachineAsync<T>
    {
        void RegisterState(T stateKey, IStateAsync state);
        void SetState(T stateKey);
    }
}