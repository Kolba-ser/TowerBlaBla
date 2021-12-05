using ECS.Despawn.Request;
using ECS.Handlers.TowerSale.Request;
using ECS.Pool.Components;
using ECS.Tags.Tags;
using ECS.UI.TowerSale.Event;
using Leopotam.Ecs;
using Scripts.ForCheck;
using Scripts.Wallet;
using System;
using UnityEngine;

namespace ECS.Handlers.TowerSale.System
{
    public sealed class TowerSalesHandlerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TowerSaleRequest> _requestFilter = null;
        private readonly EcsFilter<WhichOpenedMenuTag, PooledObjectComponent> _towerFilter = null;

        private readonly MoneySystem _moneySystem = null;
        public void Run()
        {
            if (Checker.IsThereMoreEntitiesThanZero(_requestFilter))
                HandleSellRequest();
        }

        private void HandleSellRequest()
        {
            foreach (var item in _towerFilter)
            {
                ref var entity = ref _towerFilter.GetEntity(item);

                var salePrice = _requestFilter.Get1(0).SalePrice;
                _moneySystem.AppendMoney(salePrice);

                entity.Get<DespawnRequest>();
            }

            ref var requestingEntity = ref _requestFilter.GetEntity(0);
            requestingEntity.Get<TowerSaleEvent>();
            requestingEntity.Del<TowerSaleRequest>();
        }
    }
}
