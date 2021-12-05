using ECS.Path.Components;
using ECS.Tags.Components;
using ECS.Wave.Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Path.System
{
    public sealed class PathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CurrentWaveComponent> _waveFilter = null;

        private readonly EcsFilter<PathComponent>
                         .Exclude<DisabledTagComponent>
                         _pathFilter = null;
        public void Run()
        {   
            foreach (var item in _waveFilter)
            {
                var currentWave = _waveFilter.Get1(item);
                var path = currentWave.Path;

                SetPath(path);
            }
        }

        private void SetPath(List<Transform> path)
        {
            foreach (var item in _pathFilter)
            {
                ref var pathComponent = ref _pathFilter.Get1(item);

                pathComponent.Path = path;
            }
        }
    }
}
