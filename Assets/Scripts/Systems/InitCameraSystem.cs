using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitCameraSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<SceneData> _sceneData;

        public void Init(IEcsSystems systems)
        {
            Camera camera = _sceneData.Value.MainCamera;
            Configuration configuration = _configuration.Value;

            camera.orthographic = true;
            camera.orthographicSize = 
                configuration.CellSize.x * (configuration.FieldSize.x + 1) + configuration.CameraPadding * 2;
        }
    }
}