using UnityEngine;

namespace View
{
    public class CanvasView : MonoBehaviour
    {
        public Canvas Canvas;
        
        public void Show()
        {
            Canvas.enabled = true;
        }

        public void Hide()
        {
            Canvas.enabled = false;
        }
    }
}