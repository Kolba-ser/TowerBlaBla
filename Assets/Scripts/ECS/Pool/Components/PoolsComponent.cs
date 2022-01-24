using Scripts.ObjectsInfo;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Pool.Components
{
    [Serializable]
    public struct PoolsComponent
    {
        public Transform Transform;
        public List<PooledObjectInfo> Pools;
    }
}
