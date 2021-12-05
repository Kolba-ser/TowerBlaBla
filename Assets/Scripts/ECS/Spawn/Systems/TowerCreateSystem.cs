using ECS.Dispenser.Systems;
using ECS.Model.Components;
using ECS.Pool.Types;
using ECS.Spawn.Event;
using ECS.Tags.Tags;
using ECS.UI.SelectionTower.Request;
using Leopotam.Ecs;
using Scripts.Wallet;
using Voody.UniLeo;

namespace ECS.Spawn.Systems
{
    public sealed class TowerCreateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CreateTowerRequest> _requestFilter = null;
        private readonly EcsFilter<WhichOpenedMenuTag, ModelComponent> _platformFilter = null;

        private readonly DispenseSystem _dispenser = null;

        private readonly MoneySystem _moneySystem = null;

        public void Run()
        {
            foreach (var item in _requestFilter)
            {
                ref var entity = ref _requestFilter.GetEntity(item);
                ref var request = ref _requestFilter.Get1(item);

                ref var towerType = ref request.TowerType;
                ref var cost = ref request.Cost;

                TryCreateTower(towerType, cost);

                entity.Del<CreateTowerRequest>();
            }
        }

        private void TryCreateTower(PoolType towerType, int cost)
        {
            if (_moneySystem.TakeMoney(cost) == false) 
                return;

            foreach (var item in _platformFilter)
            {
                ref var modelComponent = ref _platformFilter.Get2(item);

                ref var modelTransform = ref modelComponent.Transform;
                var spawnPosition = modelTransform.position;
                var spawnRotation = modelTransform.rotation;

                var go = _dispenser.GetObject(towerType);
                go.transform.position = spawnPosition;
                go.transform.rotation = spawnRotation;

                var entity = WorldHandler.GetWorld().NewEntity();

                entity.Get<TowerCreateEvent>();

            }


        }
    }
}
