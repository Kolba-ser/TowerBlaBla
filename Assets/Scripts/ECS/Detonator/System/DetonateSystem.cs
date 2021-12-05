using ECS.Despawn.Request;
using ECS.Detonator.Component;
using ECS.Detonator.Event;
using ECS.Model.Components;
using ECS.Tags.Components;
using Leopotam.Ecs;

namespace ECS.Detonator.System
{
    public sealed class DetonateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, DetonableComponent>
                        .Exclude<DisabledTagComponent>
                        _detonableFilter = null;
        
        
        public void Run()
        {
            foreach (var item in _detonableFilter)
            {
                ref var modelComponent = ref _detonableFilter.Get1(item);
                ref var detonalbeComponent = ref _detonableFilter.Get2(item);
                ref var entity = ref _detonableFilter.GetEntity(item);

                ref var modelTransform = ref modelComponent.Transform;
                var detonationHeight = detonalbeComponent.DetonationHieght;

                if(modelTransform.position.y <= detonationHeight)
                {
                    entity.Get<DetonateEvent>();
                    entity.Get<DespawnRequest>();
                }
            }
        }
    }
}
