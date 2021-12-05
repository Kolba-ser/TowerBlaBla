using ECS.Wave.Components;
using ECS.Wave.Events;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace ECS.Checkers.Systems
{
    public sealed class CurrentWaveAccountingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWaveComponent> _waveFilter = null;

        public void Run()
        {

            foreach (var item in _waveFilter)
            {
                ref var currentWave = ref _waveFilter.Get1(item);
                ref var entity = ref _waveFilter.GetEntity(item);

                var numberOfEnemies = currentWave.Types.Count;
                
                if (numberOfEnemies == 0)
                {
                    entity.Get<WaveEndEvent>();
                }
            }
        }
    }
}
