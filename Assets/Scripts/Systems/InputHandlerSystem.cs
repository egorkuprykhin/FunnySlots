using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.UI;

namespace FunnySlots
{
    public class InputHandlerSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button PlayButton;
        
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<MovingState>> _filter;
        private bool _subscribed;

        public void Init(IEcsSystems systems)
        {
            PlayButton.onClick.AddListener(RollSlots);
        }

        private void RollSlots()
        {
            foreach (int entity in _filter.Value)
            {
                ref MovingState movingState = ref _world.Value.Get<MovingState>(entity);
                movingState.IsMoving = !movingState.IsMoving;
            }
        }
    }
}