using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class DestroyCardSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DestroyCardEvent, CardViewRef>, Exc<HighestCardInRow>> _cardsToDestroy;
        
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int cardEntity in _cardsToDestroy.Value) 
                DestroyCard(cardEntity);
        }

        private void DestroyCard(int cardEntity)
        {
            DestroyCardView(cardEntity);
            ClearDestroyEvent(cardEntity);
            DeleteCardEntity(cardEntity);
        }

        private void ClearDestroyEvent(int cardEntity) => 
            cardEntity.Del<DestroyCardEvent>();

        private void DestroyCardView(int cardEntity) => 
            Object.Destroy(cardEntity.Get<CardViewRef>().CardView.gameObject);

        private void DeleteCardEntity(int entity) => 
            _world.Value.DelEntity(entity);
    }
}