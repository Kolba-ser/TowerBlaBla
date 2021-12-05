using UnityEngine;
using System;

namespace ECS.Rotation.Components
{
    [Serializable]
    public struct RotatableComponent
    {
        public float Speed;

        [Header("Frozen Vectors")]
        public bool X;
        public bool Y;
        public bool Z;

        [HideInInspector]
        public Vector3 Direction;
    }
}
