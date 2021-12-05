using System;
using UnityEngine;

namespace ECS.Damage.Component
{
    [Serializable]
    public struct MultipleDamageComponent
    {
        public float Damage;
        public float Rate;

        public float GrowthRates;
        [HideInInspector]
        public float Increase;

    }
}
