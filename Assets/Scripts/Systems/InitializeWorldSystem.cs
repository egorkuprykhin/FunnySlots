using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitializeWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            Vector2Int fieldSize = _configuration.Value.FieldSize;
            Vector2 cellSize = _configuration.Value.CellSize;

            for (int x = 0; x < fieldSize.x; x++)
            for (int y = 0; y < fieldSize.y; y++)
            {
                Vector2 position = new Vector2(
                    cellSize.x * (x + 0.5f - 0.5f * fieldSize.x),
                    cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));

                CreateNewCardEntity(position);
            }
        }

        private int CreateNewCardEntity(Vector2 position)
        {
            int cardEntity = _world.Value.NewEntity();
            
            cardEntity.Get<CardPosition>(_world).Position = position;
            cardEntity.Get<CardMoving>(_world).IsMoving = false;
            
            cardEntity.Set<CreateCardEvent>(_world);
            cardEntity.Set<SetCardSpriteEvent>(_world);

            return cardEntity;
        }
    }
}