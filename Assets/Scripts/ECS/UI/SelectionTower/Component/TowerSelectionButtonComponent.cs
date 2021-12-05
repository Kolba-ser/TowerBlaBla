using ECS.Pool.Types;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ECS.UI.SelectionTower.Component
{
    [Serializable]
    public struct TowerSelectionButtonComponent 
    {
        public int Cost;
        public PoolType TowerType;
        public Button Button;
    }
}
