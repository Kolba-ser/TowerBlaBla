using ECS.MouseInput.Events;
using ECS.MouseInput.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;
using Voody.UniLeo;

namespace ECS.MouseInput.System
{
    public sealed class MouseInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsEntity _entity;

        public void Init()
        {
            _entity = WorldHandler.GetWorld().NewEntity();
            _entity.Get<EntityNotEmptyComponent>();
        }

        public void Run()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _entity.Get<MouseButtonPressEvent>();
            }
            if (Input.GetMouseButton(0))
            {
                _entity.Get<MouseButtonHoldEvent>();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _entity.Del<MouseButtonHoldEvent>();
            }
        }

    }
}
