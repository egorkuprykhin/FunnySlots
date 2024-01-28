using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudPlayButtonReactSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button PlayButton;
        
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<CardMoving>> _filter;

        public void Init(IEcsSystems systems)
        {
            PlayButton.onClick.AddListener(RollSlots);
        }

        private void RollSlots()
        {
            foreach (int entity in _filter.Value)
            {
                ref CardMoving cardMoving = ref entity.Get<CardMoving>(_world.Value);
                cardMoving.IsMoving = !cardMoving.IsMoving;
            }
        }
    }
}