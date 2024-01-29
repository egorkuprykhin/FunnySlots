using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudPlayButtonSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button _playButton;

        private EcsWorldInject _world;

        private Type lastUsedEventType = typeof(StopCardsMoveEvent);

        public void Init(IEcsSystems systems)
        {
            _playButton.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            if (lastUsedEventType == typeof(StopCardsMoveEvent))
               CreateStartMovingEvent();
            else
                CreateStopMovingEvent();
        }

        private void CreateStartMovingEvent()
        {
            int movingWatcherEntity = _world.NewEntity();
            movingWatcherEntity.Set<StartCardsMoveEvent>(_world);
            
            lastUsedEventType = typeof(StartCardsMoveEvent);
        }
        
        private void CreateStopMovingEvent()
        {
            int movingWatcherEntity = _world.NewEntity();
            movingWatcherEntity.Set<StopCardsMoveEvent>(_world);
            
            lastUsedEventType = typeof(StopCardsMoveEvent);
        }
    }
}