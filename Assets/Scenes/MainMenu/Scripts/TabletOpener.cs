using System;
using UnityEngine;
using MainMenu.Base;
using System.Collections;

namespace MainMenu
{
    public class TabletOpener : MonoBehaviour
    {
        

        [SerializeField] private ButtonBase[] _buttons;
        [SerializeField] private TabletBase _currentOpenTablet;

        private void Awake()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }
        
        private void OnClickButton(TabletBase tablet)
        {
            StartCoroutine(OpenTabletRoutine(tablet));
        }
        
        private IEnumerator OpenTabletRoutine(TabletBase tablet)
        {
            CloseTablet();
            
            yield return new WaitForSeconds(0.5f);
            
            tablet.Activate();
            _currentOpenTablet = tablet;
        } 
        private void CloseTablet()
        {
            _currentOpenTablet.PlayDisablingAnimations();
            _currentOpenTablet = null;
        }

        private void SubscribeOnEvents()
        {
            if (_buttons.Length > 0)
            {
                foreach (ButtonBase button in _buttons)
                {
                    button.OnClickEvent += OnClickButton;
                }
            }
        }
        private void UnsubscribeFromEvents()
        {
            if (_buttons.Length > 0)
            {
                foreach (ButtonBase button in _buttons)
                {
                    button.OnClickEvent -= OnClickButton;
                }
            }
        }
        
    }

}
