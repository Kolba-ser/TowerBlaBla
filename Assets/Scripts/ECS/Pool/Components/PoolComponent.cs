using ECS.Pool.Types;
using System;
using UnityEngine;

namespace ECS.Pool.Components
{
    [Serializable]
    public struct PoolComponent
    {
        public GameObject Prefab;
        public int Quantity;
        public PoolType Type;

    }
}
