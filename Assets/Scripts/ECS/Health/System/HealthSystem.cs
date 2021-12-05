using ECS.Despawn.Request;
using ECS.Health.Component;
using ECS.Tags.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Health.System
{
    public sealed class HealthSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthComponent>
            .Exclude<DisabledTagComponent> 
            _healthFilter = null;

        public void Run()
        {
            foreach (var item in _healthFilter)
            {
                ref var entity = ref _healthFilter.GetEntity(item);

                ref var healthComponent = ref _healthFilter.Get1(item);
                ref var changableHealth = ref healthComponent.ChangableHealth;
                ref var isInitialized = ref healthComponent.IsInitialized;

                var health = healthComponent.Health;

                if(isInitialized == false)
                {
                    changableHealth = health;
                    isInitialized = true;
                } 

                if (IsDead(changableHealth))
                {
                    entity.Get<DespawnRequest>();
                }
            }
        }

        private bool IsDead(float health)
        {
            if(health < 0)
            {
                return true;
            }

            return false;
        }
    }
}
