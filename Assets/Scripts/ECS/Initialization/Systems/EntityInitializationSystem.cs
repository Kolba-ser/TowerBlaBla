using Leopotam.Ecs;
using ECS.Initialization.Requests;
using UnityEngine;
using Scripts.MonoB.References;
using Voody.UniLeo;
using Scripts.ForCheck;

namespace ECS.Initialization.System
{
    public sealed class EntityInitializationSystem : IEcsRunSystem
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

        public void TryInitializeEntity(GameObject gameObject)
        {
            if(gameObject.TryGetComponent(out EntityReference reference))
            {
                reference.Entity = WorldHandler.GetWorld().NewEntity();
            } 

            Checker.CheckEntityAsGameObject(gameObject);
        }

    }
}
