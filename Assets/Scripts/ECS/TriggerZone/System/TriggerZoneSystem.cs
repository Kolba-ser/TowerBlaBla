using ECS.Handlers.NullTarget.Event;
using ECS.Model.Components;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using ECS.TriggerZone.Component;
using Leopotam.Ecs;
using Scripts.MonoB.References;
using UnityEngine;

namespace ECS.TriggerZone.System
{
    public sealed class TriggerZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ZoneComponent, ModelComponent>
                        .Exclude<DisabledTagComponent>
                        _zoneFilter = null;

        private const int ENEMY_LMASK = 1 << 7;

        public void Run()
        {
            foreach (var item in _zoneFilter)
            {
                ref var zoneComponent = ref _zoneFilter.Get1(item);
                ref var entity = ref _zoneFilter.GetEntity(item);
                ref var modelComponent = ref _zoneFilter.Get2(item);

                ref var model = ref modelComponent.Transform;
                ref var target = ref zoneComponent.Target;
                ref var colliderSize = ref zoneComponent.SphereColliderSize;

                var zonePosition = model.position;
                var range = zoneComponent.Range;

                ref var targetComponent = ref entity.Get<TargetComponent>();


                if (target != null)
                {
                    if (IsTargetDead(target) ||
                        IsTargetOutOfTheZone(zonePosition, target.position, range, colliderSize))
                    {
                        target = null;
                        ResetParameters(ref targetComponent, ref colliderSize);
                        entity.Get<NullTargetEvent>();

                        continue;
                    }
                    if (targetComponent.TargetTransform == null)
                        SetParameters(ref targetComponent,ref target,ref colliderSize);

                    continue;
                }



                target = FindTarget(zonePosition, range, ref colliderSize);
            }
        }

        private void ResetParameters(ref TargetComponent targetComponent, ref float colliderSize)
        {
            targetComponent.TargetObject = null;
            targetComponent.TargetTransform = null;
            colliderSize = 0;
        }
        private void SetParameters(ref TargetComponent targetComponent, ref Transform target, ref float colliderSize)
        {
            targetComponent.TargetObject = target.gameObject;
            targetComponent.TargetTransform = target;

            colliderSize = target.GetComponent<SphereCollider>().radius * target.localScale.x;
        }

        private bool IsTargetOutOfTheZone(Vector3 zonePosition, Vector3 targetPosition, float range, float colliderSize)
        {
            if (Vector3.Distance(zonePosition, targetPosition) > range + colliderSize)
            {
                return true;
            }

            return false;
        }
        private bool IsTargetDead(Transform target)
        {
            var entity = target.GetComponent<EntityReference>().Entity;
            if (entity.Has<DisabledTagComponent>())
            {
                return true;
            }
            return false;
        }

        private Transform FindTarget(Vector3 zonePosition, float range, ref float colliderSize)
        {
            var colliders = Physics.OverlapSphere(zonePosition, range, ENEMY_LMASK);

            if (colliders.Length > 0)
            {
                var collider = colliders[0];
                return collider.transform;
            }

            return null;
        }
    }
}
