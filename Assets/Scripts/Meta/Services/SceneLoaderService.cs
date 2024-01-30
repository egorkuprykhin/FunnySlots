using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Services
{
    [UsedImplicitly]
    public class SceneLoaderService
    {
        public async UniTask LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            await SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
        }
        
        public async UniTask UnloadScene(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}