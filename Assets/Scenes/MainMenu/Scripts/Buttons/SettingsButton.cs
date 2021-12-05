using MainMenu.Base;
using UnityEngine;

namespace MainMenu.Buttons
{
    public class SettingsButton : ButtonBase
    {
        [SerializeField] private TabletBase _tablet;

        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _hoverSound;

        private bool _isHeated;
        private Animator _animator;
        private AudioSource _audioSource;

        public override event ClickHandler OnClickEvent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseDown()
        {
            OnClickEvent?.Invoke(_tablet);
            _audioSource.PlayOneShot(_clickSound);
        }
        private void OnMouseOver()
        {
            if (_isHeated == false)
            {
                _animator.SetTrigger("heat");
                _audioSource.PlayOneShot(_hoverSound);
                _isHeated = true;
            }
        }
        private void OnMouseExit()
        {
            if (_isHeated)
            {
                _animator.SetTrigger("cool");
                _isHeated = false;
            }
        }
    }
}
