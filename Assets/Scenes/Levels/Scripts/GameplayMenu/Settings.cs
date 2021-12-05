using UnityEngine;
using MainMenu.Save;
using UnityEngine.UI;
using UnityEngine.Audio;
using MainMenu.Loader;
using System;

namespace Levels.Scripts.GameplayMenu
{
    class Settings : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixer;
        [SerializeField] private Slider _slider;

        [SerializeField] private SceneLoader _sceneLoader;

        private Storage _storage = new Storage();
        private SettingsData _settingsData;


        private void Start()
        {
            Load();
        }
        
        private void OnEnable()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void OnValueChanged(float volume)
        {
            _mixer.audioMixer.SetFloat("MainSound", volume);
            _settingsData.MainSoundVolume = (int)volume;
        }
        private void OnSceneLoadEnd()
        {
            Save();
        }

        private void Save()
        {
            _storage.SaveData(_settingsData);
        }
        private void Load()
        {
            if (_storage.GetData(ref _settingsData))
            {
                _mixer.audioMixer.SetFloat("MainSound", _settingsData.MainSoundVolume);
                _slider.value = _settingsData.MainSoundVolume;
                return;
            }
        }

        private void SubscribeOnEvents()
        {
            _sceneLoader.OnSceneLoadEndEvent += OnSceneLoadEnd;
        }
        private void UnsubscribeFromEvents()
        {
            _sceneLoader.OnSceneLoadEndEvent -= OnSceneLoadEnd;
        }

    }
}
