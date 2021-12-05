using ECS.Wave.Components;
using UnityEngine;
using Zenject;

namespace Scripts.MonoInstallers
{
    public class WaveSequenceInstaller : MonoInstaller
    {

        [SerializeField]
        private WaveSequenceComponent _waveSequence; 

        public override void InstallBindings()
        {
            Container
                .Bind<WaveSequenceComponent>()
                .FromInstance(_waveSequence)
                .AsSingle()
                .NonLazy();
        }
    }
}
