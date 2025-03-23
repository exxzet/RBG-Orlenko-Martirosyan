using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float lookUpOffset = 1f; // ¬ертикальное смещение точки, на которую смотрит камера


    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // —оздаем точку чуть выше цели, чтобы камера смотрела вверх
        Vector3 lookAtPosition = target.position;
        lookAtPosition.y += lookUpOffset;


        transform.LookAt(lookAtPosition);
    }
}