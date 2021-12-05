using ECS.Handlers.TowerSale.Request;
using ECS.UI.TowerSale.Component;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace ECS.UI.TowerSale.System
{
    public sealed class TowerSalesSystem : IEcsInitSystem
    {
        private readonly EcsFilter<SalesButtonComponent> _buttonFilter = null;

        public void Init()
        {
            foreach (var item in _buttonFilter)
            {
                ref var buttonComponent = ref _buttonFilter.Get1(item);

                ref var saleButton = ref buttonComponent.SaleButton;
                var salePrice = buttonComponent.SalePrice;

                saleButton.onClick.AddListener(() => SendRequest(salePrice));
            }
        }

        private void SendRequest(int price)
        {
            var entity = WorldHandler.GetWorld().NewEntity();

            ref var request = ref entity.Get<TowerSaleRequest>();
            request.SalePrice = price;
        }
    }
}
