using System.Collections.Generic;

namespace ECS.Wave.Components
{
    public struct InitializedSequenceComponent
    {
        public Queue<WaveComponent> Waves;

        public int WaveIndex;
        public float LaunchProgress;
        public float CurrentDelay;
    }
}
