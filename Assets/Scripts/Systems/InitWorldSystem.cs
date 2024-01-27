using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            ref var fieldSize = ref _configuration.Value.FieldSize;
            ref var cellSize = ref _configuration.Value.CellSize;
            
            for (int x = 0; x < fieldSize.x; x++)
                for (int y = 0; y < fieldSize.y; y++)
                {
                    Vector2 position = new Vector2(
                        cellSize.x * (x + 0.5f - 0.5f * fieldSize.x), 
                        cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));
                    
                    CreateCardEntity(position, x);
                }
        }

        private void CreateCardEntity(Vector2 position, int row)
        {
            int cardEntity = _world.Value.NewEntity();
            
            _world.Value.Get<CardPosition>(cardEntity).Position = position;
            _world.Value.Get<CardRow>(cardEntity).Row = row;
            _world.Value.Get<CardMoving>(cardEntity).IsMoving = false;
            
            _world.Value.SetEvent<CreateCardEvent>(cardEntity);
        }
    }
}