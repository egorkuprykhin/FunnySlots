using FSM;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using States;
using UnityEngine.UI;

namespace FunnySlots
{
    public class HudBackButtonSystem : IEcsInitSystem
    {
        [EcsUguiNamed("BackButton")] Button _backButton;

        private EcsCustomInject<StateMachine> _stateMachine;

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
            _stateMachine.Value.Enter<MenuState>();
        }
    }
}