using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class CreateCardSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CreateCardEvent, CardData>, Exc<CardViewRef>> _cardsToCreate;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cardsToCreate.Value) 
                CreateCard(entity);
        }

        private void CreateCard(int cardEntity)
        {
            CardView instance = _coreFactory.Value.Create<CardView, CardData>(cardEntity.Get<CardData>());
            
            InitViewRef(cardEntity, instance);
            ClearCreateEvent(cardEntity);
        }

        private void InitViewRef(int cardEntity, CardView instance) => 
            cardEntity.Get<CardViewRef>().CardView = instance;

        private void ClearCreateEvent(int cardEntity) => 
            cardEntity.Delete<CreateCardEvent>();
    }
}