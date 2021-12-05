using ECS.Block.Component;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Block.System
{
    public sealed class BlockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BlockDurationComponent> _blockFilter = null;

        public void Run()
        {
            foreach (var item in _blockFilter)
            {
                ref var blockShot = ref _blockFilter.Get1(item);
                ref var entity = ref _blockFilter.GetEntity(item);

                ref var timer = ref blockShot.Timer;

                if(timer <= 0)
                {
                    entity.Del<BlockDurationComponent>();
                }

                timer -= Time.deltaTime;

            }
        }
    }
}
