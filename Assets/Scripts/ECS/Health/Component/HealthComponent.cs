using System;
using UnityEngine;

namespace ECS.Health.Component
{
    [Serializable]
    public struct HealthComponent
    {
        public float Health;

        [HideInInspector]
        public float ChangableHealth;
        [HideInInspector]
        public bool IsInitialized;
    }
}
