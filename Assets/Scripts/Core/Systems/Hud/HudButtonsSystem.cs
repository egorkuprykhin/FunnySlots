using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudButtonsSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button _playButton;
        [EcsUguiNamed("BackButton")] Button _backButton;
        
        private EcsFilterInject<Inc<RollingState>> _rollingState;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            _backButton.onClick.AddListener(OnBackToMenuClicked);
            _playButton.onClick.AddListener(OnStartRollClicked);
        }

        private void OnBackToMenuClicked()
        {
            SendBackToMenuEvent();
        }

        private void OnStartRollClicked()
        {
            if (CanRoll())
                SendStartRollEvent();
        }

        private void SendBackToMenuEvent() => 
            _world.NewEntity().Set<BackToMenuEvent>();

        private void SendStartRollEvent() => 
            _world.NewEntity().Set<StartRollEvent>();

        private bool CanRoll()
        {
            foreach (int rollingStateEntity in _rollingState.Value)
                return ! rollingStateEntity.Get<RollingState>().IsRolling;
            return false;
        }
    }
}