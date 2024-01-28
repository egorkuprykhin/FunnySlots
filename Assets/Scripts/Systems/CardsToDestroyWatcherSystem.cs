using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsToDestroyWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardPosition, CardViewRef>, Exc<HighestCardMarker>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            var minOffsetToDestroyCard = _configuration.Value.MinOffsetToDestroyCard * _configuration.Value.CellSize.y;
            foreach (int entity in _filter.Value)
            {
                var position = entity.Get<CardPosition>(_world.Value).Position;
                
                if (Mathf.Abs(position.y) > minOffsetToDestroyCard)
                {
                    entity.Set<DestroyCardEvent>(_world.Value);
                }
            }
        }
    }
}