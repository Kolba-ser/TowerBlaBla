using Leopotam.Ecs;
using UnityEngine;
using ECS.Movement.Components;
using ECS.Model.Components;
using ECS.Tags.Components;

namespace ECS.Movement.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, ModelComponent>.Exclude<DisabledTagComponent> _movableFilter = null;
        public void Run()
        {
            foreach (var item in _movableFilter)
            {
                var movableComponent = _movableFilter.Get1(item);
                ref var model = ref _movableFilter.Get2(item);

                ref var modelTransform = ref model.Transform;

                float speed = movableComponent.Speed;
                Vector3 direction = movableComponent.Direction;
                Move(direction, speed, ref modelTransform);
            }
        }
        private void Move (Vector3 direction, float speed, ref Transform model)
        {
            model.Translate(direction * speed * Time.deltaTime, Space.World);
        }
    }
}
