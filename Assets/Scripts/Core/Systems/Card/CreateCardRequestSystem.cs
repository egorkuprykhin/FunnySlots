using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class CreateCardRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HighestCardInRow, CardData, CardViewRef>> _highestCards;

        private EcsCustomInject<CardsInitializeDataService> _cardsInitializeDataService;
        private EcsCustomInject<FieldPositionsService> _cardsPositionsService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int highestCardEntity in _highestCards.Value)
            {
                ref CardData highestCardData = ref highestCardEntity.Get<CardData>();
                
                if (HighestCardBelowClippingDistance(ref highestCardData)) 
                    CreateNewCardAboveHighest(ref highestCardData);
            }
        }

        private bool HighestCardBelowClippingDistance(ref CardData highestCardData) => 
            _cardsPositionsService.Value.IsPositionBelowTopClippingDistance(highestCardData.Position);

        private void CreateNewCardAboveHighest(ref CardData highestCardData)
        {
            int createdCardEntity = InitCardWithHighestCardData(highestCardData);

            createdCardEntity.Set<CreateCardEvent>();
        }

        private int InitCardWithHighestCardData(CardData highestCardData)
        {
            int createdCardEntity = _world.NewEntity();
            
            ref var cardCreationData = ref createdCardEntity.Get<CardData>();

            cardCreationData.InitialData = _cardsInitializeDataService.Value.GetRandomCardInitializeData();
            cardCreationData.Position = _cardsPositionsService.Value.GetPositionForCellAbove(highestCardData.Position);
            cardCreationData.Row = highestCardData.Row;
            cardCreationData.IsMoving = highestCardData.IsMoving;

            return createdCardEntity;
        }
    }
}