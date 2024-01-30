using System.Collections.Generic;
using Scripts.Configuration;
using UnityEngine;
using VContainer;

namespace States
{
    public class ColorPrepareService : MonoBehaviour
    {
        public List<ColorSourceGraphic> graphics;

        [Inject] private GlobalConfiguration _globalConfiguration;
        
        public void PrepareColors()
        {
            foreach (var graphic in graphics) 
                graphic.SetColor(_globalConfiguration.MenuColor);
        }
    }
}