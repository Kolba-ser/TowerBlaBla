using ECS.Block.Component;
using ECS.Dispenser.Systems;
using ECS.Pool.Types;
using ECS.Rotation.Components;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using ECS.Towers.MortarTower.Component;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Towers.MortarTower.System
{
    public sealed class MortarTowerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MortarTowerComponent, TargetComponent>
                        .Exclude<DisabledTagComponent, BlockDurationComponent>
                        _mortarTowerFilter = null;

        private readonly DispenseSystem _dispenser = null;

        public void Run()
        {
            foreach (var item in _mortarTowerFilter)
            {
                ref var mortarComponent = ref _mortarTowerFilter.Get1(item);
                ref var targetComponent = ref _mortarTowerFilter.Get2(item);
                ref var entity = ref _mortarTowerFilter.GetEntity(item);

                ref var target = ref targetComponent.TargetTransform;

                //ref var cannon = ref mortarComponent.Cannon;

                var rate = mortarComponent.Rate;
                var launchSpeed = mortarComponent.LaunchSpeed;
                var shotPoint = mortarComponent.ShotPoint.position;
                var projectileType = mortarComponent.ProjectileType;

                if (target == null) continue;

                var velocity = CalculateVelocity(target, launchSpeed, shotPoint);
                //SetCannonDirection(velocity, ref cannon);
                LaunchProjectile(projectileType, shotPoint, velocity);

                ref var duration = ref entity.Get<BlockDurationComponent>();
                duration.Timer = rate;
            }
        }

        private void LaunchProjectile(PoolType projectileType, Vector3 shotPoint, Vector3 velocity)
        {
            var gameObject = _dispenser.GetObject(projectileType);
            gameObject.transform.position = shotPoint;

            var rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.velocity = velocity;
        }

/*        private void SetCannonDirection(Vector3 direction, ref Transform cannon)
        {
            ref var entity = ref cannon.GetComponent<EntityReference>().Entity;

            ref var rotatableComponent = ref entity.Get<RotatableComponent>();

            rotatableComponent.Direction = direction;
        }*/

        private Vector3 CalculateVelocity(Transform target, float launchSpeed, Vector3 shotPoint)
        {

            Vector3 targetPoint = target.position;
            targetPoint.y = 0f;

            Vector2 dir;
            dir.x = targetPoint.x - shotPoint.x;
            dir.y = targetPoint.z - shotPoint.z;
            float x = dir.magnitude;
            float y = -shotPoint.y;
            dir /= x;

            float g = 9.81f;
            float s = launchSpeed;
            float s2 = s * s;

            float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
            Debug.Assert(r >= 0f, "Launch velocity insufficient for range!");

            float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
            float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
            float sinTheta = cosTheta * tanTheta;


            Vector3 velocity = new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y);

            return velocity;
        }
    }
}
