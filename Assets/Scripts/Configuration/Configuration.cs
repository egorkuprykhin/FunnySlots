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

        public int MaxOffsetToCreateCard;
        public int CellsOffsetToDestroyCard;

        public Vector2 CardMoveSpeed;

        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;


        [Header("Prefabs")]
        public CardView CardView;
        public GameObject MaskPrefab;

        [Header("Cards")]
        public CardsData CardsData;

        public float Epsilon;
    }
}