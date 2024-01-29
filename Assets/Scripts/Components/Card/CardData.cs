using UnityEngine;

namespace FunnySlots
{
    public struct CardData : IComponent
    {
        public CardInitializeData InitializeData;
        
        public Vector2 Position;
        public int Row;
        public bool IsMoving;
    }
}