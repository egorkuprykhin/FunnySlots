using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace FunnySlots
{
    public class CardPositionsService
    {
        private readonly Configuration _configuration;
        private readonly SharedData _sharedData;

        private Vector2 _minFieldBorders;
        private Vector2 _MaxFieldBorders;

        private Vector2Int _fieldSize => _configuration.FieldSize;
        private Vector2 _cellSize => _configuration.CellSize;
        private Vector2 _oneCellUpOffset => new(0, _cellSize.y);


        public CardPositionsService(Configuration configuration, SharedData sharedData)
        {
            _configuration = configuration;
            _sharedData = sharedData;

            InitFieldBorders();
        }
        
        public bool IsPositionBelowTopClippingDistance(Vector2 position) => 
            position.y < _configuration.ExtraCells.y * _cellSize.y;

        public bool IsPositionBelowBottomClippingDistance(Vector2 position) => 
            position.y < Mathf.Sign(-1) * (_configuration.ExtraCells.y * _cellSize.y);

        public bool IsPositionInsideField(Vector2 position) =>
            position.x >= _minFieldBorders.x && position.x <= _MaxFieldBorders.x && 
            position.y >= _minFieldBorders.y && position.y <= _MaxFieldBorders.y;

        public Vector2 GetPositionForCell(int x, int y) => 
            new(
                _cellSize.x * CellPosition(x, _fieldSize.x),
                _cellSize.y * CellPosition(y, _fieldSize.y));

        public Vector2 GetPositionForCellAbove(Vector2 position) => 
            position + _oneCellUpOffset;

        public Vector2 GetPositionOfNearBelowCell(Vector2 pos)
        {
            float minTargetY = Mathf.Sign(-1) * _cellSize.y * (1 + _fieldSize.y);
            float targetY = minTargetY;
            
            while (targetY < pos.y)
                targetY += _cellSize.y;
            targetY -= _cellSize.y;

            return new Vector2(pos.x, targetY);
        }

        private void InitFieldBorders()
        {
            var minXPos = _cellSize.x * (0.5f - 0.5f * _fieldSize.x);
            var maxXPos = _cellSize.x * (_fieldSize.x - 0.5f - 0.5f * _fieldSize.x);

            var minYPos = _cellSize.y * (0.5f - 0.5f * _fieldSize.y);
            var maxYPos = _cellSize.y * (_fieldSize.y - 0.5f - 0.5f * _fieldSize.y);
            
            _minFieldBorders = new Vector2(minXPos, minYPos);
            _MaxFieldBorders = new Vector2(maxXPos, maxYPos);
        }

        private float CellPosition(int p, int size) => p + 0.5f - 0.5f * size;


        [Obsolete]
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
    }
}