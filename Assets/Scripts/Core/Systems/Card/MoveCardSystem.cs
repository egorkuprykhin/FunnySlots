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
            foreach (int cardEntity in _cards.Value) 
                TryMove(cardEntity);
        }

        private void TryMove(int cardEntity)
        {
            ref var cardData = ref cardEntity.Get<CardData>();
            
            if (cardData.IsMoving)
                Move(ref cardData);
        }

        private void Move(ref CardData cardData) => 
            cardData.Position += _configuration.Value.CardMoveSpeed * Time.deltaTime;
    }
}