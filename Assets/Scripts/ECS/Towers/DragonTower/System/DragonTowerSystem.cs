using ECS.Block.Component;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using ECS.Towers.DragonTower.Component;
using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.VFX;

namespace ECS.Towers.DragonTower.System
{
    public sealed class DragonTowerSystem : IEcsRunSystem
    {

        private readonly EcsFilter<DragonTowerComponent, TargetComponent>
                        .Exclude<DisabledTagComponent>
                        _dragonTowerFilter = null;
        public void Run()
        {
            foreach (var item in _dragonTowerFilter)
            {
                ref var towerComponent = ref _dragonTowerFilter.Get1(item);
                ref var targetComponent = ref _dragonTowerFilter.Get2(item);

                ref var isActive = ref towerComponent.IsActive;

                ref var flameVFX = ref towerComponent.FlameVFX;
                var target = targetComponent.TargetTransform;

                if (target == null)
                {
                    if (isActive)
                    {
                        DisableFlame(ref flameVFX);
                        isActive = false;
                    }
                    continue;
                }

                if (isActive == false)
                {
                    ActivateFlame(ref flameVFX);
                    isActive = true;
                }

            }
        }

        public void ActivateFlame(ref VisualEffect flameVFX)
        {
            flameVFX.Play();
        }
        public void DisableFlame(ref VisualEffect flameVFX)
        {
            flameVFX.Stop();
        }
    }
}
