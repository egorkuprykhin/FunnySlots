using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class InitWorldSystem : IEcsInitSystem
    {
        private EcsCustomInject<CardsInitializeDataService> _cardsInitializeDataService;
        private EcsCustomInject<FieldPositionsService> _fieldPositionService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            for (int x = 0; x < _configuration.Value.FieldSize.x + _configuration.Value.ExtraCells.x; x++)
            for (int y = 0; y < _configuration.Value.FieldSize.y + _configuration.Value.ExtraCells.y; y++)
            {
                int newCardEntity = _world.NewEntity();
                ref var cardCreationData = ref newCardEntity.Get<CardData>(_world);

                cardCreationData.Position = _fieldPositionService.Value.GetPositionForCell(x, y);
                cardCreationData.InitialData = _cardsInitializeDataService.Value.GetRandomCardInitializeData();
                cardCreationData.Row = x;

                newCardEntity.Set<CreateCardEvent>(_world);
            }
        }
    }
}