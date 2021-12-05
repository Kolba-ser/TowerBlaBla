using UnityEngine;
using MainMenu.Base;

namespace MainMenu.Buttons
{

    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]

    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _hoverSound;

        private Animator _animator;
        private bool _isHeated;
        private AudioSource _audioSource;

        public delegate void ExitHandler();
        public event ExitHandler OnExitButtonPressedEvent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseDown()
        {
            _audioSource.PlayOneShot(_clickSound);
            OnExitButtonPressedEvent?.Invoke();
            
        }
        private void OnMouseOver()
        {
            if(_isHeated == false)
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
