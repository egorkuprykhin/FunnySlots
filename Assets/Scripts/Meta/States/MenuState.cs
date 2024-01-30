using FSM;
using SceneData;
using UnityEngine;
using VContainer;
using View;

namespace States
{
    public class MenuState : IState
    {
        [Inject] private readonly StateMachine _stateMachine;
        
        [Inject] private readonly MenuView _menuView;
        [Inject] private GameSceneData _sceneData;

        [Inject] private readonly PolicyScreenView _policyScreenView;

        public void Enter()
        {
            _sceneData.MenuCamera.gameObject.SetActive(true);
            _menuView.Show();
            
            SubscribeMenuButtons();
            SubscribePolicyButton();
        }

        public void Exit()
        {
            _menuView.Hide();
            _sceneData.MenuCamera.gameObject.SetActive(false);
        }

        private void PlayGameButtonPressed()
        {
            _stateMachine.Enter<GameplayState>();
        }

        private void SettingsButtonPressed()
        {
        }

        private void ShowPolicyButtonPressed()
        {
            _policyScreenView.Show();
        }

        private void ExitButtonPressed()
        {
            Application.Quit();
        }

        private void PolicyBackToMenu()
        {
            _policyScreenView.Hide();
        }

        private void SubscribeMenuButtons()
        {
            _menuView.PlayButton.onClick.AddListener(PlayGameButtonPressed);
            _menuView.SettingsButton.onClick.AddListener(SettingsButtonPressed);
            _menuView.ShowPolicyButton.onClick.AddListener(ShowPolicyButtonPressed);
            _menuView.ExitButton.onClick.AddListener(ExitButtonPressed);
        }

        private void SubscribePolicyButton()
        {
            _policyScreenView.BackToMenuButton.onClick.AddListener(PolicyBackToMenu);
        }
    }
}