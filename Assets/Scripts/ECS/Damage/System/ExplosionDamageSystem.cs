using ECS.Damage.Component;
using ECS.Damage.Request;
using ECS.Detonator.Event;
using ECS.Model.Components;
using Leopotam.Ecs;
using Scripts.MonoB.References;
using UnityEngine;

namespace ECS.Damage.System
{
    public sealed class ExplosionDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DetonateEvent, ExplosionDamageComponent, ModelComponent> 
                        _explosionFilter = null;

        private const int ENEMY_LMASK = 1 << 7;

        public void Run()
        {
            foreach (var item in _explosionFilter)
            {
                ref var explosionComponent = ref _explosionFilter.Get2(item);
                ref var modelComponent = ref _explosionFilter.Get3(item);

                ref var modelTransform = ref modelComponent.Transform;
                
                var range = explosionComponent.Range;
                var damage = explosionComponent.Damage;

                var detonationLocation = modelTransform.position;

                ApplyDamage(damage, range, detonationLocation);
            }
        }

        private void ApplyDamage(float damage, float range, Vector3 detonationLocation)
        {
            var colliders = Physics.OverlapSphere(detonationLocation, range, ENEMY_LMASK);

            foreach (var collider in colliders)
            {
                var transform = collider.transform;
                ref var entity = ref transform.GetComponent<EntityReference>().Entity;
                
                Debug.Log(transform.name);

                ref var request = ref entity.Get<GetDamageRequest>();

                request.Damage = damage;

            }
        }
    }
}
