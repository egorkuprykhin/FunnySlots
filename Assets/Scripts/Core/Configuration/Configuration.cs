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

        public Vector2 CardMoveSpeed;

        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;

        public int WinDelayMs;
        
        public int CombinationScore;

        [Header("Prefabs")]
        public CardView CardView;
        public CardWinFrameView CardWinFrameView;
        public ScoresView ScoresView;
        public MaskView MaskView;
        public AudioSourceView AudioSourceView;
        
        [Header("Cards")]
        public CardsData CardsData;

        [Header("Sounds")]
        public SoundData Sounds;
    }
}