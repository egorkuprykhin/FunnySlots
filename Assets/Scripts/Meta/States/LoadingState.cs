using FSM;
using Scripts.Configuration;
using VContainer;

namespace States
{
    public class LoadingState : IState
    {
        [Inject] private readonly StateMachine _stateMachine;
        [Inject] private readonly GlobalConfiguration _globalConfiguration;
        [Inject] private readonly ColorPrepareService _colorPrepareService;

        public void Enter()
        {
            _colorPrepareService.PrepareColors();
            
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
        }
    }
}