using ECS.Pool.Components;
using ECS.Pool.Types;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

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

                InitPools(ref poolsComponent, entity);
            }
        }

        private void InitPools(ref PoolsComponent poolsComponent, EcsEntity entity)
        {
            ref var initializedPools = ref entity.Get<InitializedPoolsComponent>();
            initializedPools.Pools = new List<InitializedPoolComponent>();


            GameObject empty = new GameObject();
            ref var pools = ref poolsComponent.Pools;

            foreach (var pool in pools)
            {

                var container = GameObject.Instantiate(empty, poolsComponent.Transform);
                container.name = pool.Type.ToString();

                var initializedPool = new InitializedPoolComponent();
                initializedPool.UnusedObjects = new Queue<GameObject>();
                initializedPool.Type = pool.Type;
                initializedPool.Parent = container.transform;

                AddObjectsToPool(container.transform, pool, ref initializedPool);

                initializedPools.Pools.Add(initializedPool);
            }

            GameObject.Destroy(empty);
        }
        private void AddObjectsToPool(Transform container, PoolComponent pool, ref InitializedPoolComponent initializedPool)
        {
            for (int i = 0; i < pool.Quantity; i++)
            {
                initializedPool.UnusedObjects.Enqueue(InstantiateObject(container, pool.Prefab));
            }
        }

        private GameObject InstantiateObject(Transform parent, GameObject prefab)
        {
            var gameObject = GameObject.Instantiate(prefab, parent);

            gameObject.SetActive(false);

            return gameObject;
        }
    }
}
