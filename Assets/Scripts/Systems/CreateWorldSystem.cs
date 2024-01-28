using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CreateWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            SharedData shared = systems.GetShared<SharedData>();
            shared.MinYCoordinate = _configuration.Value.CellSize.y * _configuration.Value.FieldSize.y * 2;
            
            Vector2Int fieldSize = _configuration.Value.FieldSize;
            Vector2 cellSize = _configuration.Value.CellSize;
            
            for (int x = 0; x < fieldSize.x; x++)
                for (int y = 0; y < fieldSize.y; y++)
                {
                    Vector2 position = new Vector2(
                        cellSize.x * (x + 0.5f - 0.5f * fieldSize.x), 
                        cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));
                    
                    int cardEntity = CreateCardEntity(position, x);
                    
                    if (HighestByYCard(y))
                        _world.Value.Set<HighestCardInRow>(cardEntity);
                }

            bool HighestByYCard(int y) => y >= fieldSize.y - 1;
        }

        private int CreateCardEntity(Vector2 position, int row)
        {
            int cardEntity = _world.Value.NewEntity();
            
            _world.Value.Get<CardPosition>(cardEntity).Position = position;
            _world.Value.Get<CardMoving>(cardEntity).IsMoving = false;
            
            _world.Value.SetEvent<CreateCardEvent>(cardEntity);

            return cardEntity;
        }
    }
}