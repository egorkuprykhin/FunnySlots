using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class ScoresSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<AddScoresEvent>> _addScoresEvents;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<SceneData> _sceneData;
        
        private EcsWorldInject _world;
        
        private int _scoresEntity;
        
        public void Init(IEcsSystems systems)
        {
            _scoresEntity = InitScoresEntity();
            InitHudScoreView(_scoresEntity);
        }

        public void Run(IEcsSystems systems)
        {
            ref var scoresEntity = ref _world.Value.GetPool<Scores>().Get(_scoresEntity);
            
            foreach (int addScoreEntity in _addScoresEvents.Value)
            {
                ref var scoresToAdd = ref addScoreEntity.Get<AddScoresEvent>(_world).Value;
                scoresEntity.Value += scoresToAdd;

                UpdateScoresView();
                
                addScoreEntity.Del<AddScoresEvent>(_world);
            }
        }

        private void UpdateScoresView()
        {
            Scores scores = _world.Value.GetPool<Scores>().Get(_scoresEntity);
            _scoresEntity.Get<ScoreViewRef>(_world).Value.ScoreValue.text = scores.Value.ToString();
        }

        private int InitScoresEntity()
        {
            int scoresEntity = _world.NewEntity();
            scoresEntity.Get<Scores>(_world).Value = 0;
            
            return scoresEntity;
        }

        private void InitHudScoreView(int scoresEntity)
        {
            var prefab = _configuration.Value.ScoreView;
            var parent = _sceneData.Value.ScoreViewParent;

            var instance = Object.Instantiate(prefab, parent);

            scoresEntity.Get<ScoreViewRef>(_world).Value = instance;
        }
    }
}