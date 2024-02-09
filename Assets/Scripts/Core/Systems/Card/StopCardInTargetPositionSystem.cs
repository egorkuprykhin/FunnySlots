using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class StopCardInTargetPositionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardTargetPosition, CardData>> _cardsToStop;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            bool playSound = false;
            
            foreach (int cardEntity in _cardsToStop.Value)
            
                if (CardInTargetPosition(cardEntity))
                {
                    StopCardInTargetPosition(cardEntity);
                    DeleteTargetPosition(cardEntity);
                    playSound = true;
                }

            if (playSound) 
                PlayStopRowSound();
        }

        private bool CardInTargetPosition(int cardEntity)
        {
            CardData cardData = cardEntity.Get<CardData>();
            CardTargetPosition cardTargetPosition = cardEntity.Get<CardTargetPosition>();

            return cardData.Position.y <= cardTargetPosition.Value.y;
        }

        private void StopCardInTargetPosition(int cardEntity)
        {
            ref CardData cardData = ref cardEntity.Get<CardData>();
            
            cardData.Position = cardEntity.Get<CardTargetPosition>().Value;
            cardData.IsMoving = false;
        }

        private void PlayStopRowSound() => 
            _world.Create<PlaySoundEvent>().EventType = SoundEventType.RowStopped;

        private void DeleteTargetPosition(int cardEntity) => 
            cardEntity.Delete<CardTargetPosition>();
    }
}