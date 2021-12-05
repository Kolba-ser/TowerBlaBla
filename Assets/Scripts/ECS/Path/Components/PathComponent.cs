using System.Collections.Generic;
using UnityEngine;

namespace ECS.Path.Components
{
    public struct PathComponent
    {
        public List<Transform> Path;
        public int PointIndex;
        public Transform CurrentPoint;
    }
}
