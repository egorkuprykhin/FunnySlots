using UnityEngine;

namespace FunnySlots
{
    public class CoreView : MonoBehaviour, ICoreView
    {
        public GameObject GameObject => gameObject;
    }
}