using ECS.Initialization.Requests;
using ECS.UI.Menu.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Initialization.Systems
{
    public sealed class InitializeMenuSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InitializeMenuRequest, MenuComponent> _menuFilter = null;

        private readonly Canvas _menu;


        public void Run()
        {
            foreach (var item in _menuFilter)
            {
                ref var entity = ref _menuFilter.GetEntity(item);
                ref var menuComponent = ref _menuFilter.Get2(item);

                menuComponent.Menu = _menu;

                entity.Del<InitializeMenuRequest>();
            }
        }
    }
}
