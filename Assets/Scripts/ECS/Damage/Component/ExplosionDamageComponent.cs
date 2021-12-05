using System;

namespace ECS.Damage.Component
{
    [Serializable]
    public struct ExplosionDamageComponent
    {
        public float Range;
        public float Damage;
    }
}
