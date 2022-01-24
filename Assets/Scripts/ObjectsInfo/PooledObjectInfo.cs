using ECS.Pool.Types;
using UnityEngine;

namespace Scripts.ObjectsInfo
{
    [CreateAssetMenu(fileName = "PooledObjectInfo", menuName = "Pool/ New PooledObject")]
    public sealed class PooledObjectInfo : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private PoolType _type;
        [SerializeField] private int _count;

        public GameObject Prefab => _prefab;
        public PoolType Type => _type;
        public int Count => _count;
    }
}
