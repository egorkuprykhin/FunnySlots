using UnityEngine;

namespace FunnySlots
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public Vector2Int FieldSize;
        public Vector2 CardsOffset;
        public float CameraPadding;
        public int MinRollingCardsCount;
        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;
        public CardView CardView;
    }
}