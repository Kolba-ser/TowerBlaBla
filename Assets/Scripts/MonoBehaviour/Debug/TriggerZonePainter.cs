using UnityEngine;

namespace Scripts.MonoB.Debug
{
    [ExecuteAlways]
    public sealed class TriggerZonePainter : MonoBehaviour
    {
        [SerializeField]
        private float _radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
