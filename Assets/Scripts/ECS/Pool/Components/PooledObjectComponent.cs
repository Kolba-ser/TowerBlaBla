using ECS.Pool.Types;
using System;
using UnityEngine;

namespace ECS.Pool.Components
{
    [Serializable]
    public struct PooledObjectComponent
    {
        public GameObject Object;
        public PoolType Type;
    }
}
