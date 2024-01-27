using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsCustomInject<Configuration> _configuration = default;
        
        public void Init(IEcsSystems systems)
        {
            ref Vector2Int fieldSize = ref _configuration.Value.FieldSize;
            ref Vector2 offset = ref _configuration.Value.CardsOffset;
            
            for (int x = 0; x < fieldSize.x; x++)
            for (int y = 0; y < fieldSize.y; y++)
            {
                int cardEntity = _defaultWorld.Value.NewEntity();

                Vector2 position = new Vector2(
                    offset.x * (x + 0.5f - 0.5f * fieldSize.x), 
                    offset.y * (y + 0.5f - 0.5f * fieldSize.y));
                
                CardView instance = Object.Instantiate(_configuration.Value.CardView, position, Quaternion.identity);
                    
                EcsPool<Coordinates> coordinatesPool = _defaultWorld.Value.GetPool<Coordinates>();
                ref Coordinates coordinates = ref coordinatesPool.Add(cardEntity);
                coordinates.value = new Vector2Int(x, y);

                EcsPool<CardViewRef> cardViewRefsPool = _defaultWorld.Value.GetPool<CardViewRef>();
                ref CardViewRef cardViewRef = ref cardViewRefsPool.Add(cardEntity);
                cardViewRef.value = instance;
            }
        }
    }
}