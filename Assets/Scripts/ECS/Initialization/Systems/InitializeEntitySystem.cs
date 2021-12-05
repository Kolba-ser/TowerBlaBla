using Leopotam.Ecs;
using ECS.Initialization.Requests;
using UnityEngine;

namespace ECS.Initialization.System
{
    public sealed class InitializeEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeEntityRequest> _requestsFilter = null;

        public void Run()
        {
            foreach (var item in _requestsFilter)
            {

                ref var entityRequest = ref _requestsFilter.Get1(item);
                ref var entity = ref _requestsFilter.GetEntity(item);

                entityRequest.EntityReference.Entity = entity;
                entity.Del<InitializeEntityRequest>();
            }
        }

    }
}
