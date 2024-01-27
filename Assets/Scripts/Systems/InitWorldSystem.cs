using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Configuration> _configuration = default;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            ref var fieldSize = ref _configuration.Value.FieldSize;
            ref var cellSize = ref _configuration.Value.CellSize;
            
            for (int x = 0; x < fieldSize.x; x++)
            {
                int cardsRowEntity = world.NewEntity();
                ref CardsRow cardsRow = ref world.Get<CardsRow>(cardsRowEntity);
                cardsRow.ViewsInRow = new List<CardViewRef>(256);
                
                for (int y = 0; y < fieldSize.y; y++)
                {
                    Vector2 position = new Vector2(
                        cellSize.x * (x + 0.5f - 0.5f * fieldSize.x), 
                        cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));

                    CardView cardPrefab = _configuration.Value.CardView;
                    CardView instance = Object.Instantiate(cardPrefab, position, Quaternion.identity);

                    int cardEntity = world.NewEntity();

                    ref var cardViewRef = ref world.Get<CardViewRef>(cardEntity);
                    cardViewRef.Value = instance;
                    
                    ref var movingState = ref world.Get<MovingState>(cardEntity);
                    movingState.IsMoving = false;
                    
                    cardsRow.ViewsInRow.Add(cardViewRef);
                }
            }
            
        }
    }
}