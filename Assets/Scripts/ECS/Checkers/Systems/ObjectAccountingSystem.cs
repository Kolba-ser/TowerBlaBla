using ECS.Pool.Components;
using ECS.Pool.Types;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Checkers.Systems
{
    public class ObjectAccountingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<PoolsComponent> _poolsFilter = null;
        private readonly EcsFilter<InitializedPoolsComponent> _inializedPoolsFilter = null;

        private Dictionary<PoolType, GameObject> 
            _dataOfType = new Dictionary<PoolType, GameObject>();


        public void Init()
        {
            foreach (var item in _poolsFilter)
            {
                ref var poolsComponent = ref _poolsFilter.Get1(item);
                ref var pools = ref poolsComponent.Pools;

                SaveDataOfType(pools);
            }
        }

        public void Run()
        {
            foreach (var item in _inializedPoolsFilter)
            {
                ref var poolsComponent = ref _inializedPoolsFilter.Get1(item);
                ref var pools = ref poolsComponent.Pools;

                var pool = pools.Find(pool => pool.UnusedObjects.Count == 0);

                if (pool != null)
                    pool.UnusedObjects
                        .Enqueue(GetAnotherObject(ref pool.Parent, pool.Type));
            }
        }

        private GameObject GetAnotherObject(ref Transform parent, PoolType type)
        {
            var prefab = _dataOfType[type];

            var gameObject = GameObject.Instantiate(prefab, parent);
            gameObject.SetActive(false);

            return gameObject;
        }

        private void SaveDataOfType(List<PoolComponent> pools)
        {
            foreach (var pool in pools)
            {
                _dataOfType[pool.Type] = pool.Prefab;
            }
        }

    }
}
