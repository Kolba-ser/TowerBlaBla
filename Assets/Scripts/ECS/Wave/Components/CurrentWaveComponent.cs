using ECS.Pool.Types;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Wave.Components
{
    internal struct CurrentWaveComponent
    {
        public Transform SpawnPoint;
        public float SpawnDelay;

        public Queue<PoolType> Types;
        public List<Transform> Path;

        [HideInInspector]
        public float SpawnProgress;
    }

}
