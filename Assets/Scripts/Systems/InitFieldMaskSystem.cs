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
            GameObject maskPrefab = _configuration.Value.MaskPrefab;

            GameObject createdMask = Object.Instantiate(maskPrefab);

            var position = new Vector3(0, _configuration.Value.FieldSize.x * 0.5f);
            
            var scale = new Vector3(
                _configuration.Value.FieldSize.x + 1, 
                _configuration.Value.FieldSize.y + 1, 
                1);


            createdMask.transform.localPosition = position;
            createdMask.transform.localScale = scale;
        }
    }
}