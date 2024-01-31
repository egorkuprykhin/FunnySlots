using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class DestroyOldCardsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData, CardViewRef>, Exc<HighestCardInRow, DestroyCardEvent>> _cards;

        private EcsCustomInject<CardPositionsService> _cardPositionsService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cards.Value)
                if (CardOutsideUpperClippingDistance(entity))
                    DestroyCard(entity);
        }

        private bool CardOutsideUpperClippingDistance(int entity) => 
            _cardPositionsService.Value.IsPositionBelowBottomClippingDistance(entity.Get<CardData>(_world).Position);

        private void DestroyCard(int entity) => 
            entity.Set<DestroyCardEvent>(_world);
    }
}