using ECS.Block.Component;
using ECS.Damage.Component;
using ECS.Damage.Request;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using Leopotam.Ecs;
using Scripts.MonoB.References;
using UnityEngine;

namespace ECS.Damage.System
{
    public sealed class MultipleDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MultipleDamageComponent, TargetComponent>
                        .Exclude<DisabledTagComponent, BlockDurationComponent>
                        _damageFilter = null;

        private const float MAX_INCREASE = 2f;

        public void Run()
        {
            foreach (var item in _damageFilter)
            {
                ref var damageComponent = ref _damageFilter.Get1(item);
                ref var targetComponent = ref _damageFilter.Get2(item);
                ref var entity = ref _damageFilter.GetEntity(item);

                ref var target = ref targetComponent.TargetTransform;

                ref var increase = ref damageComponent.Increase;
                var growthRate = damageComponent.GrowthRates;
                var damage = damageComponent.Damage;
                var rate = damageComponent.Rate;

                if (target == null) continue;


                var targetEntity = target.GetComponent<EntityReference>().Entity;

                increase += increase < MAX_INCREASE ? growthRate : 0;

                var calculatedDamage = damage * increase;

                ApplyDamage(targetEntity, calculatedDamage);

                ref var duration = ref entity.Get<BlockDurationComponent>();
                duration.Timer = rate;

            }
        }

        private void ApplyDamage(EcsEntity target, float damage)
        {
            ref var request = ref target.Get<GetDamageRequest>();
            request.Damage = damage;
            //Debug.Log(damage);
        }
    }
}
