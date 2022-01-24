using Scripts.ForCheck;
using ECS.MouseInput.Components;
using ECS.MouseInput.Events;
using ECS.Raycast.Events;
using Leopotam.Ecs;
using Scripts.MonoB.References;
using System;
using UnityEngine;
using Voody.UniLeo;
using UnityEngine.EventSystems;
using ECS.CameraMovement.Components;

namespace ECS.Raycast.System
{
    public sealed class RaycastSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<MouseButtonPressEvent> _pressEventFilter = null;
        private readonly EcsFilter<MainCameraComponent> _mainCameraFilter = null;

        private Camera _camera;
        private EcsEntity _entity;

        private const int HITS_LENGHT = 1;
        private const int RAY_DISTANCE = 100;

        public void Init()
        {
            int quantity = _mainCameraFilter.GetEntitiesCount();
            Checker.CheckAsExclusive<MainCameraComponent>(_mainCameraFilter);

            _camera = _mainCameraFilter.Get1(0).Camera;

            _entity = WorldHandler.GetWorld().NewEntity();
            _entity.Get<EntityNotEmptyComponent>();
        }
        
        public void Run()
        {

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            foreach (var item in _pressEventFilter)
            {
                HandlePressEvent();
            }
        }

        private void HandlePressEvent()
        {
            Ray ray = GetRay();
            RaycastHit[] hits = new RaycastHit[HITS_LENGHT];
            Physics.RaycastNonAlloc(ray, hits, RAY_DISTANCE);

            if (hits.Length > 0)
                HanldleHits(hits);
        }
        private void HanldleHits(RaycastHit[] hits)
        {
            foreach (var hit in hits)
            {
                Debug.Log(hit.collider);

                if (hit.collider == null)
                {
                    _entity.Get<NullHitEvent>();
                    continue;
                }

                var gameObject = hit.collider.gameObject;

                var entityReference = gameObject.GetComponent<EntityReference>();
                if (entityReference == null)
                    continue;

                var entity = entityReference.Entity;

                if (entity.IsNull() == false)
                {
                    entity.Get<HitEvent>();
                }
            }
        }

        private Ray GetRay()
        {
            return _camera.ScreenPointToRay(Input.mousePosition);
        }

    }
}
