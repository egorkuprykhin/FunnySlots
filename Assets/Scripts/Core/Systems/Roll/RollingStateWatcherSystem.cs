using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class RollingStateWatcherSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvents;
        private EcsFilterInject<Inc<StopRollEvent>> _stopRollEvents;
        private EcsFilterInject<Inc<RollingState>> _rollingState;

        private EcsCustomInject<Configuration> _configuration;

        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            _world.Create<RollingState>().IsRolling = false;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var startRollEventEntity in _startRollEvents.Value)
            foreach (var rollingStateEntity in _rollingState.Value)
                SetRollingState(rollingStateEntity,true);
            
            foreach (var stopRollEventEntity in _stopRollEvents.Value) 
            foreach (var rollingStateEntity in _rollingState.Value)
                ResetRollingStateAfterDelay(rollingStateEntity);
        }

        private async void ResetRollingStateAfterDelay(int rollingStateEntity)
        {
            await UniTask.Delay(_configuration.Value.WinDelayMs);
            SetRollingState(rollingStateEntity, false);
        }

        private void SetRollingState(int rollingStateEntity, bool rollingState) => 
            rollingStateEntity.Get<RollingState>().IsRolling = rollingState;
    }
}