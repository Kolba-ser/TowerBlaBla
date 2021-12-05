using MainMenu.Base;
using UnityEngine;

namespace MainMenu.Tablets
{
    public sealed class LevelsTablet : TabletBase
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Activate()
        {
            gameObject.SetActive(true);
        }

        public override void PlayDisablingAnimations()
        {
            _animator.SetTrigger("close");
        }

        protected override void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
