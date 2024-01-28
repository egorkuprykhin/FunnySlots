using UnityEngine;

namespace FunnySlots
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public Vector2Int FieldSize;
        public Vector2 CellSize;
        
        public float CameraPadding;

        public int MaxOffsetToCreateCard;
        public int MinOffsetToDestroyCard;

        public Vector2 CardMoveSpeed;

        public Vector2 RollingTimeRange;
        public Vector2 StoppingTimeRange;


        [Header("Prefabs")]
        public CardView CardView;

        [Header("Cards")]
        public CardsData CardsData;
    }
}