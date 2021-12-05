using Leopotam.Ecs;
using System;
using ECS.Wave.Components;
using UnityEngine;
using ECS.Dispenser.Systems;
using ECS.Wave.Events;
using ECS.Block.Component;

namespace ECS.Spawn.Systems
{
    public sealed class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWaveComponent>
                        .Exclude<BlockDurationComponent>
                        _currentWaveFilter = null;

        private readonly DispenseSystem _dispenser;


        public void Run()
        {
            foreach (var item in _currentWaveFilter)
            {
                ref var currentWave = ref _currentWaveFilter.Get1(item);
                ref var entity = ref _currentWaveFilter.GetEntity(item);

                ref var progress = ref currentWave.SpawnProgress;
                var delay = currentWave.SpawnDelay;

                Spawn(ref currentWave);
           
                ref var durarion = ref entity.Get<BlockDurationComponent>();
                durarion.Timer = delay;
            }
        }

        private void Spawn(ref CurrentWaveComponent currentWave)
        {
            var type = currentWave.Types.Dequeue();
            var gameObject = _dispenser.GetObject(type);
            gameObject.transform.position = currentWave.SpawnPoint.position;
        }
    }
}
