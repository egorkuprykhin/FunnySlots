using UnityEngine;

namespace FunnySlots
{
    public struct CardData : IComponent
    {
        public Vector2 Position;
        public CardEntry CardEntry;
        public int Row;
        public float Speed;
        public bool IsMoving;
    }
}