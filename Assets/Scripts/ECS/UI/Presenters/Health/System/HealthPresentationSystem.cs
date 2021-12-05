using ECS.Handlers.EndPoint.Event;
using ECS.Skipping.Component;
using ECS.UI.Presenters.Health.Component;
using Leopotam.Ecs;
using Scripts.ForCheck;

namespace ECS.UI.Presenters.Health.System
{
    public sealed class HealthPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthBarComponent, PassesNumberComponent> _healthBarFilter = null;
        private readonly EcsFilter<EndpointReachedEvent> _eventFilter = null;

        private const int FILL_AMOUNT = 1;

        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_eventFilter))
                UpdateHealthValue();

        }

        private void UpdateHealthValue()
        {
            foreach (var item in _healthBarFilter)
            {
                ref var healthBarComponent = ref _healthBarFilter.Get1(item);
                ref var passesNumberComponent = ref _healthBarFilter.Get2(item);

                ref var healthBar = ref healthBarComponent.HealthBar;

                var numberOfPasses = passesNumberComponent.NumberOfPasses;
                var changableNumberOfPasses = passesNumberComponent.ChangableNumberOfPasses;


                healthBar.fillAmount =
                    (float)changableNumberOfPasses * 100 / numberOfPasses / 100 * FILL_AMOUNT;
            }
        }
    }
}
