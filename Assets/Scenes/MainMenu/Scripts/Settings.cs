using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using MainMenu.GameSlider;
using MainMenu.Save;
using MainMenu.Loader;
using MainMenu.Buttons;

namespace MainMenu
{

    public class Settings : MonoBehaviour
    {

        [SerializeField] private AudioMixerGroup _mixer;

        [SerializeField] private Slider _fxSlider;
        [SerializeField] private Slider _mainSoundSlider;

        [SerializeField] private ExitButton _exitButton;

        [SerializeField] private SceneLoader _sceneLoader;
        
        private Storage _storage;
        private SettingsData _settingsData;
        
        private void Awake()
        {
            if (_exitButton == null) 
                throw new Exception("ExitButton is not set");
            
            _storage = new Storage();
            _settingsData = new SettingsData();

        }
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

        private void OnVolumeChanged(Slider slider, float volume)
        {
            ChangeVolume(slider, volume);

        }
        private void OnSceneLoadEnd()
        {
            Save();
        }
        private void OnExitButtonPressed()
        {
            Save();
            Application.Quit();
        }

        private void ChangeVolume(Slider slider, float volume)
        {
            if (slider == _fxSlider)
            {
                _settingsData.FxSliderPosition = _fxSlider.transform.localPosition;
                _settingsData.FxVolume = volume;
                _mixer.audioMixer.SetFloat("FX", volume);
            }
            else if (slider == _mainSoundSlider)
            {
                _settingsData.MainSoundSliderPosition = _mainSoundSlider.transform.localPosition;
                _settingsData.MainSoundVolume = volume;
                _mixer.audioMixer.SetFloat("MainSound", volume);
            }
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
                _mixer.audioMixer.SetFloat("FX", _settingsData.FxVolume);

                _fxSlider.transform.localPosition = _settingsData.FxSliderPosition;
                _mainSoundSlider.transform.localPosition = _settingsData.MainSoundSliderPosition;
            }
        }

        private void SubscribeOnEvents()
        {
            _mainSoundSlider.OnVolumeChangedEvent += OnVolumeChanged;
            _fxSlider.OnVolumeChangedEvent += OnVolumeChanged;

            _sceneLoader.OnSceneLoadEndEvent += OnSceneLoadEnd;
            _exitButton.OnExitButtonPressedEvent += OnExitButtonPressed;
        }
        private void UnsubscribeFromEvents()
        {
            _mainSoundSlider.OnVolumeChangedEvent -= OnVolumeChanged;
            _fxSlider.OnVolumeChangedEvent -= OnVolumeChanged;
            
            _sceneLoader.OnSceneLoadEndEvent -= OnSceneLoadEnd;
            _exitButton.OnExitButtonPressedEvent -= OnExitButtonPressed;
        }
    }
}
