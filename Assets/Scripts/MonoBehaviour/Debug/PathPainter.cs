using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MonoB.Debug
{
    [ExecuteAlways]
    public sealed class PathPainter : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> _wayPoints;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            for (int i = 0; i < _wayPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[i + 1].position);
            }
        }
    }
}
