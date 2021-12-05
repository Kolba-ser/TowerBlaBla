using UnityEngine;

namespace Scripts.MonoB.Animators
{
    [RequireComponent(typeof(Animator))]
    public sealed class TowerPlatformAnimator : MonoBehaviour
    {
        private Animator _animator;
        private bool _isHeated;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnMouseOver()
        {
            if (_isHeated == false)
            {
                _animator.SetTrigger("heat");
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
