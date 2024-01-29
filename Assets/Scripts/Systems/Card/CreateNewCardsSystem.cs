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
            float offsetYToCreateCard = _configuration.Value.ExtraCells.y * _configuration.Value.CellSize.y;
            
            foreach (int entity in _highestCards.Value)
            {
                ref CardData highestCardData = ref entity.Get<CardData>(_world);
                
                if (highestCardData.Position.y < offsetYToCreateCard) 
                    CreateNewCardAboveHighest(ref highestCardData);
            }
            
            Debug.Log($"Highest cards {_highestCards.Value.GetEntitiesCount()}");
        }

        private void CreateNewCardAboveHighest(ref CardData highestCardData)
        {
            CardInitializeData cardInitializeData = _spriteSelector.Value.GetRandomCardEntryData();
            Vector2 position = highestCardData.Position + new Vector2(0, _configuration.Value.CellSize.y);

            int createdCardEntity = _world.NewEntity();
            ref var cardCreationData = ref createdCardEntity.Get<CardData>(_world);

            cardCreationData.InitializeData = cardInitializeData;
            cardCreationData.Position = position;
            
            cardCreationData.Row = highestCardData.Row;
            cardCreationData.IsMoving = highestCardData.IsMoving;
            
            createdCardEntity.Set<CreateCardEvent>(_world);
        }
    }
}