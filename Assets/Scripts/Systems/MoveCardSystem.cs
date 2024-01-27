using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class MoveCardSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<CardViewRef, MovingState>> _filter;
        private readonly EcsCustomInject<Configuration> _configuration;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref MovingState movingState = ref _world.Value.Get<MovingState>(entity);
                if (movingState.IsMoving) 
                    Move(entity);
            }
        }

        private void Move(int entity)
        {
            Transform cardViewTransform = _world.Value.Get<CardViewRef>(entity).Value.transform;
            Vector3 position = cardViewTransform.position;
                
            Vector2 positionDelta = _configuration.Value.CardMoveSpeed * Time.deltaTime;
            Vector3 nextPosition = new Vector3(position.x + positionDelta.x, position.y + positionDelta.y, 0);

            cardViewTransform.position = nextPosition;
        }
    }
}