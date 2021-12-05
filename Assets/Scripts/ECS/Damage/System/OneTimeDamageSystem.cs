using ECS.Block.Component;
using ECS.Damage.Component;
using ECS.Damage.Request;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using Leopotam.Ecs;
using Scripts.MonoB.References;

namespace ECS.Damage.System
{
    public sealed class OneTimeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<OneTimeDamageComponent, TargetComponent>
                        .Exclude<DisabledTagComponent, BlockDurationComponent>
                        _damageFilter = null;

        public void Run()
        {
            foreach (var item in _damageFilter)
            {
                ref var damageComponent = ref _damageFilter.Get1(item);
                ref var targetComponent = ref _damageFilter.Get2(item);
                ref var entity = ref _damageFilter.GetEntity(item);
                ref var target = ref targetComponent.TargetTransform; 

                var damage = damageComponent.Damage;
                var rate = damageComponent.Rate;

                if (target == null) continue;

                ref var targetEntity = ref target.GetComponent<EntityReference>().Entity;

                ApplyDamage(targetEntity, damage);

                ref var duration = ref entity.Get<BlockDurationComponent>();
                duration.Timer = rate;

            }
        }

        private void ApplyDamage(EcsEntity targetEntity, float damage)
        {
            ref var damageRequest = ref targetEntity.Get<GetDamageRequest>();
            damageRequest.Damage = damage;
        }
    }
}
