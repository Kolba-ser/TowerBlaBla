using ECS.Pool.Types;
using ECS.Wave.Components;
using ECS.Wave.Events;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Wave.Systems
{
    public sealed class WaveLauncherSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializedSequenceComponent>
                          _sequenceFilter = null;

        public void Run()
        {
            foreach (var item in _sequenceFilter)
            {
                ref var sequence = ref _sequenceFilter.Get1(item);
                ref var entity = ref _sequenceFilter.GetEntity(item);

                ref var waves = ref sequence.Waves;
                ref var waveIndex = ref sequence.WaveIndex;
                ref var launchProgress = ref sequence.LaunchProgress;
                ref var currentDelay = ref sequence.CurrentDelay;

                if (waves.Count > 0)
                {
                    var presentWave = waves.Peek();
                    var delay = presentWave.WaveDelay;

                    if (delay != currentDelay)
                        currentDelay = delay;


                    if (launchProgress >= delay)
                    {
                        var otherEntity = WorldHandler.GetWorld().NewEntity();

                        ref var currentWave = ref otherEntity.Get<CurrentWaveComponent>();
                        var wave = waves.Dequeue();
                        sequence.WaveIndex++;

                        PrepareCurrentWave(ref currentWave, wave);
                        launchProgress = 0;
                    }

                    launchProgress += Time.deltaTime;
                }
                else
                {
                    entity.Get<WaveEndEvent>();
                }


            }
        }

        private void PrepareCurrentWave(ref CurrentWaveComponent currentWave, WaveComponent wave)
        {
            currentWave.Types = new Queue<PoolType>();
            currentWave.SpawnPoint = wave.SpawnPoint;
            currentWave.SpawnDelay = wave.SpawnDelay;
            currentWave.Path = wave.Path;


            foreach (var data in wave.EnemyData)
            {
                for (int i = 0; i < data.Quantity; i++)
                {
                    currentWave.Types.Enqueue(data.EnemyType);
                }
            }
        }
    }
}
