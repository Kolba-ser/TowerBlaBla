using ECS.Model.Components;
using ECS.Rotation.Components;
using ECS.Tags.Components;
using ECS.Targeting.Components;
using Leopotam.Ecs;

namespace ECS.Targeting.System
{
    public class TargetingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetComponent, RotatableComponent, ModelComponent>
                        .Exclude<DisabledTagComponent>  
                        _targetFilter = null;


        public void Run()
        {
            foreach (var item in _targetFilter)
            {
                ref var targetComponent = ref _targetFilter.Get1(item);
                ref var rotatableComponent = ref _targetFilter.Get2(item);
                ref var modelComponent = ref _targetFilter.Get3(item);

                if (targetComponent.TargetTransform == null) 
                    continue;
                
                ref var lookDirection = ref rotatableComponent.Direction;
                
                var targetPosition = targetComponent.TargetTransform.position;
                var modelPosition = modelComponent.Transform.position;

                lookDirection = targetPosition - modelPosition;
            }
        }
    }
}
