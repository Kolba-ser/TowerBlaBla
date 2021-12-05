using UnityEngine.UI;
using System;


namespace ECS.UI.WaveLaunchProgressPresenter.Component
{
    [Serializable]
    public struct  WaveProgressBarComponent
    {
        public Text WaveNumber;
        public Image ProgressBar;
    }
}
