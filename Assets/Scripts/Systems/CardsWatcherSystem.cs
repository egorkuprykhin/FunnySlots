using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class CardsWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardsRow>, Exc<CreateCardEvent>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            foreach (int entity in _filter.Value)
            {
                var cardsInRow = world.Get<CardsRow>(entity).ViewsInRow.Count;
                if (cardsInRow < _configuration.Value.MinCardsInRow)
                {
                    world.Get<CreateCardEvent>(entity);
                }
            }
        }
    }
}