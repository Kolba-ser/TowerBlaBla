using ECS.Remove.Request;
using ECS.Dispenser.Systems;
using ECS.Pool.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Remove.System
{
    public sealed class RemoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DespawnRequest, PooledObjectComponent> _eventFilter = null;
        private readonly DispenseSystem _dispenser = null;

        public void Run()
        {
            foreach (var item in _eventFilter)
            {
                ref var pooledObject = ref _eventFilter.Get2(item);
                ref var entity = ref _eventFilter.GetEntity(item);

                Debug.Log(_eventFilter.Get2(item).Object.name);

                _dispenser.RemoveObject(pooledObject);
                entity.Del<DespawnRequest>();
            }
        }
    }
}
