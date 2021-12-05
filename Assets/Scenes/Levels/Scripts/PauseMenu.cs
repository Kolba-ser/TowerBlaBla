using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.GameplayMenu
{

    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Canvas _pauseMenu;

        private bool _isActive;

        public delegate void SwitchHandler(bool isActive);
        public event SwitchHandler OnMenuSwitchedEvent;

        public void Toggle()
        {
            if (_isActive)
            {
                _pauseMenu.enabled = false;
                _isActive = false;
                OnMenuSwitchedEvent?.Invoke(false);
            }
            else
            {
                _pauseMenu.enabled = true;
                _isActive = true;
                OnMenuSwitchedEvent?.Invoke(_isActive);
            }
        }
    }
}
