using ECS.Despawn.Request;
using ECS.Handlers.EndPoint.Event;
using Leopotam.Ecs;

namespace ECS.Handlers.EndPoint
{
    public sealed class ReachedEndpointHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EndpointReachedEvent> _eventFilter = null; 

        public void Run()
        {
            foreach (var item in _eventFilter)
            {
                ref var entity = ref _eventFilter.GetEntity(item);
                entity.Get<DespawnRequest>();
            }
        }
    }
}
