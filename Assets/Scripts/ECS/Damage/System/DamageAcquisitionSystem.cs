using ECS.Damage.Request;
using ECS.Health.Component;
using Leopotam.Ecs;

namespace ECS.Damage.System
{
    public sealed class DamageAcquisitionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<GetDamageRequest, HealthComponent> _damageFilter = null;
        

        public void Run()
        {
            foreach (var item in _damageFilter)
            {
                ref var entity = ref _damageFilter.GetEntity(item);

                ref var damageRequest = ref _damageFilter.Get1(item);
                ref var healthComponent = ref _damageFilter.Get2(item);

                ref var changableHealth = ref healthComponent.ChangableHealth;
                ref var damage = ref damageRequest.Damage;

                ApplyDamage(ref changableHealth, ref damage);

                entity.Del<GetDamageRequest>();
            }    
        }

        private void ApplyDamage(ref float health, ref float damage)
        {
            health -= damage;
        }
    }
}
