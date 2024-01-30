using UnityEngine;

namespace FunnySlots
{
    public struct CardData : IComponent
    {
        public CardInitializeData InitialData;
        
        public Vector2 Position;
        public int Row;
        public bool IsMoving;
    }
}