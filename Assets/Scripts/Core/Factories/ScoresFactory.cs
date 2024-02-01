using UnityEngine;

namespace FunnySlots
{
    public class ScoresFactory : IFactoryWithPayload<ScoresView, Transform>
    {
        private readonly Configuration _configuration;

        public ScoresFactory(Configuration configuration) => 
            _configuration = configuration;

        public ScoresView Create(Transform parent)
        {
            ScoresView instance = 
                Object.Instantiate(_configuration.ScoresView, parent);

            return instance;
        }
    }
}