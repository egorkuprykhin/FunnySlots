using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CreateNewCardsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HighestCardInRow, CardData, CardViewRef>> _highestCards;

        private EcsCustomInject<CardsSpriteSelectorService> _spriteSelector;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            float createCardClippingDistanceY = _configuration.Value.ExtraCells.y * _configuration.Value.CellSize.y;
            
            foreach (int highestCardEntity in _highestCards.Value)
            {
                ref CardData highestCardData = ref highestCardEntity.Get<CardData>(_world);
                
                if (highestCardData.Position.y < createCardClippingDistanceY) 
                    CreateNewCardAboveHighest(ref highestCardData, highestCardEntity);
            }
        }

        private void CreateNewCardAboveHighest(ref CardData highestCardData, int highestCardEntity)
        {
            CardInitializeData cardInitializeData = _spriteSelector.Value.GetRandomCardEntryData();
            Vector2 position = highestCardData.Position + OneCardUpOffset();

            int createdCardEntity = _world.NewEntity();
            ref var cardCreationData = ref createdCardEntity.Get<CardData>(_world);

            cardCreationData.InitialData = cardInitializeData;
            cardCreationData.Position = position;
            
            cardCreationData.Row = highestCardData.Row;
            cardCreationData.IsMoving = highestCardData.IsMoving;

            createdCardEntity.Set<CreateCardEvent>(_world);
        }

        private Vector2 OneCardUpOffset() => new(0, _configuration.Value.CellSize.y);
    }
}