using System.Collections.Generic;
using UnityEngine;

namespace FunnySlots
{
    public class SharedData
    {
        public Dictionary<int, Vector2> CardsToPositions = new Dictionary<int, Vector2>();
        public Dictionary<Vector2, int> PositionsToCards = new Dictionary<Vector2, int>();

        public bool RollingState { get; private set; }

        public bool HasCard(int entity) => 
            CardsToPositions.ContainsKey(entity);

        public bool HasPosition(Vector2 pos) => 
            PositionsToCards.ContainsKey(pos);

        public void SetRollingState() => 
            RollingState = true;

        public void ResetRollingState() => 
            RollingState = false;
    }
}