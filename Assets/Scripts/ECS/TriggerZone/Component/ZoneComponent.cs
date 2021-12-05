using System;
using UnityEngine;

namespace ECS.TriggerZone.Component
{
    [Serializable]
    public struct ZoneComponent
    {
        [Range(1f, 10f)]
        public float Range;

        [HideInInspector]
        public float SphereColliderSize;

        [HideInInspector]
        public Transform Target;
    }
}
