// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
// using UnityEngine;
//
// namespace FunnySlots
// {
//     public class StopCardsMovingAtTargetPositionSystem : IEcsRunSystem
//     {
//         private EcsFilterInject<Inc<CardPosition, TargetPositionY>> _targetPositionCardsFilter;
//         private EcsFilterInject<Inc<CardPosition>> _movingCardsFilter;
//
//         private EcsCustomInject<Configuration> _configuration;
//         private EcsWorldInject _world;
//         
//         public void Run(IEcsSystems systems)
//         {
//             foreach (var entity in _targetPositionCardsFilter.Value)
//             {
//                 var targetPositionY =  entity.Get<TargetPositionY>(_world).Value;
//                 
//                 ref var cardPosition = ref entity.Get<CardPosition>(_world);
//                 
//                 var positionX = entity.Get<CardPosition>(_world).Value.x;
//                 var positionY = entity.Get<CardPosition>(_world).Value.y;
//
//                 if (positionY <= targetPositionY)
//                 {
//                     cardPosition.Value = new Vector2(cardPosition.Value.x, targetPositionY);
//                     
//                     StopAllCardsInColumn(positionX);
//                     
//                     entity.Del<TargetPositionY>(_world);
//                 }
//             }
//         }
//
//         private void StopAllCardsInColumn(float targetPosX)
//         {
//             var epsilon = _configuration.Value.CellSize.x * 0.1f;
//             
//             foreach (int movingCardEntity in _movingCardsFilter.Value)
//             {
//                 var cardPosX = movingCardEntity.Get<CardPosition>(_world).Value.x;
//
//                 if (Mathf.Abs(cardPosX - targetPosX) <= epsilon)
//                 {
//                     movingCardEntity.Get<CardData>(_world).IsMoving = false;
//                 }
//             }
//         }
//     }
//     
// }