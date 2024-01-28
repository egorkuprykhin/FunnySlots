using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HighestCardInRow, CardPosition, CardMoving ,CardViewRef>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            var shared = systems.GetShared<SharedData>();

            foreach (int entity in _filter.Value)
            {
                ref Vector2 position = ref _world.Value.Get<CardPosition>(entity).Position;

                if (position.y < shared.MinYCoordinate)
                {
                    _world.Value.GetPool<HighestCardInRow>().Del(entity);
                    
                    Vector2 createdPosition = position + new Vector2(0, _configuration.Value.CellSize.y);
                    bool isMoving = _world.Value.Get<CardMoving>(entity).IsMoving;
                    
                    CreateCard(createdPosition, isMoving);
                }
            }
        }

        private int CreateCard(Vector2 position, bool isMoving)
        {
            int cardEntity = _world.Value.NewEntity();
            
            _world.Value.Get<CardPosition>(cardEntity).Position = position;
            _world.Value.Get<CardMoving>(cardEntity).IsMoving = isMoving;
            
            _world.Value.SetEvent<CreateCardEvent>(cardEntity);
            
            _world.Value.Set<HighestCardInRow>(cardEntity);

            return cardEntity;
        }
    }
}