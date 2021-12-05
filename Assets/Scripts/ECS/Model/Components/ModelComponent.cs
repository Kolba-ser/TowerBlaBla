using UnityEngine;
using System;

namespace ECS.Model.Components
{
    [Serializable]
    public struct ModelComponent
    {
        public Transform Transform;
        public GameObject Object;
    }
}
