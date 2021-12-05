using ECS.UI.WaveLaunchProgressPresenter.Component;
using ECS.Wave.Components;
using Leopotam.Ecs;

namespace ECS.UI.WaveLaunchProgressPresenter.System
{
    public sealed class LaunchProgressPresentationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WaveProgressBarComponent> _progressBarFilter = null;
        private readonly EcsFilter<InitializedSequenceComponent> _sequenceFilter = null;

        private int _waveIndex = -1;
        private const int FILL_AMOUNT = 1;

        public void Run()
        {
            foreach (var item in _sequenceFilter)
            {
                ref var sequnceComponent = ref _sequenceFilter.Get1(item);

                var waveIndex = sequnceComponent.WaveIndex;
                var launchProgress = sequnceComponent.LaunchProgress;
                var currentDelay = sequnceComponent.CurrentDelay;

                if (waveIndex != _waveIndex)
                {
                    ChangeWaveNumber(waveIndex);
                }

                ChangeProgressBarValue(launchProgress, currentDelay);
            }
        }

        private void ChangeProgressBarValue(float progress, float delay)
        {
            foreach (var item in _progressBarFilter)
            {
                ref var progressBarComponent = ref _progressBarFilter.Get1(item);

                ref var progressBar = ref progressBarComponent.ProgressBar;

                progressBar.fillAmount = (progress * 100 / delay) / 100 * FILL_AMOUNT;
            }
        }

        private void ChangeWaveNumber(int number)
        {
            foreach (var item in _progressBarFilter)
            {
                ref var progressBarComponent = ref _progressBarFilter.Get1(item);

                ref var waveNumber = ref progressBarComponent.WaveNumber;

                waveNumber.text = number.ToString();
            }
        }


    }
}
