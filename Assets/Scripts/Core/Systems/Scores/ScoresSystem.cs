using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class ScoresSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Scores>> _scores;
        private EcsFilterInject<Inc<AddScoresEvent>> _addScoresEvents;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;
        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int addScoresEntity in _addScoresEvents.Value)
            foreach (int scoresEntity in _scores.Value)
            {
                AddScores(scoresEntity, addScoresEntity);
                UpdateScoresView(scoresEntity);
                DeleteAddScoresEvent(addScoresEntity);
            }
        }

        private void AddScores(int scoresEntity, int addScoresEntity) => 
            scoresEntity.Get<Scores>().Value += addScoresEntity.Get<AddScoresEvent>().Value;

        private void UpdateScoresView(int scoresEntity) => 
            scoresEntity.Get<ScoreViewRef>().Value.ScoreValue.text = scoresEntity.Get<Scores>().Value.ToString();

        private void DeleteAddScoresEvent(int addScoreEntity) => 
            addScoreEntity.Delete<AddScoresEvent>();
    }
}