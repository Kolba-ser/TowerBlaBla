using ECS.Detonator.Event;
using ECS.Explosion.Component;
using ECS.Model.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Explosion.System
{
    public sealed class ExplosionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DetonateEvent, ExplosionComponent, ModelComponent> _explosionFilter = null;

        public void Run()
        {
            foreach (var item in _explosionFilter)
            {
                ref var explosionComponent = ref _explosionFilter.Get2(item);
                ref var modelComponent = ref _explosionFilter.Get3(item);

                ref var modelTransform = ref modelComponent.Transform;
                var explosionVFX = explosionComponent.ExplosionVFX;

                var effects = GameObject.Instantiate(explosionVFX);

                effects.transform.position = modelTransform.position;
            }
        }
    }
}
