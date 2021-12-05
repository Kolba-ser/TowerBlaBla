using UnityEngine;
using System;

namespace ECS.Movement.Components
{
    [Serializable]
    public struct MovableComponent
    {
        public float Speed;

        [HideInInspector]
        public Vector3 Direction;
    }
}
