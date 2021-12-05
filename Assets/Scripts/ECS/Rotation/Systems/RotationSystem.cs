using ECS.Model.Components;
using ECS.Rotation.Components;
using ECS.Tags.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Rotation.Systems
{
    public sealed class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, ModelComponent>
                                   .Exclude<DisabledTagComponent>
                                   _rotatableFilter = null;

        public void Run()
        {
            foreach (var item in _rotatableFilter)
            {
                var rotatable = _rotatableFilter.Get1(item);
                var model = _rotatableFilter.Get2(item);

                var speed = rotatable.Speed;
                var direction = rotatable.Direction;
                ref var modelTransform = ref model.Transform;

                ref var frozenX = ref rotatable.X;
                ref var frozenY = ref rotatable.Y;
                ref var frozenZ = ref rotatable.Z;

                if (direction == Vector3.zero) continue;
                

                Rotate(speed, direction, ref modelTransform,
                        frozenX, frozenY, frozenZ);
            }
        }

        private void Rotate(float speed, Vector3 direction, ref Transform model, bool x, bool y, bool z)
        {

            var targetRotation = Quaternion.LookRotation(direction);

            FreezeVectors(ref targetRotation, x, y, z);

            model.rotation = Quaternion.Lerp(model.rotation, targetRotation, speed * Time.deltaTime);
        }

        private void FreezeVectors(ref Quaternion direction, bool x, bool y, bool z)
        {
            if (x)
                direction.x = 0;
            if (y)
                direction.y = 0;
            if (z)
                direction.z = 0;
        }
    }
}
