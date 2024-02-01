using FunnySlots.Init;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class InitFieldMaskSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _factory;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            MaskView instance = _factory.Value.Create<MaskView>();
            InitView(instance);
        }

        private void InitView(MaskView instance)
        {
            int maskEntity = _world.NewEntity();
            maskEntity.Get<MaskViewRef>(_world).Value = instance;
        }
    }
}