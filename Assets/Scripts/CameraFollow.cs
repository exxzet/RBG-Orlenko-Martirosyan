using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    public float lookUpOffset = 1f; // ������������ �������� �����, �� ������� ������� ������


    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y = transform.position.y;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // ������� ����� ���� ���� ����, ����� ������ �������� �����
        Vector3 lookAtPosition = target.position;
        lookAtPosition.y += lookUpOffset;


        transform.LookAt(lookAtPosition);
    }
}