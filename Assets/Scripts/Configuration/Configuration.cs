using UnityEngine;

namespace FunnySlots
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public Vector2Int FieldSize;
        public Vector2 CellSize;
        
        public float CameraPadding;

        public Vector2 CardMoveSpeed;
        public int MinCardsInRow;

        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;


        [Header("Prefabs")]
        public CardView CardView;
    }
}