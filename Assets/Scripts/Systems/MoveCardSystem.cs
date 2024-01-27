using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class MoveCardSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CardMoving, CardPosition>> _filter;
        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                if (_world.Value.Get<CardMoving>(entity).IsMoving) 
                    Move(entity);
            }
        }

        private void Move(int entity)
        {
            ref var initialPosition = ref _world.Value.Get<CardPosition>(entity).Position;
            var deltaPosition  =  _configuration.Value.CardMoveSpeed * Time.deltaTime;
            
            initialPosition += deltaPosition;
        }
    }
}