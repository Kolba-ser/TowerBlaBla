using ECS.Handlers;
using ECS.Handlers.EndPoint.Event;
using ECS.Skipping.Component;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.Skipping.System
{
    public sealed class EnemySkipAccountingSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<EndpointReachedEvent> _eventFilter = null;
        private readonly EcsFilter<PassesNumberComponent> _passesNumberFilter = null;

        private bool _wasEventInvoked;
        public void Init()
        {
            foreach (var item in _passesNumberFilter)
            {
                ref var passesNumberComponent = ref _passesNumberFilter.Get1(item);

                ref var numberOfPasses = ref passesNumberComponent.NumberOfPasses;
                ref var changableNumberOfPasses = ref passesNumberComponent.ChangableNumberOfPasses;

                changableNumberOfPasses = numberOfPasses;
            }
        }

        public void Run()
        {
            if (_wasEventInvoked) return;

            if (Checker.IsThereMoreEntitiesThanZero(_eventFilter))
                SubtractPass();
        }

        private void SubtractPass()
        {
            foreach (var item in _passesNumberFilter)
            {
                ref var passesNumberComponent = ref _passesNumberFilter.Get1(item);

                ref var changableNumberOfPasses = ref passesNumberComponent.ChangableNumberOfPasses;
                
                changableNumberOfPasses--;

                if(changableNumberOfPasses <= 0)
                {
                    var entity = WorldHandler.GetWorld().NewEntity();
                    entity.Get<EndGameEvent>();
                    _wasEventInvoked = true;
                    return;
                }
                
                
            }
        }
    }
}
