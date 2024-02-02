using FSM;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using States;

namespace FunnySlots
{
    public class MetaMediatorSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<BackToMenuEvent>> _backToMenuEvent;

        private EcsCustomInject<StateMachine> _stateMachine;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int backToMenuEventEntity in _backToMenuEvent.Value)
            { 
                ExitToMenu();
                DeleteEvent(backToMenuEventEntity);
            }
        }

        private void ExitToMenu() => 
            _stateMachine.Value.Enter<MenuState>();

        private void DeleteEvent(int eventEntity) => 
            eventEntity.Del<BackToMenuEvent>();
    }
}