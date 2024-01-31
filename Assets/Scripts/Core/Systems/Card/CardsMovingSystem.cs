using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsMovingSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CardData>> _cards;
        
        private readonly EcsCustomInject<Configuration> _configuration;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cards.Value)
            {
                ref var cardData = ref entity.Get<CardData>(_world);
                
                if (cardData.IsMoving)
                    Move(ref cardData);
                
                else if (entity.Has<TargetPosition>(_world))
                {
                    SetPosition(ref cardData, entity.Get<TargetPosition>(_world).Value);
                    entity.Del<TargetPosition>(_world);
                }
            }
        }

        private void Move(ref CardData cardData)
        {
            var position  = cardData.Position + _configuration.Value.CardMoveSpeed * Time.deltaTime;
            SetPosition(ref cardData, position);
        }

        private void SetPosition(ref CardData cardData, Vector2 position)
        {
            cardData.Position = position;
        }
    }
}