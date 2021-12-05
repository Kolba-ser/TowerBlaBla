using UnityEngine;
using MainMenu.Base;

namespace MainMenu.Buttons
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    
    public class SelectionLevelButton : MonoBehaviour
    {
        

        [SerializeField] private int _sceneIndex;

        [SerializeField] private AudioClip _clickSound;
        [SerializeField] private AudioClip _hoverSound;

        public delegate void ClickHandler(int index);
        public static event ClickHandler OnClickEvent;

        private bool _isHeated;

        private Animator _animator;
        private AudioSource _audioSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseDown()
        {
            _audioSource.PlayOneShot(_clickSound);
            OnClickEvent?.Invoke(_sceneIndex);
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
