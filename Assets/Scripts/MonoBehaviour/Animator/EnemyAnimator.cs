using UnityEngine;

namespace Levels.Enemies.Animator
{

    [RequireComponent(typeof(UnityEngine.Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private UnityEngine.Animator _animator;

        public delegate void DestroyHandler();
        public event DestroyHandler OnDestroyAnimationEndEvent;
        
        private void Awake()
        {
            _animator = GetComponent<UnityEngine.Animator>();
        }
        
        public void PlayDestroy()
        {
            _animator.SetTrigger("destroy");
        }

        private void OnDisable()
        {
            PlayDestroy();
        }

        private void InvoikeDestroyAnimationEndEvent() 
        {
            OnDestroyAnimationEndEvent?.Invoke();
        }
    }
}
