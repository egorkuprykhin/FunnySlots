using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

namespace FunnySlots
{
    public class FieldPositionsService
    {
        private Configuration _configuration;
        private SharedData _sharedData;

        private Vector2 _minFieldBorders;
        private Vector2 _MaxFieldBorders;

        public FieldPositionsService(Configuration configuration, SharedData sharedData)
        {
            _sharedData = sharedData;
            _configuration = configuration;
                
            InitFieldPositions();
        }

        public int CardPositionToEntity(Vector2 position)
        {
            return _sharedData.PositionsToCards[position];
        }

        public List<int> GetAllHorizontalCardsByCard(int entity)
        {
            List<int> combinationCards = new List<int>();
            
            var cardPos = _sharedData.CardsToPositions[entity];
            
            while (_sharedData.HasPosition(cardPos))
            {
                combinationCards.Add(_sharedData.PositionsToCards[cardPos]);
                cardPos.x += _configuration.CellSize.x;
            }

            return combinationCards.OrderBy( card => _sharedData.CardsToPositions[card].x).ToList();
        }

        public List<int> GetAllVerticalCardsByCard(int entity)
        {
            List<int> combinationCards = new List<int>();

            var cardPos = _sharedData.CardsToPositions[entity];

            while (_sharedData.HasPosition(cardPos))
            {
                combinationCards.Add(_sharedData.PositionsToCards[cardPos]);
                cardPos.y += _configuration.CellSize.y;
            }

            return combinationCards;
        }

        // public bool HasComb(int entity)
        // {
        // }
        //
        // public bool HasCombinationsHorizontal(int entity)
        // {
        //     
        // }
        //
        // public bool HasCombinationVertical(int entity)
        // {
        //     
        // }
        
        public bool PositionInsideField(Vector2 position)
        {
            bool positionInsideField = position.x >= _minFieldBorders.x && position.x <= _MaxFieldBorders.x && 
                                      position.y >= _minFieldBorders.y && position.y <= _MaxFieldBorders.y;
            return positionInsideField;
        }

        public Vector2 GetFieldPosition(int x, int y)
        {
            var fieldSize = _configuration.FieldSize;
            var cellSize = _configuration.CellSize;

            Vector2 position = new Vector2(
                cellSize.x * (x + 0.5f - 0.5f * fieldSize.x),
                cellSize.y * (y + 0.5f - 0.5f * fieldSize.y));

            return position;
        }

        private void InitFieldPositions()
        {
            var fieldSize = _configuration.FieldSize;
            var cellSize = _configuration.CellSize;

            var minXPos = cellSize.x * (0.5f - 0.5f * fieldSize.x);
            var maxXPos = cellSize.x * (fieldSize.x - 0.5f - 0.5f * fieldSize.x);

            var minYPos = cellSize.y * (0.5f - 0.5f * fieldSize.y);
            var maxYPos = cellSize.y * (fieldSize.y - 0.5f - 0.5f * fieldSize.y);

            _minFieldBorders = new Vector2(minXPos, minYPos);
            _MaxFieldBorders = new Vector2(maxXPos, maxYPos);
        }
    }
}