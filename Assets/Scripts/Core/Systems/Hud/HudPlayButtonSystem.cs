using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudPlayButtonSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button _playButton;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            _playButton.onClick.AddListener(CreateStartRollEvent);
        }

        private void CreateStartRollEvent()
        {
            int newEntity = _world.NewEntity();
            newEntity.Set<StartRollEvent>(_world);
        }
    }
}