using UnityEngine;
using UnityEngine.UI;

namespace Unity
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear ();
        }
    }
}