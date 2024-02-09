using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CleanFinishedSoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PlayingSound>> _playingSounds;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int playingSoundEntity in _playingSounds.Value)
            {
                ref PlayingSound playingSound = ref playingSoundEntity.Get<PlayingSound>();
                playingSound.ElapsedTime += Time.deltaTime;

                if (SoundFinished(playingSound)) 
                    CleanSound(playingSoundEntity, playingSound);
            }
        }

        private void CleanSound(int playingSoundEntity, PlayingSound playingSound)
        {
            playingSound.AudioSourceView.AudioSource.Stop();
            
            Object.Destroy(playingSound.AudioSourceView.gameObject);
            
            playingSoundEntity.Delete<PlayingSound>();
        }

        private static bool SoundFinished(PlayingSound playingSound) => 
            playingSound.ElapsedTime >= playingSound.LifeTime;
    }
}