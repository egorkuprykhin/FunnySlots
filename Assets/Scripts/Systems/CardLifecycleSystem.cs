using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardLifecycleSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CreateCardEvent, CardData>, Exc<CardViewRef>> _cardsToCreate;
        private EcsFilterInject<Inc<DestroyCardEvent, CardViewRef>, Exc<HighestCardMarker>> _cardsToDestroy;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cardsToCreate.Value) 
                CreateCardView(entity);

            foreach (int entity in _cardsToDestroy.Value) 
                DestroyCard(entity);
        }

        private void CreateCardView(int entity)
        {
            var createCardData = entity.Get<CardData>(_world);

            var cardPrefab = _configuration.Value.CardView;
            var position = createCardData.Position;

            CardView instance = Object.Instantiate(cardPrefab, position, Quaternion.identity);

            instance.Renderer.sprite = createCardData.CardEntry.Sprite;

            entity.Get<CardViewRef>(_world).CardView = instance;
            
            entity.Del<CreateCardEvent>(_world);
        }

        private void DestroyCard(int entity)
        {
            var cardView = entity.Get<CardViewRef>(_world).CardView;
            
            Object.Destroy(cardView.gameObject);
            
            entity.Del<DestroyCardEvent>(_world);
            
            _world.Value.DelEntity(entity);
        }
    }
}