using System;
using UnityEngine;

namespace ECS.Skipping.Component
{
    [Serializable]
    public struct PassesNumberComponent
    {
        public int NumberOfPasses;
        
        [HideInInspector]
        public int ChangableNumberOfPasses;
    }
}
