using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class StopCardInTargetPositionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TargetPosition, CardData>> _cardsToStop;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int cardEntity in _cardsToStop.Value)
                
                if (CardInTargetPosition(cardEntity))
                {
                    StopCardInTargetPosition(cardEntity);
                    DeleteTargetPosition(cardEntity);
                }
        }

        private bool CardInTargetPosition(int cardEntity)
        {
            CardData cardData = cardEntity.Get<CardData>(_world);
            TargetPosition targetPosition = cardEntity.Get<TargetPosition>(_world);

            return cardData.Position.y <= targetPosition.Value.y;
        }

        private void StopCardInTargetPosition(int cardEntity)
        {
            ref CardData cardData = ref cardEntity.Get<CardData>(_world);
            
            cardData.Position = cardEntity.Get<TargetPosition>(_world).Value;
            cardData.IsMoving = false;
        }

        private void DeleteTargetPosition(int cardEntity) => 
            cardEntity.Del<TargetPosition>(_world);
    }
}