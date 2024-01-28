using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudPlayButtonReactSystem : IEcsInitSystem
    {
        [EcsUguiNamed("PlayButton")] Button PlayButton;

        private EcsFilterInject<Inc<CardMoving>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            PlayButton.onClick.AddListener(RollSlots);
        }

        private void RollSlots()
        {
            foreach (int entity in _filter.Value)
            {
                ref CardMoving cardMoving = ref entity.Get<CardMoving>(_world);
                cardMoving.IsMoving = !cardMoving.IsMoving;
            }

            CreateFinishRollingConditions();
        }

        private void CreateFinishRollingConditions()
        {
            float rollingTime = Random.Range(
                _configuration.Value.RollingTimeRange.x,
                _configuration.Value.RollingTimeRange.y);

            int movingWatcherEntity = _world.NewEntity();
            movingWatcherEntity.Get<WaitForStopMoving>(_world).RollingTime = rollingTime;
        }
    }
}