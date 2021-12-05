using UnityEngine;
using System;
using System.Collections;


namespace MainMenu.GameSlider
{

    [RequireComponent(typeof(BoxCollider))]
    public class Slider : MonoBehaviour
    {
        [SerializeField] private Transform _minPosition;
        [SerializeField] private Transform _maxPosition;

        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        private float _distance;
        private float _mouseZCoord;
        private Vector3 _mouseOffset;

        public delegate void ChangeVolumeHandler(Slider slider, float volume);
        public event ChangeVolumeHandler OnVolumeChangedEvent;


        private void Awake()
        {
            _distance = Vector3.Distance(_minPosition.localPosition, _maxPosition.localPosition);
        }

        private void OnMouseDown()
        {
            _mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
            _mouseOffset = transform.position - GetMouseAsWorldPoint();
        }
        private void OnMouseDrag()
        {
            Move();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_minPosition.position, _maxPosition.position);
        }

        private void Move()
        {
            Vector3 dragPosition = GetMouseAsWorldPoint() + _mouseOffset;


            if (CanDrag(dragPosition))
            {
                transform.position = new Vector3(dragPosition.x, transform.position.y, dragPosition.z);
                CalculateVolume();
            }
        }

        private void CalculateVolume()
        {
            float currentDistance = Vector3.Distance(transform.localPosition, _maxPosition.localPosition);

            float distancePercent = currentDistance * 100f / _distance;

            float volume = (int)(_minValue * (distancePercent / 100));

            if (volume < -75)
            {
                volume = _minValue;
            }
            else if (volume > -5)
            {
                volume = 0;
            }

            OnVolumeChangedEvent?.Invoke(this, volume);

        }
        private Vector3 GetMouseAsWorldPoint()
        {
            Vector3 mousePoint = Input.mousePosition;

            mousePoint.z = _mouseZCoord;

            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
        private bool CanDrag(Vector3 dragPosition)
        {
            if (dragPosition.x >= _maxPosition.position.x)
            {
                return false;
            }
            else if (dragPosition.x <= _minPosition.position.x)
            {
                return false;
            }

            return true;
        }
    }
}
