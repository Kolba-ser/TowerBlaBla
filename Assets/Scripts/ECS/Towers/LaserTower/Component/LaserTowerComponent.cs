using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Towers.LaserTower.Component
{
    [Serializable]
    public struct LaserTowerComponent
    {
        public Transform ShotPoint;        
        public GameObject StartVFX;
        public GameObject EndVFX;
        public LineRenderer LaserBeam;

        [HideInInspector]
        public List<ParticleSystem> Effects;
        [HideInInspector]
        public bool IsActive;

    }
}
