using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cam;
    private State state;

    [SerializeField] private Vector2 horizontalRotLimits;
    [SerializeField] private Vector2 verticalRotLimits;

    private Vector3 currentRotation;

    private void Start()
    {
        state = State.Find();
        cam = Camera.main;
        currentRotation = cam.transform.rotation.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private Vector3 GetAxisVec()
    {
        var yRot = Input.GetAxisRaw("Mouse X");
        var xRot = Input.GetAxisRaw("Mouse Y");

        return new Vector3(-xRot, yRot, 0);
    }

    private void Update()
    {
        var rotAxis = GetAxisVec() * state.LookSensitivity;

        currentRotation = ClampVec(currentRotation + rotAxis,
            verticalRotLimits, horizontalRotLimits);

        cam.transform.rotation = Quaternion.Euler(currentRotation);
    }

    private Vector3 ClampVec(Vector3 vec, Vector2 xLimit, Vector2 yLimit)
    {
        var res = new Vector3(
            Mathf.Clamp(vec.x, xLimit.x, xLimit.y),
            Mathf.Clamp(vec.y, yLimit.x, yLimit.y),
            0
        );
        return res;
    }
}