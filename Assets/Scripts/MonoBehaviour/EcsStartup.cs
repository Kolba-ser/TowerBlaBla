using ECS.Path.System;
using ECS.Pool.Systems;
using ECS.Wave.Systems;
using ECS.Spawn.Systems;
using ECS.Health.System;
using ECS.Damage.System;
using ECS.Raycast.System;
using ECS.Despawn.Systems;
using ECS.Checkers.Systems;
using ECS.Movement.Systems;
using ECS.Rotation.Systems;
using ECS.Targeting.System;
using ECS.Initialization.System;
using ECS.Dispenser.Systems;
using ECS.MouseInput.System;
using ECS.Navigation.System;
using ECS.TriggerZone.System;
using ECS.Towers.LaserTower.System;
using ECS.Towers.DragonTower.System;
using ECS.Towers.MortarTower.System;
using ECS.Detonator.System;
using ECS.Block.System;

using ECS.Handlers.WaveRemoval;

using ECS.Wave.Events;
using ECS.Raycast.Events;
using ECS.MouseInput.Events;
using ECS.Detonator.Event;

using ECS.Pool.Components;

using Leopotam.Ecs;
using Voody.UniLeo;
using UnityEngine;
using ECS.Explosion.System;
using ECS.UI.Menu.System;
using ECS.UI.SelectionTower.System;
using ECS.Spawn.Event;
using ECS.CameraMovement.System;
using Zenject;
using Scripts.Wallet;
using ECS.Handlers.Despawn;
using ECS.Handlers.NullTarget.Event;
using ECS.Handlers.NullTarget;
using ECS.UI.WaveLaunchProgressPresenter.System;
using ECS.Handlers.EndPoint.Event;
using ECS.Skipping.System;
using ECS.UI.Presenters.Health.System;
using ECS.Handlers.EndPoint;
using ECS.Handlers;
using ECS.UI.TowerSale.System;
using ECS.Handlers.TowerSale.System;
using ECS.Initialization.Systems;
using ECS.UI.TowerSale.Event;

namespace MonoB.Startup
{
    class EcsStartup : MonoBehaviour
    {
        [SerializeField]
        private Canvas _towerMenu;

        private EcsWorld _world;
        private EcsSystems _systems;

        private DispenseSystem _dispenseSystem;
        private MoneySystem _wallet;

        [Inject]
        private void Construct(MoneySystem service)
        {
            _wallet = service; 
        }

        private void Start()
        {
            InitializeVariables();

            _systems.ConvertScene();

            AddInjections();
            AddOneFrame();
            AddSystems();

            _systems.Init();
        }
        private void Update()
        {
            _systems.Run();
        }
        private void OnDestroy()
        {
            if (_systems == null) return;

            _systems.Destroy();
            _systems = null;

            _world.Destroy();
            _world = null;
        }

        private void AddSystems()
        {
            _systems
                .Add(new PoolSystem())

                .Add(new ObjectAccountingSystem())
                .Add(_dispenseSystem)

                .Add(new InitializeEntitySystem())
                .Add(new InitializeMenuSystem())

                .Add(new PathSystem())
                .Add(new NavigationSystem())

                .Add(new EnemySkipAccountingSystem())

                .Add(new ReachedEndpointHandlerSystem())

                .Add(new WavePreparationSystem())
                .Add(new WaveLauncherSystem())

                .Add(new CurrentWaveAccountingSystem())
                .Add(new EnemySpawnSystem())

                .Add(new TriggerZoneSystem())
                .Add(new NullTargetHandlerSystem())

                .Add(new TargetingSystem())
                .Add(new MovementSystem())
                .Add(new RotationSystem())

                .Add(new MouseInputSystem())
                .Add(new RaycastSystem())

                .Add(new CameraMovementSystem())

                .Add(new TowerCreateSystem())

                .Add(new TowerSelectionSystem())
                .Add(new TowerSalesSystem())


                .Add(new LaserTowerSystem())
                .Add(new DragonTowerSystem())
                .Add(new MortarTowerSystem())

                .Add(new DetonateSystem())

                .Add(new OneTimeDamageSystem())
                .Add(new MultipleDamageSystem())
                .Add(new ExplosionDamageSystem())

                .Add(new ExplosionSystem())

                .Add(new DamageAcquisitionSystem())

                .Add(new HealthSystem())

                .Add(new BlockSystem())
                .Add(new DespawnHandlerSystem())

                .Add(new HealthPresentationSystem())
                .Add(new LaunchProgressPresentationSystem())

                .Add(new WaveRemovalHandlerSystem())
                .Add(new TowerSalesHandlerSystem())

                .Add(new MenuPresentationSystem())
                .Add(new RemoveSystem())

                ;
        }
        private void AddInjections()
        {
            _systems
                .Inject(_dispenseSystem)

                .Inject(_wallet)

                .Inject(_towerMenu)
                ;
        }
        private void AddOneFrame()
        {
            _systems

                .OneFrame<WaveEndEvent>()
                
                .OneFrame<PoolsComponent>()

                .OneFrame<MouseButtonPressEvent>()

                .OneFrame<HitEvent>()
                .OneFrame<NullHitEvent>()

                .OneFrame<DetonateEvent>()

                .OneFrame<TowerCreateEvent>()
                .OneFrame<TowerSaleEvent>()
                
                .OneFrame<NullTargetEvent>()

                .OneFrame<EndpointReachedEvent>()

                .OneFrame<EndGameEvent>()
                ;
        }

        private void InitializeVariables()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _dispenseSystem = new DispenseSystem();
        }
    }
}
