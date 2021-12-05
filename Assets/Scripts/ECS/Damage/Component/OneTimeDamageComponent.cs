using System;

namespace ECS.Damage.Component
{
    [Serializable]
    public struct OneTimeDamageComponent
    {
        public float Damage;
        public float Rate;
    }
}
