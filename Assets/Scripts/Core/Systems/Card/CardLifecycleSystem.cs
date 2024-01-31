using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardLifecycleSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CreateCardEvent, CardData>, Exc<CardViewRef>> _cardsToCreate;
        private EcsFilterInject<Inc<DestroyCardEvent, CardViewRef>, Exc<HighestCardInRow>> _cardsToDestroy;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cardsToCreate.Value) 
                CreateCard(entity);

            foreach (int entity in _cardsToDestroy.Value) 
                DestroyCard(entity);
        }

        private void CreateCard(int cardEntity)
        {
            InstantiateCardView(cardEntity, ref cardEntity.Get<CardData>(_world));
            ClearCreateEvent(cardEntity);
        }

        private void DestroyCard(int cardEntity)
        {
            DestroyCardView(cardEntity);
            ClearDestroyEvent(cardEntity);
            DeleteCardEntity(cardEntity);
        }

        private void InstantiateCardView(int cardEntity, ref CardData cardData)
        {
            CardView instance = InstantiateInPosition(cardData.Position);
            instance.CardRenderer.sprite = cardData.InitialData.Sprite;
            cardEntity.Get<CardViewRef>(_world).CardView = instance;
        }

        private CardView InstantiateInPosition(Vector2 position) =>
            Object.Instantiate(_configuration.Value.CardView, position, Quaternion.identity);

        private void ClearCreateEvent(int cardEntity) => 
            cardEntity.Del<CreateCardEvent>(_world);

        private void ClearDestroyEvent(int cardEntity) => 
            cardEntity.Del<DestroyCardEvent>(_world);

        private void DestroyCardView(int cardEntity) => 
            Object.Destroy(cardEntity.Get<CardViewRef>(_world).CardView.gameObject);

        private void DeleteCardEntity(int entity) => 
            _world.Value.DelEntity(entity);
    }
}