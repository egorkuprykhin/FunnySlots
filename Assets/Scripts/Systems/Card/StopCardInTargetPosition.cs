using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class StopCardInTargetPosition : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TargetPosition, CardData>> _cardsToStop;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cardsToStop.Value)
            {
                ref CardData cardData = ref entity.Get<CardData>(_world);
                ref TargetPosition targetPosition = ref entity.Get<TargetPosition>(_world);

                if (CardInPosition(cardData, targetPosition))
                {
                    cardData.Position = targetPosition.Value;
                    cardData.IsMoving = false;
                    
                    entity.Del<TargetPosition>(_world);
                }
            }
        }

        private static bool CardInPosition(CardData cardData, TargetPosition targetPosition)
        {
            return cardData.Position.y <= targetPosition.Value.y;
        }
    }
}