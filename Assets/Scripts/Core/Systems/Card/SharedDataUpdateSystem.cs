using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots.Systems
{
    [Obsolete]
    public class SharedDataUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData>> _cards;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            var shared = systems.GetShared<SharedData>();
            
            foreach (var card in _cards.Value)
            {
                ref var cardData = ref card.Get<CardData>();

                shared.CardsToPositions.TryAdd(card, cardData.Position);
                shared.PositionsToCards.TryAdd(cardData.Position, card);

                shared.CardsToPositions[card] = cardData.Position;
                shared.PositionsToCards[cardData.Position] = card;
            }
        }
    }
}