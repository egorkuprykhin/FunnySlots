using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitCameraSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Configuration> _configuration = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Init(IEcsSystems systems)
        {
            Camera camera = _sceneData.Value.MainCamera;
            camera.orthographic = true;
            Configuration configuration = _configuration.Value;
            camera.orthographicSize = configuration.CardsOffset.x * (1 + configuration.FieldSize.x) + 2 * configuration.CameraPadding;
        }
    }
}