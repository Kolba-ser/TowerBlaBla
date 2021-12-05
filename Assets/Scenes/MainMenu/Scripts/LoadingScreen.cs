using UnityEngine;

namespace MainMenu
{
    [RequireComponent(typeof(Canvas))]
    public class LoadingScreen : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Activate()
        {
            _canvas.enabled = true;
        }
    }
}
