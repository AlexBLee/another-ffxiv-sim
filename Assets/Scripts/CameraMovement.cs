using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _rotationSpeed = new Vector2(5f, 3f);
    [SerializeField] private Vector2 _pitchLimits = new Vector2(-20f, 70f);
    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private float _minZoom = 2f;
    [SerializeField] private float _maxZoom = 10f;
    [SerializeField] private Vector3 _pivotOffset = new Vector3(0, 1.5f, 0);

    private float _yaw = 0f;
    private float _pitch = 15f;
    private float _currentZoom = 5f;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _yaw = angles.y;
        _pitch = angles.x;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LateUpdate()
    {
        HandleInput();

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 targetPosition = _target.position + _pivotOffset;
        Vector3 offset = rotation * new Vector3(0, 0, -_currentZoom);

        transform.position = targetPosition + offset;
        transform.rotation = rotation;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(1))
        {
            _yaw += Input.GetAxis("Mouse X") * _rotationSpeed.x;
            _pitch -= Input.GetAxis("Mouse Y") * _rotationSpeed.y;
            _pitch = Mathf.Clamp(_pitch, _pitchLimits.x, _pitchLimits.y);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _currentZoom -= scroll * _zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);
    }
}