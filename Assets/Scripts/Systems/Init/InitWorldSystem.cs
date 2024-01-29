using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CardsSpriteSelectorService> _spriteSelector;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            var fieldSize = _configuration.Value.FieldSize;
            var cellSize = _configuration.Value.CellSize;
            var extraCells = _configuration.Value.ExtraCells;

            for (int x = 0; x < fieldSize.x + extraCells.x; x++)
            for (int y = 0; y < fieldSize.y + extraCells.y; y++)
            {
                CardInitializeData cardInitializeData = _spriteSelector.Value.GetRandomCardEntryData();
                Vector2 position = new Vector2(
                    cellSize.x * (x + 0.5f - 0.5f * fieldSize.x),
                    cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));

                int newCardEntity = _world.NewEntity();
                ref var cardCreationData = ref newCardEntity.Get<CardData>(_world);

                cardCreationData.Position = position;
                cardCreationData.InitializeData = cardInitializeData;
                cardCreationData.Row = x;

                newCardEntity.Set<CreateCardEvent>(_world);
            }
        }
    }
}