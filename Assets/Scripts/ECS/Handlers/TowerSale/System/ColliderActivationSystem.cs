using ECS.Model.Components;
using ECS.Spawn.Component;
using ECS.UI.TowerSale.Event;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;

namespace ECS.Handlers.TowerSale.System
{
    public sealed class ColliderActivationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent, TowerPlatformComponent> _platformFilter = null;
        private readonly EcsFilter<TowerSaleEvent> _eventFilter = null;

        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_eventFilter))
                ActivateCollider();
        }

        private void ActivateCollider()
        {
            foreach (var item in _platformFilter)
            {
                ref var modelComponent = ref _platformFilter.Get1(item);
                ref var platformComponent = ref _platformFilter.Get2(item);

                ref var modelTransform = ref modelComponent.Transform;
                ref var spawnedTower = ref platformComponent.SpawnedTower;

                if (spawnedTower == null) continue;

                var collider = modelTransform.GetComponent<BoxCollider>();

                bool isTowerActive = spawnedTower.gameObject.activeInHierarchy;

                if (isTowerActive is false && collider.enabled is false)
                {
                    collider.enabled = true;
                    spawnedTower = null;
                }



            }
        }
    }
}
