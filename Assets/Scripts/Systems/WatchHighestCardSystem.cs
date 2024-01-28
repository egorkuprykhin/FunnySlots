using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WatchHighestCardSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardPosition>> _cardPositions;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            float cardMaxPosY = float.MinValue;

            foreach (int cardEntity in _cardPositions.Value)
            {
                float cardPosY = cardEntity.Get<CardPosition>(_world).Position.y;

                if (cardPosY > cardMaxPosY)
                    cardMaxPosY = cardPosY;

                if (cardEntity.Has<HighestCardMarker>(_world))
                    cardEntity.Del<HighestCardMarker>(_world);
            }

            foreach (int cardEntity in _cardPositions.Value)
            {
                float cardPosY = cardEntity.Get<CardPosition>(_world).Position.y;

                if (EqualsAround(cardPosY, cardMaxPosY))
                    cardEntity.Get<HighestCardMarker>(_world);
            }
        }

        private bool EqualsAround(float cardPosY, float cardMaxPosY)
        {
            return Mathf.Abs(cardPosY - cardMaxPosY) < _configuration.Value.CellSize.y * 0.2f;
        }
        
    }
    
}