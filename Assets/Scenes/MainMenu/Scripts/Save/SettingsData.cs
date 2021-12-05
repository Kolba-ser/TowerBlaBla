using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace MainMenu.Save
{

    [Serializable]
    public struct SettingsData
    {

        public Vector3 FxSliderPosition { get; set; }
        public Vector3 MainSoundSliderPosition { get; set; }

        public float FxVolume { get; set; }
        public float MainSoundVolume { get; set; }

        public int LevelIndex { get; set; }

    }
}
