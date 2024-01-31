using States;
using UnityEngine;
using View;

namespace SceneData
{
    public class GameSceneData : MonoBehaviour
    {
        public MenuView MenuView;
        public PolicyScreenView PolicyScreenView;
        public Camera MenuCamera;
        public ColorPrepareService ColorPrepareService;
        public AudioSource BackSoundAudioSource;
    }
}