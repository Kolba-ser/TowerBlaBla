using ECS.CameraMovement.Components;
using ECS.MouseInput.Events;
using Leopotam.Ecs;
using Scripts.ForCheck;
using UnityEngine;

namespace ECS.CameraMovement.System
{
    public sealed class CameraMovementSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<MainCameraComponent, MovementParametersComponent> _cameraFilter = null;
        private readonly EcsFilter<MouseButtonPressEvent> _pressEventFilter = null;
        private readonly EcsFilter<MouseButtonHoldEvent> _holdEventFilter = null;

        private Vector3 _startMousePosition;

        public void Init()
        {
            Checker.CheckAsExclusive<MainCameraComponent>(_cameraFilter);

            foreach (var item in _cameraFilter)
            {
                ref var cameraComponent = ref _cameraFilter.Get1(item);
                ref var parametersComponent = ref _cameraFilter.Get2(item);

                var camera = cameraComponent.Camera;
                var size = camera.orthographicSize;

                parametersComponent.MaxCameraSize = size;
            }
        }

        public void Run()
        {
            if (_pressEventFilter.GetEntitiesCount() > 0)
            {
                SetStartMousePosition();
            }

            if (_holdEventFilter.GetEntitiesCount() > 0)
            {
                MoveCamera();
            }
        }

        private void SetStartMousePosition()
        {
            foreach (var item in _cameraFilter)
            {
                ref var cameraComponent = ref _cameraFilter.Get1(item);
                var camera = cameraComponent.Camera;

                _startMousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        private void MoveCamera()
        {
            foreach (var item in _cameraFilter)
            {
                ref var cameraComponent = ref _cameraFilter.Get1(item);
                ref var parametersComponent = ref _cameraFilter.Get2(item);

                ref var camera = ref cameraComponent.Camera;
                var cameraTransform = camera.transform;
                
                var speed = parametersComponent.MoveSpeed;
                var maxOffset = parametersComponent.MaxOffset;
                var minOffset = parametersComponent.MinOffset;

                float mousePositionX = camera.ScreenToWorldPoint(Input.mousePosition).x - _startMousePosition.x;
                float mousePositionZ = camera.ScreenToWorldPoint(Input.mousePosition).z - _startMousePosition.z;

                var movedMousePosition = new Vector3(
                                        Mathf.Clamp(cameraTransform.position.x - mousePositionX, minOffset.x, maxOffset.x),
                                        cameraTransform.position.y,
                                        Mathf.Clamp(cameraTransform.position.z - mousePositionZ, minOffset.z, maxOffset.z)
                                        );

                cameraTransform.position = new Vector3(
                                             Mathf.Lerp(cameraTransform.position.x, movedMousePosition.x, speed * Time.deltaTime),
                                             cameraTransform.position.y,
                                             Mathf.Lerp(cameraTransform.position.z, movedMousePosition.z, speed * Time.deltaTime)
                                             );
            }
        }
    }
}
