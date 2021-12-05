using System;
using UnityEngine;
using UnityEngine.UI;
using Levels.GameplayMenu;
using MainMenu.Loader;

namespace Levels.Controllers
{

    public class TimeChanger : MonoBehaviour
    {
        [SerializeField] private Button _changeTimeButton;
        [SerializeField] private Image _buttonImage;

        [Header("Time status images")]
        [SerializeField] private TimeStatusSpriteInfo _normalStatusData;
        [SerializeField] private TimeStatusSpriteInfo _acceleratedStatusData;
        [SerializeField] private TimeStatusSpriteInfo _slowedStatusData;

        private PauseMenu _pauseMenu;
        
        private TimeState _currentState;

        private int _clickCount;
        private int _timeStateLengh;
        private enum TimeState
        {
            Normal = 0,
            Accelerated = 1,
            Slowed = 2,
            Stoped = 3
        }

        [Serializable]
        private struct TimeStatusSpriteInfo
        {
            public Sprite SourceImage;
            public SpriteState SpriteState;
        }


        public void ChangeTimeState()
        {
            _clickCount++;

            if (_clickCount > _timeStateLengh - 1)
            {
                _clickCount = 0;
            }

            _currentState = (TimeState)Enum.GetValues(typeof(TimeState)).GetValue(_clickCount);

            SetFlowTime();
        }


        private void Awake()
        {
            _timeStateLengh = Enum.GetNames(typeof(TimeState)).Length - 1;
            _pauseMenu = (PauseMenu)FindObjectOfType(typeof(PauseMenu));

            SetNormalFlowTime();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void OnMenuSwitched(bool isActive)
        {
            StopTime(isActive);
        }

        private void SetFlowTime()
        {
            switch (_currentState)
            {
                case TimeState.Normal:
                    Time.timeScale = 1f;
                    ChangeButtonSprite(_normalStatusData);
                    break;
                case TimeState.Accelerated:
                    Time.timeScale = 2f;
                    ChangeButtonSprite(_acceleratedStatusData);
                    break;
                case TimeState.Slowed:
                    Time.timeScale = 0.5f;
                    ChangeButtonSprite(_slowedStatusData);
                    break;
            }
        }

        private void StopTime(bool isActive)
        {
            if (isActive)
            {
                _changeTimeButton.interactable = false;
                Time.timeScale = 0f;
                return;
            }

            _changeTimeButton.interactable = true;
            Time.timeScale = 1f;
        }
        private void SetNormalFlowTime()
        {
            print("Time is launched");
            float flowRate = Time.timeScale;

            if (flowRate > 1f || flowRate < 1f)
            {
                Time.timeScale = 1f;
            }
        }

        private void ChangeButtonSprite(TimeStatusSpriteInfo data)
        {
            _buttonImage.sprite = data.SourceImage;
            _changeTimeButton.spriteState = data.SpriteState;
        }

        private void SubscribeToEvents()
        {
            if (_pauseMenu != null)
            {
                _pauseMenu.OnMenuSwitchedEvent += OnMenuSwitched;
            }
        }
        private void UnsubscribeFromEvents()
        {
            if (_pauseMenu != null)
            {
                _pauseMenu.OnMenuSwitchedEvent -= OnMenuSwitched;
            }
        }
    }


}