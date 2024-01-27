using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class SpawnCardsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardsRow, CreateCardEvent>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            foreach (int entity in _filter.Value)
            {
                var cardsRow = world.Get<CardsRow>(entity);
                var maxY = cardsRow.ViewsInRow.Max(view => view.Value.transform.position.y);
                var cardView = cardsRow.ViewsInRow.First(view => view.Value.transform.position.y >= maxY).Value;
                
                var position = cardView.transform.position + new Vector3(0, _configuration.Value.CellSize.y,0);
                
                CardView cardPrefab = _configuration.Value.CardView;
                CardView instance = Object.Instantiate(cardPrefab, position, Quaternion.identity);
                
                int cardEntity = world.NewEntity();

                ref var cardViewRef = ref world.Get<CardViewRef>(cardEntity);
                cardViewRef.Value = instance;
                
                ref var movingState = ref world.Get<MovingState>(cardEntity);
                movingState.IsMoving = false;
                    
                cardsRow.ViewsInRow.Add(cardViewRef);
                
                world.GetPool<CreateCardEvent>().Del(entity);
            }
        }
    }
}