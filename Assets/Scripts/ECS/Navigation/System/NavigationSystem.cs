using ECS.Despawn.Request;
using ECS.Handlers.EndPoint.Event;
using ECS.Model.Components;
using ECS.Movement.Components;
using ECS.Path.Components;
using ECS.Rotation.Components;
using ECS.Tags.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Navigation.System
{
    public sealed class NavigationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PathComponent, MovableComponent,
                                   RotatableComponent, ModelComponent,
                                   EnemyTagComponent>.Exclude<DisabledTagComponent>
                                   _enemyFilter = null;

        private const float MESUREMENT_ERROR = 0.4f;

        public void Run()
        {
            foreach (var item in _enemyFilter)
            {
                ref var pathComponent = ref _enemyFilter.Get1(item);
                ref var movableComponent = ref _enemyFilter.Get2(item);
                ref var rotatableComponent = ref _enemyFilter.Get3(item);
                ref var modelComponent = ref _enemyFilter.Get4(item);
                ref var entity = ref _enemyFilter.GetEntity(item);

                var currentPosition = modelComponent.Transform.position;

                ref var lookDirection = ref rotatableComponent.Direction;
                ref var moveDirection = ref movableComponent.Direction;

                if (CanNavigate(ref pathComponent, currentPosition, ref entity) == false) continue;

                var targetPosition = pathComponent.CurrentPoint.position;
                var targetDirection = targetPosition - currentPosition;
                targetDirection = targetDirection.normalized;
                lookDirection = targetDirection;
                moveDirection = targetDirection;

            }
        }

        private bool CanNavigate(ref PathComponent pathComponent, Vector3 currentPosition, ref EcsEntity entity)
        {
            ref var pointIndex = ref pathComponent.PointIndex;
            var pathlenght = pathComponent.Path.Count;

            ref var targetPoint = ref pathComponent.CurrentPoint;

            if (targetPoint == null)
            {
                targetPoint = pathComponent.Path[pointIndex];
                return true;
            }

            var targetPosition = targetPoint.position;

            if (HasReachedTheDestination(currentPosition, targetPosition))
            {
                pointIndex++;

                if (pointIndex > pathlenght - 1)
                {
                    entity.Get<EndpointReachedEvent>();
                    return false;
                }

                pathComponent.CurrentPoint = pathComponent.Path[pointIndex];
            }

            return true;
        }

        private bool HasReachedTheDestination(Vector3 currentPosition, Vector3 targetPosition)
        {
            if (Vector3.Distance(currentPosition, targetPosition) <= MESUREMENT_ERROR)
            {
                return true;
            }
            return false;
        }
    }
}
