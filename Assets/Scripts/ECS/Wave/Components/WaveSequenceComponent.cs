using System;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Wave.Components
{
    [Serializable]
    public struct WaveSequenceComponent
    {
        public float Delay;
        public List<WaveComponent> WavesInfo;
        [HideInInspector]
        public bool IsLaunched;
        [HideInInspector]
        public float LaunchProgress;
    }
}
