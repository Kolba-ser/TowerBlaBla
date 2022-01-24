using ECS.Initialization.Requests;
using ECS.Pool.Components;
using Leopotam.Ecs;
using Scripts.ForCheck;
using Scripts.MonoB.References;
using System;
using UnityEngine;

namespace ECS.Initialization.Systems
{
    public sealed class PooledObjectInitializationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializePooledObjectRequest> _requestFilter = null;

        public void Run()
        {
            foreach (var item in _requestFilter)
            {
                ref var entity = ref _requestFilter.GetEntity(item);
                ref var request = ref _requestFilter.Get1(item);

                ref var pooledObjectComponent = ref request.PooledObjectComponent;
                ref var gameObject = ref pooledObjectComponent.Object;

                if(TryInitializePooledObject(gameObject, ref pooledObjectComponent))
                    entity.Del<InitializePooledObjectRequest>();


            }
        }

        private bool TryInitializePooledObject(GameObject gameObject,
            ref PooledObjectComponent pooledObjectComponent)
        {

            if (gameObject.TryGetComponent(out EntityReference reference))
            {
                if (reference.Entity.IsNull()) return false;

                ref var entity = ref reference.Entity;
                ref var component = ref entity.Get<PooledObjectComponent>();

                component = pooledObjectComponent;
                return true;
            }

            return false;
        }
    }
}
