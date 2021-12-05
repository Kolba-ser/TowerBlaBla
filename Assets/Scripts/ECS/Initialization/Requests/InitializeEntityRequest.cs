using Scripts.MonoB.References;
using System;

namespace ECS.Initialization.Requests
{
    [Serializable]
    public struct InitializeEntityRequest
    {
        public EntityReference EntityReference;
    }
}
