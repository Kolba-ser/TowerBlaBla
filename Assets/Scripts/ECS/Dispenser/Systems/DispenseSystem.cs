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



                ActivateObject(gameObject, ref entity);
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

                var targetPool = pools.Find(x => x.Type == pooledObject.Type);
                Debug.Log(targetPool.Type);

                DeactivateObject(gameObject, ref entity);

                targetPool.UnusedObjects.Enqueue(gameObject);
            }
        }

        private void ActivateObject(GameObject gameObject, ref EcsEntity entity)
        {
            if (entity.IsNull() == false
                    && entity.Has<DisabledTagComponent>())
            {
                entity.Del<DisabledTagComponent>();
            }

            gameObject.SetActive(true);
        }
        private void DeactivateObject(GameObject gameObject, ref EcsEntity entity)
        {
            gameObject.SetActive(false);
            entity.Get<DisabledTagComponent>();
        }


    }
}
