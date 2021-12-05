using ECS.Wave.Components;
using ECS.Wave.Events;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;

namespace ECS.Handlers.WaveRemoval
{
    public sealed class WaveRemovalHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WaveEndEvent, CurrentWaveComponent> _endWaveFilterFilter = null;
        private readonly EcsFilter<CurrentWaveComponent> _currentWaveFilter = null;
        private readonly EcsFilter<InitializedSequenceComponent> _waveSequenceFilter = null;
        private readonly EcsFilter<EndGameEvent> _eventFilter = null;

        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_endWaveFilterFilter))
                HandleCurrentWave();

            if (Checker.IsThereMoreEntitiesThanZero(_eventFilter))
            {
                HandleEndEvent();
            }

        }

        private void HandleCurrentWave()
        {
            foreach (var item in _endWaveFilterFilter)
            {
                ref var entity = ref _endWaveFilterFilter.GetEntity(item);
                entity.Del<CurrentWaveComponent>();
                Debug.Log("HandleCurrentWave");
            }
        }
        private void HandleEndEvent()
        {
            foreach (var item in _waveSequenceFilter)
            {
                ref var entity = ref _waveSequenceFilter.GetEntity(item);
                entity.Del<InitializedSequenceComponent>();
            }

            foreach (var item in _currentWaveFilter)
            {
                ref var entity = ref _currentWaveFilter.GetEntity(item);
                entity.Del<CurrentWaveComponent>();
            }
        }

    }
}
