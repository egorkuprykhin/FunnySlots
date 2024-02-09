using System.Linq;
using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class PlaySoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlaySoundEvent>> _playSoundEvents;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<CoreFactory> _coreFactory;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var playSoundEntity in _playSoundEvents.Value)
            {
                SoundEventType type = playSoundEntity.Get<PlaySoundEvent>().EventType;
                SoundDataEntry soundData = _configuration.Value.Sounds.Sounds.FirstOrDefault(sound => sound.Event == type);
                
                if (soundData != null && soundData.AudioClipsData.Length > 0)
                    foreach (var clipData in soundData.AudioClipsData)
                        PLayAudioClip(clipData);

                playSoundEntity.Delete<PlaySoundEvent>();
            }
        }

        private void PLayAudioClip(AudioClipData clipData)
        {
            AudioSourceView audioSourceView = _coreFactory.Value.Create<AudioSourceView>();
            
            audioSourceView.AudioSource.clip = clipData.AudioClip;
            audioSourceView.AudioSource.volume = clipData.Volume;
            audioSourceView.AudioSource.Play();

            ref PlayingSound playingSound = ref _world.Create<PlayingSound>();

            playingSound.ElapsedTime = 0;
            playingSound.LifeTime = clipData.SoundLength;
            playingSound.AudioSourceView = audioSourceView;
        }
    }
}