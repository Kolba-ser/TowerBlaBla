using ECS.Damage.Component;
using ECS.Handlers.NullTarget.Event;
using Leopotam.Ecs;
using Scripts.ForCheck;
using System;

namespace ECS.Handlers.NullTarget
{
    public sealed class NullTargetHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<NullTargetEvent, MultipleDamageComponent> _miltipleDamageFilter = null;
        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_miltipleDamageFilter))
                HandleMultipleDamage();
        }

        private void HandleMultipleDamage()
        {
            foreach (var item in _miltipleDamageFilter)
            {
                ref var damageComponent = ref _miltipleDamageFilter.Get2(item);
                damageComponent.Increase = 0;
            }
        }

        
    }
}
