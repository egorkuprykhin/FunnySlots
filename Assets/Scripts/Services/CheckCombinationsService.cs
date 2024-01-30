using UnityEngine;

namespace FunnySlots
{
    public class CheckCombinationsService
    {
        private Configuration _configuration;

        public CheckCombinationsService(Configuration configuration)
        {
            _configuration = configuration;
        }

        public void CheckPositionInsideField(Vector2 pos)
        {
            Vector2 checkRanges = new Vector2(
                0.11f + _configuration.FieldSize.x * _configuration.CellSize.x,
                0.11f + _configuration.FieldSize.y * _configuration.CellSize.y);

            bool xCheck =
                pos.x <= checkRanges.x || pos.x > -checkRanges.x;
            
            bool yCheck =
                pos.y <= checkRanges.y || pos.y > -checkRanges.y;
        }
    }
}