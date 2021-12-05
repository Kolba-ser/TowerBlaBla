using UnityEngine;

namespace Levels.Camera
{

    [RequireComponent(typeof(UnityEngine.Camera))]

    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector3 _maxOffset = new Vector3(13.63f, 0f, 1.41f);
        [SerializeField] private Vector3 _minOffset = new Vector3(-3.45f, 0f, -13.51f);

        private UnityEngine.Camera _camera;

        private float _maxSize;
        private float _speed = 30f;
        private float _minSize = 3f;
        private float _scrollWheelSpeed = 10;

        private Vector3 _removedMousePosition;
        private Vector3 _startMousePosition;

        private void Awake()
        {
            _camera = GetComponent<UnityEngine.Camera>();
            _maxSize = _camera.orthographicSize;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _startMousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                MoveCamera();
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                if (CanChangeSize(Input.GetAxis("Mouse ScrollWheel")))
                {
                    float mouseWheelDirection = Input.GetAxis("Mouse ScrollWheel");
                    ChangeCameraSize(mouseWheelDirection);
                }
            }

        }

        private void MoveCamera()
        {
            float positionX = _camera.ScreenToWorldPoint(Input.mousePosition).x - _startMousePosition.x;
            float positionZ = _camera.ScreenToWorldPoint(Input.mousePosition).z - _startMousePosition.z;


            _removedMousePosition = new Vector3
                                        (
                                        Mathf.Clamp(transform.position.x - positionX, _minOffset.x, _maxOffset.x),
                                        transform.position.y,
                                        Mathf.Clamp(transform.position.z - positionZ, _minOffset.z, _maxOffset.z)
                                        );

            _camera.transform.position = new Vector3
                                             (
                                             Mathf.Lerp(transform.position.x, _removedMousePosition.x, _speed * Time.deltaTime),
                                             transform.position.y,
                                             Mathf.Lerp(transform.position.z, _removedMousePosition.z, _speed * Time.deltaTime)
                                             );
        }
        private void ChangeCameraSize(float direction)
        {

            _camera.orthographicSize = Mathf.Lerp(
                                                    _camera.orthographicSize,
                                                    _camera.orthographicSize + direction * _scrollWheelSpeed,
                                                    Mathf.Abs(direction * _scrollWheelSpeed
                                                ));
        }

        private bool CanChangeSize(float direction)
        {
            if (direction > 0 && _camera.orthographicSize < _maxSize)
            {
                return true;
            }
            else if (direction < 0 && _camera.orthographicSize > _minSize)
            {
                return true;
            }

            return false;
        }
    }
}