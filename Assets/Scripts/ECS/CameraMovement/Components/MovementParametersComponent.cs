using System;
using UnityEngine;

namespace ECS.CameraMovement.Components
{
    [Serializable]
    public struct MovementParametersComponent
    {
        public float ChangeSizeSpeed;
        public float MoveSpeed;

        [Header("Limitations")]
        public Vector3 MaxOffset;
        public Vector3 MinOffset;
        public float MinCameraSize;

        [HideInInspector]
        public float MaxCameraSize;
    }
}
