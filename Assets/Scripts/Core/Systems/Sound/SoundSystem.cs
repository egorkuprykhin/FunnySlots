using System.Linq;
using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots.Systems
{
    public class SoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlaySoundEvent>> _playSoundEvents;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<SceneData> _sceneData;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var playSound in _playSoundEvents.Value)
            {
                if (playSound.Get<PlaySoundEvent>(_world).NeedPlay)
                {
                    CoreSound type = playSound.Get<PlaySoundEvent>(_world).Type;
                    SoundDataEntry soundData = _configuration.Value.Sounds.Sounds.FirstOrDefault(sound => sound.Event == type);
                    
                    if (soundData != null && soundData.Clips.Length > 0)
                        foreach (var clip in soundData.Clips)
                            if (clip != null)
                                _sceneData.Value.AudioSource.PlayOneShot(clip);
                }
                else
                    _sceneData.Value.AudioSource.Stop();

                playSound.Del<PlaySoundEvent>(_world);
            }
        }
    }
}