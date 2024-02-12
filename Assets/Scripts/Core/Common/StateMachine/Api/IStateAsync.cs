using System.Threading.Tasks;

namespace Core.Common.StateMachine.Api
{
    public interface IStateAsync
    {
        void ResetState();
        Task Enter();
        Task Exit();
    }
}