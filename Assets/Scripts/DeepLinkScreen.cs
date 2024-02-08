using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FunnySlots
{
    public class DeepLinkScreen : MonoBehaviour
    {
        // AppsFlyer object - the only communication with AppsFlyer
        [SerializeField] private AppsFlyerManager appsFlyerManager;
        
        public TMP_Text DeepLinkParamsText;
        public Button BackButton;
        public Canvas Canvas;

        private void Awake()
        {
            BackButton.onClick.AddListener(Hide);
            appsFlyerManager.DeepLinkReceived += Show;
        }

        private void OnDestroy()
        {
            appsFlyerManager.DeepLinkReceived -= Show;
        }

        private void Show()
        {
            Canvas.enabled = true;

            Dictionary<string, object> deepLinkParams = appsFlyerManager.DeepLinkParams;
            string text = "No bonuses have received yet. You can check again in a bit.";
            if (deepLinkParams != null)
            {
                string[] headlines = { "Start level", "Extra butterflies", "Extra points", "Referrer name"};
                if (deepLinkParams.ContainsKey("deep_link_error"))
                {
                    text = "Bonuses Loading Error.";
                }
                else if (deepLinkParams.ContainsKey("deep_link_not_found"))
                {
                    text = "Can Not Find Bonuses.";
                }
                else
                {
                    int i = 0;
                    text = "";
                    foreach (KeyValuePair<string, object> entry in deepLinkParams)
                    {
                        if (i < deepLinkParams.Count)
                        {
                            text += headlines[i] + ": ";
                            if (entry.Value != null)
                            {
                                text += entry.Value.ToString() + '\n';
                            }
                            else
                            {
                                text += "null\n";
                            }
                            i++;
                        }
                    }
                    if (i == 0)
                    {
                        text = "Deep Link Received With No Bonuses.";
                    }
                }
            }
            DeepLinkParamsText.text = text;
        }

        private void Hide()
        {
            Canvas.enabled = false;
        }
    }
}