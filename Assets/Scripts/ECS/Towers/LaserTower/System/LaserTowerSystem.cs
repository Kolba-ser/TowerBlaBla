using ECS.Damage.Component;
using ECS.Damage.Request;
using ECS.Model.Components;
using ECS.Targeting.Components;
using ECS.Towers.LaserTower.Component;
using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Towers.LaserTower.System
{
    public sealed class LaserTowerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LaserTowerComponent, ModelComponent, TargetComponent> _towerFilter = null;

        public void Run()
        {
            foreach (var item in _towerFilter)
            {
                ref var towerComponent = ref _towerFilter.Get1(item);
                ref var modelComponent = ref _towerFilter.Get2(item);
                ref var targetComponent = ref _towerFilter.Get3(item);

                ref var isActive = ref towerComponent.IsActive;
                ref var effects = ref towerComponent.Effects;
                ref var laserBeam = ref towerComponent.LaserBeam;

                ref var target = ref targetComponent.TargetTransform;

                var startVFX = towerComponent.StartVFX;
                var endVFX = towerComponent.EndVFX;

                if (effects.Count == 0)
                {
                    InitializeEffects(ref effects, startVFX, endVFX);
                }

                if (target != null)
                {
                    if (isActive == false)
                    {
                        ActivateLaser(ref effects, ref laserBeam);
                        isActive = true;
                    }

                    var shotPosition = towerComponent.ShotPoint.position;
                    var targetPosition = targetComponent.TargetTransform.position;

                    SetEffectsPosition(shotPosition, targetPosition, laserBeam, startVFX, endVFX);

                    continue;
                }

                if (isActive)
                {
                    DisableLaser(ref effects, ref laserBeam);
                    isActive = false;
                }
            }

        }

        private void InitializeEffects(ref List<ParticleSystem> effects, GameObject startVFX, GameObject endVFX)
        {
            effects = new List<ParticleSystem>();

            Action<List<ParticleSystem>, GameObject>
                AddEffectsTo = (List<ParticleSystem> effects, GameObject effect) =>
            {
                for (int i = 0; i < effect.transform.childCount; i++)
                {
                    ParticleSystem particleSystem =
                      effect.transform.GetChild(i).GetComponent<ParticleSystem>();

                    if (particleSystem != null)
                    {
                        effects.Add(particleSystem);
                    }
                }
            };

            AddEffectsTo(effects, startVFX);
            AddEffectsTo(effects, endVFX);


        }

        private void ActivateLaser(ref List<ParticleSystem> effects, ref LineRenderer beam)
        {

            beam.enabled = true;
            foreach (var effect in effects)
            {
                effect.Play();
            }
        }
        private void DisableLaser(ref List<ParticleSystem> effects, ref LineRenderer beam)
        {
            beam.enabled = false;
            foreach (var effect in effects)
            {
                effect.Stop();
            }
        }

        private void SetEffectsPosition(Vector3 shotPosition, Vector3 targetPosition, LineRenderer beam, GameObject startVFX, GameObject endVFX)
        {
            beam.SetPosition(0, shotPosition);
            beam.SetPosition(1, targetPosition);

            startVFX.transform.position = shotPosition;
            endVFX.transform.position = shotPosition;

        }

    }
}
