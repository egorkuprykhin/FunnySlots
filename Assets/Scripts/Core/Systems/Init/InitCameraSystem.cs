using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitCameraSystem : IEcsInitSystem
    {
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<FieldPositionsService> _fieldPositionService;
        private EcsCustomInject<SceneData> _sceneData;

        public void Init(IEcsSystems systems)
        {
            Camera camera = _sceneData.Value.MainCamera;

            camera.orthographic = true;
            camera.orthographicSize = _fieldPositionService.Value.GetCameraSize();
        }
    }
}