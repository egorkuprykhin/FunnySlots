using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class SetCardsSpriteSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SetCardSpriteEvent, CardViewRef>> _filter;
        private EcsCustomInject<CardsSpriteSelectorService> _cardSpriteSelector;
        private EcsWorldInject _world;
            
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                CardEntry cardEntry = _cardSpriteSelector.Value.GetRandomCardData();
                SpriteRenderer renderer = entity.Get<CardViewRef>(_world).CardView.Renderer;

                renderer.sprite = cardEntry.Sprite;

                entity.Get<CardId>(_world).Id = cardEntry.Id;
                
                entity.Del<SetCardSpriteEvent>(_world);
            }
        }
    }
}