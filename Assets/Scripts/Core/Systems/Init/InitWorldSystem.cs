using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<CardsSpriteSelectorService> _spriteSelector;
        private EcsCustomInject<FieldPositionsService> _fieldPositionService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            var fieldSize = _configuration.Value.FieldSize;
            var extraCells = _configuration.Value.ExtraCells;

            for (int x = 0; x < fieldSize.x + extraCells.x; x++)
            for (int y = 0; y < fieldSize.y + extraCells.y; y++)
            {
                int newCardEntity = _world.NewEntity();
                ref var cardCreationData = ref newCardEntity.Get<CardData>(_world);

                cardCreationData.Position = _fieldPositionService.Value.GetFieldPosition(x, y);
                cardCreationData.InitialData = _spriteSelector.Value.GetRandomCardEntryData();
                cardCreationData.Row = x;

                newCardEntity.Set<CreateCardEvent>(_world);
            }
        }
    }
}