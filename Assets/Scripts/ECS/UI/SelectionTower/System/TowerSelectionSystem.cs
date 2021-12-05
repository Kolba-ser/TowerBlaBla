using ECS.Pool.Types;
using ECS.UI.SelectionTower.Component;
using ECS.UI.SelectionTower.Request;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.UI.SelectionTower.System
{
    public sealed class TowerSelectionSystem : IEcsInitSystem
    {
        private readonly EcsFilter<TowerSelectionButtonComponent> _buttonFilter = null;

        public void Init()
        {
            foreach (var item in _buttonFilter)
            {
                ref var buttonComponent = ref _buttonFilter.Get1(item);

                ref var button = ref buttonComponent.Button;
                var cost = buttonComponent.Cost;
                var towerType = buttonComponent.TowerType;

                button.onClick.AddListener(() => SendRequest(towerType, cost));
            }
        }

        private void SendRequest(PoolType towerType, int cost)
        {
            var entity = WorldHandler.GetWorld().NewEntity();

            ref var request = ref entity.Get<CreateTowerRequest>();

            request.Cost = cost;
            request.TowerType = towerType;
        }
    }
}
