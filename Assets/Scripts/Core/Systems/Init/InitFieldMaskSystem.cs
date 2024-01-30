using FunnySlots.Init;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitFieldMaskSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            int maskEntity = _world.NewEntity();
            
            MaskView instance = Object.Instantiate(_configuration.Value.MaskView);

            maskEntity.Get<MaskViewRef>(_world).Value = instance;

            var scale = new Vector3(
                _configuration.Value.FieldSize.x + 1, 
                _configuration.Value.FieldSize.y, 
                1);

            instance.transform.localScale = scale;
        }
    }
}