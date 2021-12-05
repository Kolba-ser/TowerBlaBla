using UnityEngine;

namespace MainMenu.Base
{
    [RequireComponent(typeof(Animator))]
    public abstract class TabletBase : MonoBehaviour
    {
        public abstract void Activate();
        public abstract void PlayDisablingAnimations();
        protected abstract void Disable();
    }
}
