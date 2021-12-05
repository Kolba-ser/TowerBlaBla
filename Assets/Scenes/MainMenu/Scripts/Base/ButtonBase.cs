using UnityEngine;


namespace MainMenu.Base
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public abstract class ButtonBase : MonoBehaviour
    {
        public delegate void ClickHandler(TabletBase tablet);
        public abstract event ClickHandler OnClickEvent;

    }
}
