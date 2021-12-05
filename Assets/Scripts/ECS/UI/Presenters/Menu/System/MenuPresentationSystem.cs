using ECS.Raycast.Events;
using ECS.Spawn.Event;
using ECS.Tags.Tags;
using ECS.UI.Menu.Components;
using ECS.UI.TowerSale.Event;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;

namespace ECS.UI.Menu.System
{
    public sealed class MenuPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HitEvent, MenuComponent> _menuFilter = null;

        private readonly EcsFilter<NullHitEvent> _nullHitFilter = null;
        private readonly EcsFilter<TowerCreateEvent> _creationEventFilter = null;
        private readonly EcsFilter<TowerSaleEvent> _saleEventFilter = null;

        private readonly EcsFilter<WhichOpenedMenuTag, MenuComponent> _openerFilter = null;

        public void Run()
        {
            if(DoNeedToCloseMenu())
            {
                CloseCurrentlyOpenMenu();
            }

            foreach (var item in _menuFilter)
            {
                CloseCurrentlyOpenMenu();

                ref var menuComponent = ref _menuFilter.Get2(item);
                ref var entity = ref _menuFilter.GetEntity(item);

                ref var menu = ref menuComponent.Menu;
                OpenMenu(menu);
                entity.Get<WhichOpenedMenuTag>();

            }
        }

        private void OpenMenu( Canvas menu)
        {
            menu.enabled = true;
        }

        private void CloseCurrentlyOpenMenu()
        {
            foreach (var item in _openerFilter)
            {
                ref var menuComponent = ref _openerFilter.Get2(item);
                ref var entity = ref _openerFilter.GetEntity(item);
                
                ref var menu = ref menuComponent.Menu;

                menu.enabled = false;
                entity.Del<WhichOpenedMenuTag>();
                
            }
        }

        private bool DoNeedToCloseMenu()
        {
            return Checker.IsThereMoreEntitiesThanZero(_nullHitFilter) |
                   Checker.IsThereMoreEntitiesThanZero(_creationEventFilter) |
                   Checker.IsThereMoreEntitiesThanZero(_saleEventFilter);
        }
    }
}
