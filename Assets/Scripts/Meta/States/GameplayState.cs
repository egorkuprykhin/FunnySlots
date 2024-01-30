using FSM;
using Scripts.Configuration;
using Services;
using VContainer;

namespace States
{
    public class GameplayState : IState
    {
        [Inject] private GlobalConfiguration _globalConfiguration;
        [Inject] private SceneLoaderService _sceneLoader;
        
        public async void Enter()
        {
            await _sceneLoader.LoadScene(_globalConfiguration.GameSceneName);
        }

        public void Exit()
        {
        }
    }
}