using UnityEngine;

namespace FunnySlots
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public Vector2Int FieldSize;
        public Vector2 CellSize;

        public Vector2 ExtraCells;
        
        public float CameraPadding;

        public int CellsOffsetToDestroyCard;

        public Vector2 CardMoveSpeed;

        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;

        public int TimeBeforeWinEventMs;

        [Header("Prefabs")]
        public CardView CardView;
        public CardWinFrameView CardWinFrameView;
        public ScoreView ScoreView;
        public MaskView MaskView;

        [Header("Cards")]
        public CardsData CardsData;

        public int MinCombinationX;
        public int MinCombinationY;
        public int CombinationScore;
    }
}