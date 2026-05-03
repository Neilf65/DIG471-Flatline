using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    private Vector2 _mouseInput;
    private float _pitch;
    public Transform Target;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(Vector3.up, _mouseInput.x * _sensitivity * Time.deltaTime);
        _pitch = _mouseInput.y * _sensitivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90);
    }
}
