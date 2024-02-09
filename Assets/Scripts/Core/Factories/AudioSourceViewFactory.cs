using UnityEngine;

namespace FunnySlots
{
    public class AudioSourceViewFactory : IFactory<AudioSourceView>
    {
        private readonly Configuration _configuration;

        public AudioSourceViewFactory(Configuration configuration) => 
            _configuration = configuration;

        public AudioSourceView Create()
        {
            AudioSourceView instance = Object.Instantiate(_configuration.AudioSourceView);

            return instance;
        }
    }
}