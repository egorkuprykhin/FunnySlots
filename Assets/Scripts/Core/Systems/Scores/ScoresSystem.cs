using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class ScoresSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<AddScoresEvent>> _addScoresEvents;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;
        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;

        private int _scoresEntity;

        public void Init(IEcsSystems systems)
        {
            InitScoresEntity();
            InitHudScoresView();
        }

        public void Run(IEcsSystems systems)
        {
            ref var scoresEntity = ref _world.Value.GetPool<Scores>().Get(_scoresEntity);
            
            foreach (int addScoreEntity in _addScoresEvents.Value)
            {
                AddScores(ref scoresEntity, addScoreEntity);
                UpdateScoresView();
                DeleteAddScoresEvent(addScoreEntity);
            }
        }

        private void AddScores(ref Scores scoresEntity, int addScoreEntity) => 
            scoresEntity.Value += addScoreEntity.Get<AddScoresEvent>().Value;

        private void UpdateScoresView()
        {
            Scores scores = _world.Value.GetPool<Scores>().Get(_scoresEntity);
            _scoresEntity.Get<ScoreViewRef>().Value.ScoreValue.text = scores.Value.ToString();
        }

        private void DeleteAddScoresEvent(int addScoreEntity) => 
            addScoreEntity.Del<AddScoresEvent>();

        private void InitScoresEntity()
        {
            _scoresEntity = _world.NewEntity();
            _scoresEntity.Get<Scores>().Value = 0;
        }

        private void InitHudScoresView()
        {
            ScoresView instance = _coreFactory.Value.Create<ScoresView, Transform>(_sceneData.Value.ScoreViewParent);
            _scoresEntity.Get<ScoreViewRef>().Value = instance;
        }
    }
}