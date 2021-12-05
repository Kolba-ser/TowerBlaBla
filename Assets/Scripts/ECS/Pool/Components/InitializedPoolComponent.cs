using ECS.Pool.Types;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Pool.Components
{
    public class InitializedPoolComponent
    {
        public PoolType Type;
        public Transform Parent;
        public Queue<GameObject> UnusedObjects; 
    }
}
