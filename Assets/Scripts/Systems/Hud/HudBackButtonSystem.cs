using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudBackButtonSystem : IEcsInitSystem
    {
        [EcsUguiNamed("BackButton")] Button _backButton;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            _backButton.onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            BackToMenu();
        }

        private void BackToMenu()
        {
            Debug.Log("Button Back To Menu pressed!");
        }
    }
}