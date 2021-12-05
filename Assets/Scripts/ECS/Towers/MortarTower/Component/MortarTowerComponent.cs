using ECS.Pool.Types;
using System;
using UnityEngine;

namespace ECS.Towers.MortarTower.Component
{
    [Serializable]
    public struct MortarTowerComponent
    {
        public Transform ShotPoint;
        //public Transform Cannon;
        public PoolType ProjectileType;

        public float Rate;
        public float LaunchSpeed;

    }
}
