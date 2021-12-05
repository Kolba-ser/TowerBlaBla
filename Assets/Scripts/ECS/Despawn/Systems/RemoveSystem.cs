using ECS.Despawn.Request;
using ECS.Dispenser.Systems;
using ECS.Pool.Components;
using ECS.Tags.Components;
using Leopotam.Ecs;

namespace ECS.Despawn.Systems
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

                _dispenser.RemoveObject(pooledObject);
                entity.Del<DespawnRequest>();
            }
        }
    }
}
