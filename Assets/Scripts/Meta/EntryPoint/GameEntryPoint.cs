using FSM;
using States;
using VContainer;
using VContainer.Unity;

namespace EntryPoint
{
    public class GameEntryPoint : IInitializable
    {
        [Inject] private readonly StateMachine _stateMachine;
        
        public void Initialize()
        {
            _stateMachine.Enter<LoadingState>();
        }
    }
}