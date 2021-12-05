using ECS.Despawn.Request;
using ECS.Health.Component;
using ECS.Path.Components;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;

namespace ECS.Handlers.Despawn
{
    public sealed class DespawnHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DespawnRequest, HealthComponent> _healthFilter = null;
        private readonly EcsFilter<DespawnRequest, PathComponent> _pathFilter = null;

        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_healthFilter)) HandleHealth();

            if (Checker.IsThereMoreEntitiesThanZero(_pathFilter)) HandlePath();
        }

        private void HandlePath()
        {
            foreach (var item in _pathFilter)
            {
                ref var pathComponent = ref _pathFilter.Get2(item);

                pathComponent.PointIndex = 0;
                pathComponent.CurrentPoint = null;
            }
        }
        private void HandleHealth()
        {
            foreach (var item in _healthFilter)
            {
                ref var healthComponent = ref _healthFilter.Get2(item);

                ref var changableHealth = ref healthComponent.ChangableHealth;
                var health = healthComponent.Health;

                changableHealth = health;
            }
        }

    }
}
