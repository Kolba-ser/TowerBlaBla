using Leopotam.Ecs;
using ECS.Wave.Components;
using System.Collections.Generic;
using UnityEngine;
using Scripts.ForCheck;

namespace ECS.Wave.Systems
{
    public sealed class WavePreparationSystem : IEcsInitSystem
    {
        private readonly EcsFilter<WaveSequenceComponent> _sequenceFilter = null;

        public void Init()
        {
            int quantity = _sequenceFilter.GetEntitiesCount();
            Checker.CheckAsExclusive<WaveSequenceComponent>(_sequenceFilter);

            foreach (var item in _sequenceFilter)
            {
                ref var sequence = ref _sequenceFilter.Get1(item);
                ref var entity = ref _sequenceFilter.GetEntity(item);
                ref var progress = ref sequence.LaunchProgress;


                ref var initializedSequence = ref entity.Get<InitializedSequenceComponent>();
                initializedSequence.Waves = new Queue<WaveComponent>();

                InitializeSequence(ref sequence, ref initializedSequence);
                entity.Del<WaveSequenceComponent>();


                progress += Time.deltaTime;
            }
        }

        private void InitializeSequence(ref WaveSequenceComponent sequence, ref InitializedSequenceComponent waves)
        {
            foreach (var wave in sequence.WavesInfo)
            {
                waves.Waves.Enqueue(wave);
            }
        }
    }
}
