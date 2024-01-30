using UnityEngine;

namespace Scripts.Configuration
{
    [CreateAssetMenu(menuName = "Config/Configuration", order = 0)]
    public class GlobalConfiguration : ScriptableObject
    {
        public string GameSceneName;
        public Color MenuColor;
    }
}