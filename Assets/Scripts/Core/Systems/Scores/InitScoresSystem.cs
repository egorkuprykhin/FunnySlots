using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class InitScoresSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<AddScoresEvent>> _addScoresEvents;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;
        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            InitScores();
        }

        private void InitScores()
        {
            var scoresEntity = _world.NewEntity();
            scoresEntity.Set<Scores>();
            
            ScoresView instance = _coreFactory.Value.Create<ScoresView, Transform>(_sceneData.Value.ScoreViewParent);
            scoresEntity.Get<ScoreViewRef>().Value = instance;
        }
    }
}