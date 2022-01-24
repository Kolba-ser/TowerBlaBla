using ECS.Initialization.Requests;
using ECS.Pool.Components;
using ECS.Pool.Types;
using Leopotam.Ecs;
using Scripts.ObjectsInfo;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Pool.Systems
{
    public sealed class PoolSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PoolsComponent> _poolsFilter = null;

        public void Init()
        {

            foreach (var item in _poolsFilter)
            {
                ref var poolsComponent = ref _poolsFilter.Get1(item);
                ref var entity = ref _poolsFilter.GetEntity(item);

                InitializePools(ref poolsComponent, entity);
            }
        }

        private void InitializePools(ref PoolsComponent poolsComponent, EcsEntity entity)
        {
            ref var initializedPools = ref entity.Get<InitializedPoolsComponent>();
            initializedPools.Pools = new List<InitializedPoolComponent>();

            GameObject empty = new GameObject();
            ref var pools = ref poolsComponent.Pools;

            foreach (var pool in pools)
            {
                var container = GameObject.Instantiate(empty, poolsComponent.Transform);
                container.name = pool.Type.ToString();

                var initializedPool =
                    InitializeNewPool(pool.Type, container.transform);

                AddObjectsToPool(container.transform, pool, ref initializedPool);

                initializedPools.Pools.Add(initializedPool);
            }

            GameObject.Destroy(empty);
        }
        private void AddObjectsToPool(Transform container, PooledObjectInfo pool, ref InitializedPoolComponent initializedPool)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                var gameObject = InstantiateObject(container, pool.Prefab, pool.Type);
                initializedPool
                    .UnusedObjects
                    .Enqueue(gameObject);

                SendRequest(gameObject, pool.Type);
            }
        }

        private GameObject InstantiateObject(Transform parent, GameObject prefab, PoolType type)
        {
            var gameObject = GameObject.Instantiate(prefab, parent);

            //InitializePooledObject(gameObject, type);

            DeactivateObject(gameObject);

            return gameObject;
        }

        private void SendRequest(GameObject gameObject, PoolType type)
        {
            var entity = WorldHandler.GetWorld().NewEntity();
            ref var request = ref entity.Get<InitializePooledObjectRequest>();

            var pooledObjectComponent = new PooledObjectComponent();
            pooledObjectComponent.Object = gameObject;
            pooledObjectComponent.Type = type;

            request.PooledObjectComponent = pooledObjectComponent;
        }

        private InitializedPoolComponent InitializeNewPool(PoolType type, Transform container)
        {
            var initializedPool = new InitializedPoolComponent();
            initializedPool.UnusedObjects = new Queue<GameObject>();
            initializedPool.Type = type;
            initializedPool.Parent = container;
            return initializedPool;
        }

        private void DeactivateObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
