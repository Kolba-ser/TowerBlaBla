using ECS.Pool.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Wave.Components
{
    [Serializable]
    public struct EnemyInfo
    {
        public PoolType EnemyType;
        public int Quantity;
    }

    [Serializable]
    public struct WaveComponent
    {
        public Transform SpawnPoint;
        public float SpawnDelay;
        public float WaveDelay;

        public List<EnemyInfo> EnemyData;
        public List<Transform> Path;

        [HideInInspector]
        public float SpawnProgress;
        [HideInInspector]
        public int IndexOfEnemyData;
    }

}
