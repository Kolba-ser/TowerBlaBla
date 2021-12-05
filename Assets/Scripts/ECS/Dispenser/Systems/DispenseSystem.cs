using ECS.Pool.Components;
using ECS.Pool.Types;
using ECS.Tags.Components;
using Leopotam.Ecs;
using Scripts.MonoB.References;
using System;
using UnityEngine;

namespace ECS.Dispenser.Systems
{
    public sealed class DispenseSystem : IEcsSystem
    {
        private readonly EcsFilter<InitializedPoolsComponent> _poolsFilter = null;


        public GameObject GetObject(PoolType type)
        {
            foreach (var item in _poolsFilter)
            {
                ref var pools = ref _poolsFilter.Get1(item).Pools;
                var pool = pools.Find(x => x.Type == type);

                if (pools.Contains(pool) == false)
                    throw new Exception($"Pools do not contain {type} type");

                ref var unusedObjects = ref pool.UnusedObjects;

                var gameObject = unusedObjects.Dequeue(); 
                   
                var entity = gameObject.GetComponent<EntityReference>().Entity;

                if (entity.IsNull() == false
                    && entity.Has<DisabledTagComponent>()) 
                {
                    entity.Del<DisabledTagComponent>();
                }

                gameObject.SetActive(true);
                return gameObject;
            }

            return null;

        }

        public void RemoveObject(PooledObjectComponent pooledObject)
        {

            foreach (var item in _poolsFilter)
            {
                ref var poolsComponent = ref _poolsFilter.Get1(item);
                var pools = poolsComponent.Pools;

                var gameObject = pooledObject.Object;
                var entity = gameObject.GetComponent<EntityReference>().Entity;

                if (entity.IsNull())
                    throw new Exception("You forgot to hang up" +
                                        " the components for the" +
                                        $" entity reference on the {gameObject.name}");


                entity.Get<DisabledTagComponent>();

                var targetPool = pools.Find(x => x.Type == pooledObject.Type);
                gameObject.SetActive(false);
                targetPool.UnusedObjects.Enqueue(gameObject);
            }
        }


        
    }
}
