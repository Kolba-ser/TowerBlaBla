using Leopotam.Ecs;
using Scripts.MonoB.References;
using System;
using UnityEngine;

namespace Scripts.ForCheck
{
    public sealed class Checker
    {
        public static void CheckAsExclusive<T>(EcsFilter filter)
        {
            var quantity = filter.GetEntitiesCount();
            if (quantity == 0)
                throw new Exception($"Missing {typeof(T)}");
            if (quantity > 1)
                throw new Exception($"{typeof(T)} must be single");
        }

        public static void CheckEntityAsGameObject(GameObject gameObject)
        {
            var reference = gameObject.GetComponent<EntityReference>();
            if (reference == null)
                throw new Exception($"Maybe {gameObject.name} lack EntityReference");

            ref var entity = ref reference.Entity;
            if (entity.IsNull())
                throw new Exception($"The entity not initizlized to the {gameObject.name}");
        }

        public static bool IsThereMoreEntitiesThanZero(EcsFilter filter)
        {
            if (filter.GetEntitiesCount() > 0)
                return true;

            return false;
        }
    }
}
