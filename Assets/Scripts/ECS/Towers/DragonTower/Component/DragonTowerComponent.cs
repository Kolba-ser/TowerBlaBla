using System;
using UnityEngine;
using UnityEngine.VFX;

namespace ECS.Towers.DragonTower.Component
{
    [Serializable]
    public struct DragonTowerComponent
    {
        public Transform ShotPoint;
        public VisualEffect FlameVFX;

        [HideInInspector]
        public bool IsActive;
    }
}
