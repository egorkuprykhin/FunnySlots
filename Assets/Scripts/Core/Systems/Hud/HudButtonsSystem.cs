using FSM;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using States;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudButtonsSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button _playButton;
        [EcsUguiNamed("BackButton")] Button _backButton;

        private EcsCustomInject<StateMachine> _stateMachine;

        private EcsWorldInject _world;
        private SharedData _sharedData;

        public void Init(IEcsSystems systems)
        {
            _sharedData = systems.GetShared<SharedData>();
            
            _backButton.onClick.AddListener(BackToMenu);
            _playButton.onClick.AddListener(TryStartRollCards);
        }

        private void BackToMenu()
        {
            _stateMachine.Value.Enter<MenuState>();
        }

        private void TryStartRollCards()
        {
            if (_sharedData.RollingState)
                return;
            
            int newEntity = _world.NewEntity();
            newEntity.Set<StartRollEvent>(_world);
            
            _sharedData.SetRollingState();
        }
    }
}