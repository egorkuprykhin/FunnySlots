using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class MoveCardSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CardData>> _cards;
        
        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cards.Value)
                if (entity.Get<CardData>(_world).IsMoving)
                    Move(entity);
        }

        private void Move(int entity)
        {
            ref var position = ref entity.Get<CardData>(_world).Position;
            var deltaPosition  =  _configuration.Value.CardMoveSpeed * Time.deltaTime;
            
            position += deltaPosition;
        }
    }
}