using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class RollingStateWatcherSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvents;
        private EcsFilterInject<Inc<StopRollEvent>> _stopRollEvents;

        private EcsCustomInject<Configuration> _configuration;

        private EcsWorldInject _world;
        
        private int _rollingStateEntity;

        public void Init(IEcsSystems systems)
        {
            _rollingStateEntity = _world.NewEntity();
            _rollingStateEntity.Set<RollingState>(_world);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var startRollEventEntity in _startRollEvents.Value) 
                SetRollingState(true);
            
            foreach (var stopRollEventEntity in _stopRollEvents.Value) 
                ResetRollingStateAfterDelay();
        }

        private void SetRollingState(bool rollingState)
        {
            Debug.Log("SetRollingState");
            _rollingStateEntity.Get<RollingState>(_world).IsRolling = rollingState;
        }

        private async void ResetRollingStateAfterDelay()
        {
            await UniTask.Delay(_configuration.Value.WinTimeMs);

            Debug.Log("ResetRollingStateAfterDelay");
            SetRollingState(false);
        }
    }
}