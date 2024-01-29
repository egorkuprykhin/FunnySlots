using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WatchHighestCardSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData>> _cards;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            float cardMaxPosY = float.MinValue;

            foreach (int cardEntity in _cards.Value)
            {
                CardData cardData = _cards.Pools.Inc1.Get(cardEntity);

                if (cardData.Position.y > cardMaxPosY)
                    cardMaxPosY = cardData.Position.y;

                if (cardEntity.Has<HighestCardMarker>(_world))
                    cardEntity.Del<HighestCardMarker>(_world);
            }

            foreach (int cardEntity in _cards.Value)
            {
                float cardPosY = cardEntity.Get<CardData>(_world).Position.y;

                if (ApproximatelyEqual(cardPosY, cardMaxPosY))
                    cardEntity.Set<HighestCardMarker>(_world);
            }
        }

        private bool ApproximatelyEqual(float cardPosY, float cardMaxPosY) => 
            Mathf.Abs(cardPosY - cardMaxPosY) < _configuration.Value.Epsilon;
    }
    
}